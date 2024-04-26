using DTO;
using DTO.Enum;
using Microsoft.AspNetCore.Http;
using Twilio.TwiML.Voice;

namespace WebUI.Utility
{
    public static class ImagesUtilities
    {
        public static string SaveFile(IFormFile formFile, string path, ImagesType type)
        {
            string filePath = string.Empty;
            string absolutePath = string.Empty;
            if (formFile.Length > 0)
            {
                string guid = Guid.NewGuid().ToString();

                filePath = GetPath(formFile, path, guid, type);
                absolutePath = GetAbsolute(formFile, path, guid, type);
                using (var stream = System.IO.File.Create(filePath))
                {
                    formFile.CopyTo(stream);
                }
            }
            return absolutePath;
        }

        private static string GetPath(IFormFile formFile, string path, string id, ImagesType imageType)
        {
            string folder = string.Empty;
            switch (imageType)
            {
                case ImagesType.Event:
                    folder = "events";
                    break;
                case ImagesType.User:
                    folder = "users";
                    break;
            }
            if (!Directory.Exists(path + "wwwroot\\img\\" + folder)) { Directory.CreateDirectory(path + "wwwroot\\img\\" + folder); }

            return Path.Combine(path + "wwwroot\\img\\" + folder, id + Path.GetExtension(formFile.FileName));
        }

        private static string GetAbsolute(IFormFile formFile, string path, string id, ImagesType imageType)
        {
            string folder = string.Empty;
            switch (imageType)
            {
                case ImagesType.Event:
                    folder = "events";
                    break;
                case ImagesType.User:
                    folder = "users";
                    break;
            }

            return string.Format("/img/{0}/{1}", folder, id + Path.GetExtension(formFile.FileName));
        }
    }
}
