using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Page_Backend.Data;
using Page_Backend.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Page_Backend.Authentication_and_Authorization
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthRepository(DataContext context,IConfiguration configuration,IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Korisnik korisnik = await _context.Korisnici.FirstOrDefaultAsync(x => x.Username.Equals(username));
            if (korisnik == null)
            {
                response.Message = "Korisnik nije pronađen";
                response.Success = false;
            }
            else if (!VerifyPasswordHash(password,korisnik.PasswordHash,korisnik.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Pogrešna lozinka";
            }
            else
            {
                response.Data = CreateToken(korisnik);
                response.Success = true;
                response.Message = "Uspješna prijava";
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(Korisnik korisnik, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if(await UserExists(korisnik.Username))
            {
                response.Success = false;
                response.Message = "Korisnik sa ovim korisničkim imenom već postoji";
                return response;
            }
          
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            korisnik.PasswordHash = passwordHash;
            korisnik.PasswordSalt = passwordSalt;
            await _context.Korisnici.AddAsync(korisnik);
            await _context.SaveChangesAsync();
            response.Success = true;
            response.Message = "Uspješna registracija";
            response.Data = korisnik.KorisnikId;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Korisnici.AnyAsync(x => x.Username == username))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<KorisnikViewModel>> GetById(int id)
        {

            ServiceResponse<KorisnikViewModel> serviceResponse = new ServiceResponse<KorisnikViewModel>();
            Korisnik korisnik = await _context.Korisnici.FirstOrDefaultAsync(x => x.KorisnikId == id);
            serviceResponse.Data = _mapper.Map<KorisnikViewModel>(korisnik);
            return serviceResponse;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(Korisnik korisnik)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,korisnik.KorisnikId.ToString()),
                new Claim(ClaimTypes.Name,korisnik.Username),
                new Claim(ClaimTypes.Role,korisnik.Role)

            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
