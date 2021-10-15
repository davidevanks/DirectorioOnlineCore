using System.IdentityModel.Tokens.Jwt;

namespace AppDirectorioWeb.Utiles.Jwt
{
    public class Decode : IDecode
    {
        #region Public Methods

        public JwtSecurityToken DecodeToken(string token)
        {
            var tokenResult = new JwtSecurityToken(jwtEncodedString: token);
            return tokenResult;
        }

        #endregion Public Methods
    }
}