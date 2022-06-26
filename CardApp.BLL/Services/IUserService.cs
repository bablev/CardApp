using CardApp.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Services
{
    public interface IUserService
    {
        Task<string> Authenticate(string username, string password);
        Task RegistrationUser(RegistrationModel registrationModel);
    }
}
