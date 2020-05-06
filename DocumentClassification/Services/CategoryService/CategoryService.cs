using DocumentClassification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClassification.Services.CategoryService
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(DocumentClassificationContext context)
            : base(context)
        {

        }
    }
}
