
namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ArticleDTO
    {
        [Key]
        [Display(Name = "ID")]
        public virtual int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Description { get; set; }

        [Display(Name = "Сложность")]
        [Required(ErrorMessage = "Обязательно для заполнения!")]
        public virtual string Difficulty { get; set; }

        [Display(Name = "Автор")]
        public virtual ApplicationUser Author { get; set; }

        [Display(Name = "ID Автора")]
        public virtual string AuthorId { get; set; }

        [Display(Name = "ID Типа")]
        public int? TypeId { get; set; }

        [Display(Name = "Тип")]
        public Type Type { get; set; }

        [Display(Name = "Фото блюда")]
        public virtual Document Document { get; set; }

        [Display(Name = "Фото блюда - внешний ключ")]
        public virtual int? DocumentId { get; set; }

    }

}