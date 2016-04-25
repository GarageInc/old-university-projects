
using System.Collections.Generic;

namespace WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Ingridient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения!")]
        [Display(Name = "Название ингридиента")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи(50 символов)")]
        public string Name { get; set; }
        
        // Внешний ключ Категория
        [Display(Name = "ID Категории")]
        public int? CategoryId { get; set; }

        [Display(Name = "Категория")]
        public Category Category { get; set; }
        
        [Display(Name="Рецепты, в которые входит данный ингридиент")]
        public virtual ICollection<Article> Articles { get; set; }

        public Ingridient()
        {
            Articles = new List<Article>();
        }
    }
}