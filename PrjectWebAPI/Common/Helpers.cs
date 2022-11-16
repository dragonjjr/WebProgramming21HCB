using System;

namespace Common
{
    public static class Helpers
    {
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
    }
}
