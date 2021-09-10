using BaseFacware.API.Services;
using BaseFacware.API.Services.PDFTemplateProcess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseFacware.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        public IPdfDemoProcess _pdfProcess { get; set; }
        public PDFController(IPdfDemoProcess pdfProcess)
        {
            _pdfProcess = pdfProcess;
        }

        [HttpGet, Route("{id}")]
        public IActionResult DownloadPdfFile(int id)
        {
            return _pdfProcess.ExportDemoPDF(id);
        }

        
    }
}
