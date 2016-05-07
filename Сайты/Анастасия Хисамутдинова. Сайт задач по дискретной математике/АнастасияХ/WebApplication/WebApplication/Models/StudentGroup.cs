using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class StudentGroup : BaseModel 
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Name { get; set; }
        
        [Display(Name = "Студенты")]
        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}