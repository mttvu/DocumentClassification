using DocumentClassification.Models;
using DocumentClassification.Services.Classification;
using DocumentClassification.Services.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DocumentClassification.Services
{
    public class ClassificationService : IClassificationService
    {
        private HttpClient _httpClient;
        private readonly ClassificationSettings _options;
        public ClassificationService(IOptions<ClassificationSettings> options)
        {
            _httpClient = new HttpClient();
            _options = options.Value;
        }
        public async Task<string> PostAsync(Models.Document document)
        {
            var fileContent = new StreamContent(new MemoryStream(document.File));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = "document", FileName = document.Name };
            var content = new MultipartFormDataContent();
            content.Add(fileContent, "document", document.Name);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_options.ApiUrl + "/analyzer/"),
                Content = content
            };
            var response = await _httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
