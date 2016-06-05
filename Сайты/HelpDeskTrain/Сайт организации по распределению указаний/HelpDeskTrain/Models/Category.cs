using System.ComponentModel.DataAnnotations;
namespace HelpDeskTrain.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название категории")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
    }
}