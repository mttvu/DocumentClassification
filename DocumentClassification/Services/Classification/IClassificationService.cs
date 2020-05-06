using DocumentClassification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClassification.Services.Classification
{
    public interface IClassificationService
    {
        Task<string> PostAsync(Models.Document document);
    }
}
