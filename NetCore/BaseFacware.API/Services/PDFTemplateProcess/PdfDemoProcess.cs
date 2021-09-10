using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BaseFacware.API.Services.PDFTemplateProcess
{
    public interface IPdfDemoProcess 
    {
        public FileStreamResult ExportDemoPDF(int id);
    }

    public class PdfDemoProcess : IPdfDemoProcess
    {
        public FileStreamResult ExportDemoPDF(int id)
        {
            // build PDF template path
            string templatePath = PDFUtility.BuildTemplatePath("PDFTemplate.html", @"Services\PDF\Template");
            // read template
            string htmlString = System.IO.File.ReadAllText(templatePath);
            // fill template parameters
            StringBuilder htmlPDF = new StringBuilder(htmlString);
            htmlPDF.Replace("{0}", $"{id}");
            // create PDF and extract memory-stream
            var render = new IronPdf.HtmlToPdf();
            byte[] dataPDF = render.RenderHtmlAsPdf(htmlPDF.ToString()).BinaryData;
            // export PDF in base binaries
            return PDFUtility.ExportPDF(dataPDF, "demoPDF");
        }
    }
}
