using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Page_Backend.Authentication_and_Authorization;
using Page_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Page_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(KorisnikRequestModel korisnikReq)
        {
            ServiceResponse<string> response = await _authRepo.Login(korisnikReq.Username, korisnikReq.Password);
            return Ok(response);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(KorisnikRequestModel korisnikReq)
        {
            ServiceResponse<int> response = await _authRepo.Register(new Korisnik {Username=korisnikReq.Username }, korisnikReq.Password);
            return Ok(response);
        }
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            string korisnik_id = User.FindFirst(ClaimTypes.NameIdentifier.ToString())?.Value;
            var korisnik = await _authRepo.GetById(int.Parse(korisnik_id));
            return Ok(korisnik);
        }
    }
}
