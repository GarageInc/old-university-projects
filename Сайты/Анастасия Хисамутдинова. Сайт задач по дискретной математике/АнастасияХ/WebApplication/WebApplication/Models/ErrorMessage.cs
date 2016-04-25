

namespace WebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ErrorMessage
    {
        [Display(Name = "ID сообщения")]
        public virtual int Id { get; set; }

        [Display(Name = "Сообщение")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Text { get; set; }

        [Display(Name = "Файлы")]
        public virtual Document Document { get; set; }

        [Display(Name = "Автор")]
        public virtual ApplicationUser Author { get; set; }

        [Display(Name = "ID Автора")]
        public virtual string AuthorId { get; set; }

        // Статус задачи
        [Display(Name = "Статус")]
        public int ErrorStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата создания")]
        public virtual DateTime CreateDate { get; set; }
        
        [Display(Name = "Для администрации")]
        public virtual bool ForAdministration { get; set; }

        [Display(Name = "Мыло")]
        public virtual string Email { get; set; }
    }

    // Перечисление для статуса задачи
    public enum ErrorMessageStatus
    {
        Open = 0,
        Closed = 1
    }
}