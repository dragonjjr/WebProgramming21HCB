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
        public static string PublicKey = "<RSAKeyValue><Modulus>wDMByIE1irne3d2NBJ9IWL/3iltV7kwBEBYy7zkftv/bnx13cEuzLS7l1+6Y2WcxU4KOwW+Kjxe0rMAjKI72ZK0igV3mdnY3xfDtTXNr+3+k7bzC8KnGLXN6QYNM60RcUpLHqs5pi7x7uMWLtBKfpXYCbsAAj+NhFapJkpFFdb8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static string PrivateKey = "<RSAKeyValue><Modulus>wDMByIE1irne3d2NBJ9IWL/3iltV7kwBEBYy7zkftv/bnx13cEuzLS7l1+6Y2WcxU4KOwW+Kjxe0rMAjKI72ZK0igV3mdnY3xfDtTXNr+3+k7bzC8KnGLXN6QYNM60RcUpLHqs5pi7x7uMWLtBKfpXYCbsAAj+NhFapJkpFFdb8=</Modulus><Exponent>AQAB</Exponent><P>36fdifJI9dU83A3Q4i6ctX0osoUwNebGRF0nc0rtZHivATyXWPysrZQ2GmBKEFRZfjnRDLEH4awY3cZfS/aCWQ==</P><Q>2/6RCLU+8b5+4iy6gSxBRADEdgDCTTK69q6pbsiTrnT0it0x4MUkHCuFdYo9z6dLHDsosmsXCpDQMpqSmXxF1w==</Q><DP>JRF/aFOdwBDdi2NG0ZYEJxhdXGkyulxLVB1UYolymwpdhwjx1K/cNtCvvuNiox43zvHqMf5NXhvV6zvro31x0Q==</DP><DQ>Kl6W9ERkAQ8dRNY0fVhWoZA8RjXTNicFFymAfFOpDbp8tpnvV0jgsYQ4SfD8AphHwQIrzmENqP1G+9gFUAY9NQ==</DQ><InverseQ>q+nuWefoKuEHuSHR1Aaos/vbdKDE4MBocMCXURMlBG+dYVhWO2YPNidavNMcwXp0/cZOiHfj6DBAxuNQu1Jqtw==</InverseQ><D>CYB8DhWVOA6IXh+d4SSexwR2kHiDfwxy4QC38+u3Da0Iho1GYl7btNgktNAu7lCTt7U0qYuCJiDd5cx58H9g3vNhVuyFZ/XVVku7WqzAq9z+AKFTgp0W3EzN6POPixh2iGCkKArZX/ywHoyQM0ctpH02ORqaMJuHRk01mvvjpoE=</D></RSAKeyValue>";


        
        public static string GetToken(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + SecretKey);
            SHA256 sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static string Encryption(string strText)
        {
            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    rsa.FromXmlString(PublicKey.ToString());

                    var encryptedData = rsa.Encrypt(testData, true);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private static string Decryption(string strText)
        {
            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var base64Encrypted = strText;
                    rsa.FromXmlString(PrivateKey);

                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
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
