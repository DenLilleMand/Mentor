using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mentor.Models
{
    public class ProgramMessage
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set;  }

         

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ProgramId")]
        public virtual Program Program { get; set; }

    }
}