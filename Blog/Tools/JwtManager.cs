using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
namespace Blog.Api.Tools
{
    public static class JwtManager
    {

        public static Model.DTO.TokenDetail GenerateToken(Model.DTO.User user, ApplicationSettings.Main appSettings)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = System.Text.Encoding.ASCII.GetBytes(appSettings.JwtSettings.SecretKey);
                var securityAlgorithm = SecurityAlgorithms.HmacSha256Signature;

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
                        new Claim("id",user.Id.ToString()),
                        new Claim("username",user.UserName),                        
                  }),
                    Expires = DateTime.UtcNow.AddMinutes(appSettings.JwtSettings.ExpiresInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), securityAlgorithm)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new Model.DTO.TokenDetail()
                {
                    Token = tokenHandler.WriteToken(token),
                    RefreshToken = Guid.NewGuid().ToString(),
                    ExpireDateTime = tokenDescriptor.Expires.Value,
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<System.Security.Claims.Claim> VerifyToekn(string token, string secrestKey)
        {
            try
            {
                var key = System.Text.Encoding.ASCII.GetBytes(secrestKey);

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = System.TimeSpan.Zero
                }, out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);

                var jwtToken = validatedToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

                if (jwtToken == null)
                {
                    return null;
                }

                return jwtToken.Claims.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
