
namespace WebApplication.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения!")]
        [Display(Name = "Название категории")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи(50 символов)")]
        public string Name { get; set; }

        public virtual ICollection<Ingridient> Ingridients { get; set; }
    }
}