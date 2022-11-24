using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class Helpers
    {
        public static string conn = string.Empty;
        public static string SecretKey = "abc";
        public static string JWT_key = string.Empty;
        public static int JWT_Time = 0;
        public static string JWT_Issuer = string.Empty;
        public static string JWT_Audience = string.Empty;
        public enum RoleID
        {
            Staff = 0,
            Customer = 1
        }

        /// <summary>
        /// Tạo OTP random
        /// </summary>
        /// <returns></returns>
        public static string GenerateOTP()
        {
            Random rd = new Random();
            string OTP = rd.Next(100000,999999).ToString();
            return OTP;
        }

        public static JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_key));
            _ = int.TryParse(JWT_Time.ToString(), out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: JWT_Issuer,
                audience: JWT_Audience,
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
