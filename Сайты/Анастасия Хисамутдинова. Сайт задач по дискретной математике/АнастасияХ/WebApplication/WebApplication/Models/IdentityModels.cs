
namespace WebApplication.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;

    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Имя/логин пользователя")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Display(Name = "Рейтинг/карма")]
        public virtual int Karma { get; set; }

        [Display(Name="Аватарка")]
        public virtual ICollection<Document> Avatar { get; set; }

        [Display(Name = "Задачи")]
        public virtual ICollection<Request> Requests { get; set; }
        [Display(Name = "Решения задач")]
        public virtual ICollection<RequestSolution> RequestSolutions { get; set; }

        [Display(Name = "Дата последнего посещения")]
        public virtual DateTime LastVisition { get; set; } 

        [Display(Name = "Оставленные отзывы")]
        public virtual ICollection<RecallMessage> RecallMessages { get; set; }

        //[Display(Name = "Сообщения об ошибках")]
        //public virtual ICollection<ErrorMessage> ErrorMessages { get; set; }
        
        [Display(Name = "Контакты")]
        public virtual ICollection<Contact> Contacts { get; set; }

        [Display(Name = "Дата регистрации")]
        public virtual DateTime RegistrationDate { get; set; }

        [Display(Name = "О себе")]
        public virtual string UserInfo { get; set; }

        [Display(Name = "Комментарии")]
        public virtual ICollection<Comment> Comments { get; set; }

        [Display(Name = "Заблокирован?")]
        public  virtual bool IsBlocked { get; set; }

        [Display(Name = "Дата блокировки")]
        public  virtual DateTime BlockDate { get; set; }

        [Display(Name = "Причина блокировки")]
        public  virtual  string BlockReason { get; set; }

        [Display(Name = "Плюсованные комментарии")]
        public virtual ICollection<Comment> UpComments { get; set; }

        [Display(Name = "Минусованные комментарии")]
        public virtual ICollection<Comment> DownComments { get; set; }

        [Display(Name = "Плюсованные отзывы")]
        public virtual ICollection<RecallMessage> UpRecalls { get; set; }

        [Display(Name = "Минусованные отзывы")]
        public virtual ICollection<RecallMessage> DownRecalls { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }

        
    }

}