using DocumentClassification.Models;
using DocumentClassification.Services.Classification;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClassification.Services.Document
{
    public class DocumentService : BaseService<Models.Document>, IDocumentService
    {
        private readonly IClassificationService _classificationService;
        public DocumentService(DocumentClassificationContext context, IClassificationService classificationService) 
            : base(context) 
        {
            _classificationService = classificationService;
        }
        public async Task AddFilesAsync(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var document = await CreateDocumentAsync(file);
                Context.Add(document);
            }
            Context.SaveChanges();
        }

        public async Task<Models.Document> CreateDocumentAsync(IFormFile file)
        {
            var document = new Models.Document();
            byte[] fileBytes;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                fileBytes = stream.ToArray();
            }
            document.Name = file.FileName;
            document.File = fileBytes;
            var category = await _classificationService.PostAsync(document);

            document.Category = Context.Category.FirstOrDefault(c => c.Name == category);

            return document;
        }

        public IEnumerable<Models.Document> GetByCategory(int categoryId)
        {
            var tesxt = Context.Document.Where(d => d.CategoryId == categoryId).ToList();
            return Context.Document.Where(d => d.CategoryId == categoryId).ToList();
        }
    }
}
