namespace VShow_BackEnd.Services.Abstracts
{
    /// <summary>
    /// ส่วนของการเข้ารหัสข้อมูลที่ไม่สามารถถอดรหัสได้
    /// </summary>
    public interface IHashSerucityService
    {
        /// <summary>
        /// สำหรับเข้ารหัสผ่าน Password
        /// </summary>
        /// <param name="password">รหัสผ่านที่ต้องการมาเข้ารหัส</param>
        /// <returns></returns>
        string PasswordHash(string password);

        /// <summary>
        /// สำหรับตรวจสอบ รหัสผ่านที่ยังไม่เข้ารหัสกับที่เข้ารหัสแล้วตรงกันหรือไม่
        /// </summary>
        /// <param name="password">รหัสผ่านที่ยังไม่เข้ารหัส</param>
        /// <param name="passwordHash">รหัสผ่านที่เข้ารหัสแล้ว</param>
        /// <returns></returns>
        bool PasswordVerify(string password, string passwordHash);

        /// <summary>
        /// สำหรับสร้างรหัสที่ไม่ซ้ำกัน
        /// </summary>
        /// <returns></returns>
        string GenerateGuid();
    }
}