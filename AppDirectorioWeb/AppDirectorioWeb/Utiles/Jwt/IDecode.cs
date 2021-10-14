using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Utiles.Jwt
{
    public interface IDecode
    {
        JwtSecurityToken DecodeToken(string token);
    }
}
