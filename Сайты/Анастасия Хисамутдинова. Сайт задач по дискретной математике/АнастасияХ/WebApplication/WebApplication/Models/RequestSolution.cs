
namespace WebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RequestSolution
    {
        [Key]
        [Display(Name = "ID решения задачи")]
        public virtual int Id { get; set; }
        
        [Display(Name = "Название")]
        public virtual string Name { get; set; }

        [Display(Name = "ID задачи")]
        public virtual int? RequestId { get; set; }

        [Display(Name = "Задача")]
        [ForeignKey("RequestId")]
        public virtual Request Request { get; set; }

        public virtual bool IsRightSolution { get; set; }

        [Display(Name = "Файл")]
        public virtual Document Document { get; set; }

        [Display(Name = "ID файла")]
        public virtual int? DocumentId { get; set; }

        [Display(Name = "Автор")]
        public virtual ApplicationUser Author { get; set; }

        [Display(Name = "ID Автора")]
        public virtual string AuthorId { get; set; }

        [Display(Name = "Дата выполнения")]
        public virtual DateTime Date { get; set; }

        [Display(Name = "Комментарии")]
        public virtual string Comment { get; set; }
        
        [Display(Name = "Проверено ли решение?")]
        public virtual bool IsChecked { get; set; }

        [Display(Name = "Правильно ли решено?")]
        public virtual bool Right { get; set; }

        
    }
}