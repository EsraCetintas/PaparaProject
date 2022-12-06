using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Utilities.Security.JWT
{
    public interface ITokenService
    {
        Token CreateAccessToken(User user);
        string CreateRefreshToken();
    }
}
