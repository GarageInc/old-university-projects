using System.Collections.Generic;

namespace WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Type
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи(50 символов)")]
        public string Name { get; set; }
        
    }
}