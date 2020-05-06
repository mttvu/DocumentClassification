using DocumentClassification.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClassification.Services.Document
{
    public interface IDocumentService : IBaseService<Models.Document>
    {
        Task AddFilesAsync(IFormFileCollection files);
        IEnumerable<Models.Document> GetByCategory(int category);
        Task<Models.Document> CreateDocumentAsync(IFormFile file);
    }
}
