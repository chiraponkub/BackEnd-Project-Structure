using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VShow_CoreLibs.Services.Upload
{
    public static class UploadFile
    {
        /// <summary>
        /// UploadFile & UploadImage
        /// </summary>
        /// <param name="file"></param>
        /// <param name="SetPath"></param>
        /// <returns></returns>
        public static string Upload(IFormFile file, string SetPath)
        {
            try
            {
                //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'); // ชื่อไฟล์
                var NewName = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(); // ตั้งชื่อไฟล์ไหม่
                var ContentType = file.ContentType; // ดึงค่านามสกุลไฟล์
                string folderName; // ตั้งเอาไว้กำหนด Folder + Path
                string pathToSave; // ตั้งเอาไว้บันทึกลง Folder
                string PathToSaveDb; // ตั้งเอาไว้บันทึกลง DB 
                var Splittype = file.FileName.Split(".");  // ดึงค่านามสกุลไฟล์
                int Number = Splittype.Count();
                int type = Number - 1; 
                long FileSize = file.Length; // ขนาดไฟล์
                folderName = (Path.Combine("wwwroot", string.IsNullOrEmpty(SetPath) ? "" : SetPath)).Replace("\\", "/"); // เอาไว้กำหนด Folder + Path
                pathToSave = (Directory.CreateDirectory(folderName) + "\\" + NewName + "." + Splittype[type]).Replace("\\", "/"); // เอาไว้บันทึกลง Folder
                PathToSaveDb = SetPath + "/" + NewName + "." + Splittype[type].Replace("\\", "/"); // เอาไว้บันทึกลง DB 
                using (var fileStream = new FileStream(pathToSave, FileMode.Create))
                {
                    fileStream.Position = 0;
                    file.CopyTo(fileStream);
                    return NewName + "." + Splittype[type];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
