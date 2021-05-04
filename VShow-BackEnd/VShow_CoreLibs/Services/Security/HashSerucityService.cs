using System;
using System.Security.Cryptography;
using VShow_BackEnd.Services.Abstracts;

namespace VShow_BackEnd.Services.Security
{
    /// <summary>
    /// ส่วนของการเข้ารหัสข้อมูลที่ไม่สามารถถอดรหัสได้
    /// </summary>
    public class HashSerucityService : IHashSerucityService
    {
        private const int SALT_SIZE = 24;
        private const int NUM_ITERATIONS = 1000;
        private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        /// <summary>
        /// สุ่ม Guid ขึ้นมา
        /// </summary>
        /// <returns></returns>
        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// เข้ารหัส สำหรับสมัครสมาชิกที่ถอดไม่ได้มีแต่ต้อง verify เท่านั้น
        /// </summary>
        /// <param name="password">รหัสผ่าน</param>
        /// <returns></returns>
        public string PasswordHash(string password)
        {
            byte[] buf = new byte[SALT_SIZE];
            rng.GetBytes(buf);
            string salt = Convert.ToBase64String(buf);

            Rfc2898DeriveBytes deriver2898 = new Rfc2898DeriveBytes(password.Trim(), buf, NUM_ITERATIONS);
            string hash = Convert.ToBase64String(deriver2898.GetBytes(16));
            return salt + ':' + hash;
        }

        /// <summary>
        /// ตรวจสอบรหัสผ่านมีเปรียบเทียบกันว่าถูกมั้ย
        /// </summary>
        /// <param name="password">รหัสผ่าน</param>
        /// <param name="passwordHash">รหัสผ่านที่เข้ารหัสแล้ว</param>
        /// <returns></returns>
        public bool PasswordVerify(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                return false;
            byte[] buf = Convert.FromBase64String(parts[0]);
            Rfc2898DeriveBytes deriver2898 = new Rfc2898DeriveBytes(password.Trim(), buf, NUM_ITERATIONS);
            string computedHash = Convert.ToBase64String(deriver2898.GetBytes(16));
            return parts[1].Equals(computedHash);
        }
    }
}
