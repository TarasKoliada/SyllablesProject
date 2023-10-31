using Sklady;
using Sklady.Export;
using Sklady.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class SyllablesController : ApiController
    {
        [HttpPost]
        public ResultViewModel GetTextProcessingResult(string inputText, Settings settings)
        {
            if (settings == null)
                settings = new Settings();

            var exporter = new ResultsExporter(settings);
            var analyzer = new TextAnalyzer(inputText, String.Empty, settings, exporter);
            var result = analyzer.GetResults();            

            return new ResultViewModel()
            {
                ReadableResults = exporter.GetSyllables(result.ReadableResults),
                CVVResults = exporter.GetSyllablesCVV(result.CvvResults)
            };
        }
    }
}
