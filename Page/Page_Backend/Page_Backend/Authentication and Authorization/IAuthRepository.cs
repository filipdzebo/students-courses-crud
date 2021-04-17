using Page_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend.Authentication_and_Authorization
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(Korisnik korisnik, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<ServiceResponse<KorisnikViewModel>> GetById(int id);
    }
}
