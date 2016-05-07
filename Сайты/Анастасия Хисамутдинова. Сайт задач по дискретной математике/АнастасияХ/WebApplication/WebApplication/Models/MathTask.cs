
namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MathTask : BaseModel
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Name { get; set; }

        [Display(Name = "Описание/Пожелания")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Description { get; set; }
        
        [Display(Name = "Файл")]
        public virtual Document Document { get; set; }

        [Display(Name = "ID файла")]
        public virtual int? DocumentId { get; set; }

        [Display(Name = "Автор")]
        public virtual ApplicationUser Author { get; set; }
        
        [Display(Name = "Данные")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Data { get; set; }
        
        [Display(Name = "ID Автора")]
        public virtual string AuthorId { get; set; }
        
        [Display(Name = "Решающие")]
        public virtual ICollection<ApplicationUser> Executors { get; set; }
        
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Срок?")]
        public virtual DateTime Deadline { get; set; }

        // Статус задачи
        [Display(Name = "Статус")]
        public int Status { get; set; }

        // Приоритет задачи
        [Display(Name = "Приоритет")]
        public int Priority { get; set; }
        
        [Display(Name="Решения")]
        public virtual ICollection<MathTaskSolution> RequestSolutions { get; set; }
        
        [Display(Name = "Комментарии")]
        public virtual ICollection<Comment> Comments { get; set; }
        
        [Display(Name = "Правильное решение")]
        public virtual MathTaskSolution RightMathTaskSolution { get; set; }

        [Display(Name = "ID правильного решения")]
        public virtual int? RightRequestSolutionId { get; set; }
    }


    // Перечисление для статуса задачи
    public enum RequestStatus
    {
        Open = 1,
        Distributed = 2,
        Proccesing = 3,
        Closed = 4
    }

    // Перечисление для приоритета задачи
    public enum RequestPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }
}