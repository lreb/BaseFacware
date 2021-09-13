using BaseFacware.API.Services.PDF.Template;
using Fluid;
using Microsoft.AspNetCore.Mvc;
using System;
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
            // retrieve data model
            var model = new PdfDemoModel();
            model.Title = "demo title";
            model.Header = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin malesuada luctus volutpat. Nulla lacus urna, tincidunt ut nulla id, volutpat aliquet metus. Vestibulum sit amet felis vel urna pellentesque viverra ac et leo. Vivamus ut venenatis enim, ac maximus diam. Fusce sit amet justo vel velit convallis tincidunt in quis nibh. Donec imperdiet lectus nec nunc iaculis fringilla. Pellentesque ac sagittis orci.";
            model.Body = "Proin elementum nec velit ac feugiat. Maecenas sollicitudin tortor vitae lectus egestas bibendum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Donec laoreet porta lectus, sed cursus elit condimentum at. Quisque vitae ligula nec enim lacinia fermentum non pretium ligula. Nulla in gravida lacus, congue posuere ante. Vestibulum in nibh pretium, mollis erat tempor, euismod metus.";
            model.Id = id;

            // build PDF template path
            string templatePath = PDFUtility.BuildTemplatePath("PDFTemplate.html", @"Services\PDF\Template");
            // read template
            string htmlString = System.IO.File.ReadAllText(templatePath);
            // use Fluid to fill template parameters
            var parser = new FluidParser();
            if (parser.TryParse(htmlString, out var template, out var error))
            {
                var engineContext = new TemplateContext(model);

                // create PDF and extract memory-stream
                var render = new IronPdf.HtmlToPdf();
                byte[] dataPDF = render.RenderHtmlAsPdf(template.Render(engineContext)).BinaryData;
                // export PDF in base binaries
                return PDFUtility.ExportPDF(dataPDF, "demoPDF");
            }
            else
            {
                Console.WriteLine($"Error: {error}");
                return null;
            }
        }
    }
}
