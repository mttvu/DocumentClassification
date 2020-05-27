using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassification.Models
{
    public class DocumentClassificationContext : DbContext
    {
        public DocumentClassificationContext (DbContextOptions<DocumentClassificationContext> options)
            : base(options)
        {
        }
        public DbSet<Document> Document { get; set; }
        public DbSet<Category> Category { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Category);
        }


    }
}
