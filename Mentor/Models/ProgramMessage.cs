using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models
{
    public class ProgramMessage : IContextEntity
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set;  }

         

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ProgramId")]
        public virtual Program Program { get; set; }

    }
}