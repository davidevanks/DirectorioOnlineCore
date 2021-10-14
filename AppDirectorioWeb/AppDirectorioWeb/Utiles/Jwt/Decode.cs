using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Utiles.Jwt
{
    public class Decode:IDecode
    {
        public JwtSecurityToken DecodeToken(string token)
        {
            var tokenResult = new JwtSecurityToken(jwtEncodedString: token);
            return tokenResult;

        }
    }
}
