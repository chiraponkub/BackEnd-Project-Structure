using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace VShow_BackEnd.Configs
{
    /// <summary>
    /// สำหรับทำส่วนดักจับ Error Message ที่เข้าถึงแบบ Dynamic
    /// </summary>
    public static class ExceptionConfig
    {
        /// <summary>
        /// ดึงข้อมูล Error ที่อยู่ในสุดของ Exception ออกมา
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string ErrorMessage(this Exception exception)
        {
            if (exception.InnerException != null)
                return exception.InnerException.ErrorMessage();
            return exception.Message;
        }

        /// <summary>
        /// เก็บข้อมูล Error Exception ใส่ไว้ใน ModelState
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="exception"></param>
        public static void AddException(this ModelStateDictionary modelState, Exception exception)
        {
            modelState.AddModelError(string.Empty, exception.ErrorMessage());
        }

        /// <summary>
        /// แสดงข้อความ Error ตัวแรกสุดของ ModelState
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string ErrorMessage(this ModelStateDictionary modelState)
        {
            var returnMessage = "";
            if (modelState.Values.Count() > 0)
            {   
                var Errors = modelState.Values.FirstOrDefault().Errors;
                if (Errors.Count > 0)
                {
                    returnMessage = Errors[0].ErrorMessage;
                }
            }
            return returnMessage;
        }
    }
}
