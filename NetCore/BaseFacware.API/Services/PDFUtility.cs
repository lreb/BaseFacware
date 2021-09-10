using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace BaseFacware.API.Services
{
    public static class PDFUtility
    {
        public const string MimeType = "application/pdf";
        public const string FileExtension = "pdf";

        public static string BuildTemplatePath(string templateName, string templateLocation)
        {
            string baseDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            return Path.Combine(baseDirectory, @$"{templateLocation}\{templateName}");
        }

        public static FileStreamResult ExportPDF(byte[] pdfByteData, string fileName)
        {
            Stream stream = new MemoryStream(pdfByteData);
            return new FileStreamResult(stream, MimeType)
            {
                FileDownloadName = $"{fileName}.{FileExtension}"
            };
        }
    }
}
