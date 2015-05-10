﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mentor.Models
{
    public class Notification
    {

         public Notification()
        {
            this.Users = new HashSet<User>();
           
        }

        public int Id { get; set; }
        public int ProgramId { get; set; }

        public string Text { get; set; }



        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int InterestId { get; set; }


        //
        public virtual ICollection<User> Users { get; set; }
            [ForeignKey("ProgramId")]
          public virtual Program Program { get; set; }


    }

    }
}