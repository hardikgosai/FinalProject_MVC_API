using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface ILoginRepository
    {
        UserLogin AuthenticateUser(string email, string password);
    }
}
