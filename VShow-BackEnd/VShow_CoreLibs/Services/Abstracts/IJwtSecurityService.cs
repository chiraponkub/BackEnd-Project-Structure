namespace VShow_BackEnd.Services.Abstracts
{
    /// <summary>
    /// ส่วนของการเข้ารหัสในรูปแบบ Json Web Token ที่สามารถถอดรหัสได้
    /// </summary>
    public interface IJwtSecurityService
    {
        /// <summary>
        /// สร้าง Token สำหรับยืนยันตัวตน
        /// </summary>
        /// <param name="id">รหัสผู้ใช้งาน</param>
        /// <param name="role">สิทธิ์ของผู้ใช้งาน</param>
        /// <param name="minute">อายุโทเคนเป็นนาที</param>
        /// <returns></returns>
        string GenerateJWTAuthentication(string id, string role, int minute = 20);


        /// <summary>
        /// สำหรับถอดรหัส Json Web Token
        /// </summary>
        /// <typeparam name="T">Class ที่ต้องการ Mapping</typeparam>
        /// <param name="token">json web token ที่เข้ารหัสแล้ว</param>
        /// <returns></returns>
        T JWTDecode<T>(string token);

        /// <summary>
        /// สำหรับเข้ารหัส Json Web Token
        /// </summary>
        /// <typeparam name="T">Class ที่ต้องการ Mapping</typeparam>
        /// <param name="data">ข้อมูลที่ต้องการมาเข้ารหัส</param>
        /// <param name="minute">เวลากำหนดอายุของ Token เป็นนาที</param>
        /// <returns></returns>
        string JWTEncode<T>(T data, int minute);
    }
}