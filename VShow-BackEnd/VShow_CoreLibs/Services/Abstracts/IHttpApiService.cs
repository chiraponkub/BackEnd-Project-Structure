using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VShow_BackEnd.Services.Models;

namespace VShow_BackEnd.Services.Abstracts
{
    /// <summary>
    /// ส่วนของการส่งข้อมูลไปหา Server อื่นๆในรูปแบบ Resful API [GET, POST, PUT, DELETE]
    /// </summary>
    public interface IHttpApiService
    {
        /// <summary>
        /// เพิ่ม Authrizeation Header เพื่อยืนยันตัวตน
        /// </summary>
        /// <param name="authorization">accessToken สำหรับยืนยัน</param>
        void Authorization(string authorization);

        /// <summary>
        /// สำหรับส่งข้อมูลด้วย Method DELETE
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <returns>ส่งค่ากลับมาเป็นสตริง</returns>
        Task<HttpResponse<string>> Delete(string url);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย Method DELETE
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <returns>ส่งค่ากลับมาเป็นคลาสที่ทำการ Mapping</returns>
        Task<HttpResponse<T>> Delete<T>(string url);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย Method GET
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <returns>ส่งค่ากลับมาเป็นสตริง</returns>
        Task<HttpResponse<string>> Get(string url);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย Method GET
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <returns>ส่งค่ากลับมาเป็นคลาสที่ทำการ Mapping</returns>
        Task<HttpResponse<T>> Get<T>(string url);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย Method POST
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <param name="data">Data ข้อมูลที่ต้องการส่ง</param>
        /// <returns>ส่งค่ากลับมาเป็นสตริง</returns>
        Task<HttpResponse<string>> Post(string url, object data);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย Method POST
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <param name="data">Data ข้อมูลที่ต้องการส่ง</param>
        /// <returns>ส่งค่ากลับมาเป็นคลาสที่ทำการ Mapping</returns>
        Task<HttpResponse<T>> Post<T>(string url, object data);
        /// <summary>
        /// สำหรับส่ง File ด้วย Method POST
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <param name="files">File ข้อมูลที่ต้องการส่ง</param>
        /// <returns>ส่งค่ากลับมาเป็นสตริง</returns>
        Task<HttpResponse<string>> PostFile(string url, List<IFormFile> files);
        /// <summary>
        /// สำหรับส่ง File ด้วย Method POST
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <param name="files">File ข้อมูลที่ต้องการส่ง</param>
        /// <returns>ส่งค่ากลับมาเป็นคลาสที่ทำการ Mapping</returns>
        Task<HttpResponse<T>> PostFile<T>(string url, List<IFormFile> files);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย Method PUT
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <param name="data">Data ข้อมูลที่ต้องการส่ง</param>
        /// <returns>ส่งค่ากลับมาเป็นสตริง</returns>
        Task<HttpResponse<string>> Put(string url, object data);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย Method PUT
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <param name="data">Data ข้อมูลที่ต้องการส่ง</param>
        /// <returns>ส่งค่ากลับมาเป็นคลาสที่ทำการ Mapping</returns>
        Task<HttpResponse<T>> Put<T>(string url, object data);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย การใส่ Method เอง
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <param name="method">Method ที่ต้องการส่งข้อมูล</param>
        /// <param name="data">Data ข้อมูลที่ต้องการส่งหากว่าเป็น Method POST หรือ PUT</param>
        /// <param name="headers">Header สำหรับแนบข้อมูลบางอย่างไป</param>
        /// <returns>ส่งค่ากลับมาเป็นสตริง</returns>
        Task<HttpResponse<string>> Send(string url, HttpMethod method, object data, Dictionary<string, string> headers = null);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย การใส่ Method เอง
        /// </summary>
        /// <param name="url">URL ที่ต้องการส่งข้อมูล</param>
        /// <param name="method">Method ที่ต้องการส่งข้อมูล</param>
        /// <param name="data">Data ข้อมูลที่ต้องการส่งหากว่าเป็น Method POST หรือ PUT</param>
        /// <param name="headers">Header สำหรับแนบข้อมูลบางอย่างไป</param>
        /// <returns>ส่งค่ากลับมาเป็นคลาสที่ทำการ Mapping</returns>
        Task<HttpResponse<T>> Send<T>(string url, HttpMethod method, object data, Dictionary<string, string> headers = null);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย การกำหนดค่าเองทั้งหมด
        /// </summary>
        /// <param name="httpRequest">Http Request ค่าที่ต้องการส่ง</param>
        /// <returns>ส่งค่ากลับมาเป็นสตริง</returns>
        Task<HttpResponse<string>> Send(HttpRequestMessage httpRequest);
        /// <summary>
        /// สำหรับส่งข้อมูลด้วย การกำหนดค่าเองทั้งหมด
        /// </summary>
        /// <param name="httpRequest">Http Request ค่าที่ต้องการส่ง</param>
        /// <returns>ส่งค่ากลับมาเป็นคลาสที่ทำการ Mapping</returns>
        Task<HttpResponse<T>> Send<T>(HttpRequestMessage httpRequest);
    }
}
