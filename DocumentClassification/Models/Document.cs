using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DocumentClassification.Models
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Name { get; set; }

        [Column(TypeName = "int")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Column(TypeName = "varbinary(MAX)")]
        public byte[] File { get; set; }
    }
}
