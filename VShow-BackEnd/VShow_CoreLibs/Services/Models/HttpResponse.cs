using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace VShow_BackEnd.Services.Models
{
    /// <summary>
    /// สำหรับ Return ค่า Http ออกมา
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResponse<T>
    {
        /// <summary>
        /// รหัสบ่งบอกว่าส่งข้อมูลสำเร็จหรือไม่
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// ข้อความบ่งบอกว่าส่งข้อมูลสำเร็จหรือไม่
        /// </summary>
        public string ReasonPhrase { get; set; }

        /// <summary>
        /// ข้อมูลที่ส่งไปหา Server ปลายทาง
        /// </summary>
        public HttpRequestMessage RequestMessage { get; set; }

        /// <summary>
        /// ข้อมูล Header ที่ได้กลับมาจาก Server ปลายทาง
        /// </summary>
        public HttpResponseHeaders Headers { get; set; }

        /// <summary>
        /// ข้อความ Error หากเกิดข้อผิดพลาด (กรณีเกิด Error ที่ตัวระบบ)
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// ข้อมูลที่ Server ปลายทางส่งมา (กรณีเกิด Error ที่ตัวระบบ)
        /// </summary>
        public string OutputMessage { get; set; }

        /// <summary>
        /// ข้อมูลที่ Server ปลายทางส่งมา
        /// </summary>
        public T Content { get; set; }
    }
}
