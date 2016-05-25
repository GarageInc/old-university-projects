using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class Hardware
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
    }

    public class Producer
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
    }

    // Класс закрепленной за пользователем оргтехники
    public class Orgtechnic
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Модель")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Серийный номер")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Serial { get; set; }

        [Required]
        [Display(Name = "Пользователь")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public int HardwareId { get; set; }
        public Hardware Hardware { get; set; }

        [Required]
        [Display(Name = "Производитель")]
        public int? ProducerId { get; set; }
        public Producer Producer { get; set; }
    }
}