namespace VShow_BackEnd.Services.Models
{
    /// <summary>
    /// ข้อมูล Json Web Token
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JwtPayload<T>
    {
        /// <summary>
        /// ข้อมูลที่เข้าหรือถอดรหัส
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// เวลาหมดอายุ
        /// </summary>
        public long Expire { get; set; }
    }
}
