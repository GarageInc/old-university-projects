namespace WebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    ///  Отзыв
    /// </summary>
    public class RecallMessage
    {
        [Key]
        [Display(Name = "ID сообщения")]
        public virtual int Id { get; set; }

        [Display(Name = "Текст")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Text { get; set; }
        
        [Display(Name = "Автор")]
        public virtual ApplicationUser Author { get; set; }

        [Display(Name = "ID Автора")]
        public virtual string AuthorId { get; set; }
        
        [Display(Name = "Рейтинг")]
        public virtual int Karma { get; set; }

        [Display(Name = "Первый уровень")]
        public virtual int? ParentId { get; set; } //Идентификатор родительской новости

        [Display(Name = "Удаленность")]
        public virtual bool IsDeleted { get; set; } //Флаг удаления

        [Display(Name = "Дата добавления")]
        public virtual DateTime AddDateTime { get; set; }

        [Display(Name = "Флаг 'О сайте'")]
        public  virtual bool AboutSite { get; set; }

        [Display(Name = "ID адресата")]
        public virtual string UserId { get; set; }

        [Display(Name = "Адресат")]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}