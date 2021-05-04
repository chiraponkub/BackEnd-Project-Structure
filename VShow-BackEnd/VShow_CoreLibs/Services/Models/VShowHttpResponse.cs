using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VShow_BackEnd.Services.Models
{
    public class VShowHttpResponse<T>
    {
        /// <summary>
        /// ข้อความที่ Server ปลายทางส่งมา
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// ข้อมูลที่ Server ปลายทางส่งมา
        /// </summary>
        public T data { get; set; }
    }
}
