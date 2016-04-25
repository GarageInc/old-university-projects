
namespace WebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Автор")]
        public virtual ApplicationUser Author { get; set; }

        [Display(Name = "ID Автора")]
        public virtual string AuthorId { get; set; }

        [Display(Name = "Рейтинг")]
        public virtual int Karma { get; set; }

        [Display(Name = "Текст")]
        public virtual string Text { get; set; }

        [Display(Name = "Первый уровень")]
        public virtual int? ParentId { get; set; } //Идентификатор родительской новости

        public virtual bool IsDeleted { get; set; } //Флаг удаления

        public virtual DateTime AddDateTime { get; set; }

        [Display(Name = "ID рецепта")]
        public virtual int? ArticleId { get; set; }

        [Display(Name = "Рецепт")]
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}