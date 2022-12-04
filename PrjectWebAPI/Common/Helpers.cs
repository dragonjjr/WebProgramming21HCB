using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
        public static string Private_key = "MIICWgIBAAKBgHaeweEe2m6BBw+KdODv1cJzS8rwQePtVIDFM/K4apLedcHyPJUW\r\nJmE7E0ioqxA7h4hJNG5maqV97l17CRAfRph8smJwFg5i0dYsJhjaNSCQE5An1KGO\r\n1AtzcQvZPn2b4n03shgD9TvVrX2aGgmCIwlW8fi9gKh3KSsYKmZi4Y7dAgMBAAEC\r\ngYA06/mF7ZT4jjpPNa+Vl4sf+P6MqQpMnVsBJHpbxOlPY07YW7GptjsjUA73cMD5\r\nOgXqyPZKdwkHkpqhPD474ihFTkM6YWS7TB1TQgjpQF9m5e+k5azadJEyZABbfaVZ\r\nzdEhRYA4HbLeHVXA2vL/Z+O1iDzu7gC7hWnbZWxOf8jaBQJBALizVyyWT651u2xu\r\naShYCYpWfB6QTTLnW15NwhHwPFLGPTgQ//EJpYBBOKiHlNw8G3bYpC1NrAbN3LwW\r\nnAmefssCQQCkaSd33T8zV9QdnjbZkFQmXYXhWCaSwQ0W+ot87gCthKhgEOd7Y+Nz\r\nIZnxpvUwAB1ouS0+QpMuCivTaFwbt4v3AkAq8pC3rm/yyi99pCLRnb8CKuALn1RE\r\nHOXzBLO2xhzQxoXfrpxE6RBRxViuX3Bu0Y81UGTEoAX7Qw0rszovRmqHAkAuSj0G\r\njpCA0DW0sRsYXn6S3roXHE6f+yLIWXp0jj46nKMbiSbjotgjTk6drzhRb3bYSWrn\r\noK73w31bZIKqex85AkAK2rg5DQOPId1ArSXPZwCOtVoNbS+RRLsM1Q86se0x0uIa\r\nfZj99ikYWRVK5/qMjSgcZZ/TYL7+24NSYnfNQAbq";
        public static string Public_key = "MIGeMA0GCSqGSIb3DQEBAQUAA4GMADCBiAKBgHaeweEe2m6BBw+KdODv1cJzS8rw\r\nQePtVIDFM/K4apLedcHyPJUWJmE7E0ioqxA7h4hJNG5maqV97l17CRAfRph8smJw\r\nFg5i0dYsJhjaNSCQE5An1KGO1AtzcQvZPn2b4n03shgD9TvVrX2aGgmCIwlW8fi9\r\ngKh3KSsYKmZi4Y7dAgMBAAE=";
        public static string Decrypt(string data)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(Private_key);
            var decryptedByte = rsa.Decrypt(dataByte, false);

            return ByteConverter.GetString(decryptedByte);
        }

        public static string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(Public_key);
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            var dataToEncrypt = ByteConverter.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();
        }
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
