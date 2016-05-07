using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class BaseModel
    {

        [Key]
        [Display(Name = "ID")]
        public virtual int Id { get; set; }

    }
}