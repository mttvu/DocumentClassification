using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DocumentClassification.Models;
using System.Web;
using System.IO;
using DocumentClassification.Services;
using DocumentClassification.Services.Classification;
using System.Diagnostics;
using DocumentClassification.Services.Document;
using Newtonsoft.Json;

namespace DocumentClassification.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public IActionResult Index(int categoryId)
        {
            return View(_documentService.GetByCategory(categoryId));
        }

        // GET: Documents/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _documentService.Get(id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDocuments()
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files != null)
                {
                    var documents = await _documentService.AddFilesAsync(Request.Form.Files);
                    foreach (var item in documents)
                    {
                        item.File = null;
                    }
                    TempData["UploadedDocuments"] = JsonConvert.SerializeObject(documents);
                    return RedirectToAction("ShowUpload");
                }
            }
            return View();
        }

        public IActionResult ShowUpload() 
        {
            var documents = JsonConvert.DeserializeObject<IEnumerable<Document>>((string)TempData["UploadedDocuments"]);
            return View("UploadNotification", documents);
        }

        // GET: Documents/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _documentService.Get(id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _documentService.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [ActionName("PredictedCategory")]
        public IActionResult ShowPredictedCategories(List<Document> documents)
        {
            return View(documents);
        }

    }
}
