using System.IdentityModel.Tokens.Jwt;

namespace AppDirectorioWeb.Utiles.Jwt
{
    public interface IDecode
    {
        #region Public Methods

        JwtSecurityToken DecodeToken(string token);

        #endregion Public Methods
    }
}