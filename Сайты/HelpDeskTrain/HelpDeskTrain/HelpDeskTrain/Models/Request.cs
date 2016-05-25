using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    // Модель Заявка
    public class Request
    {
        // ID 
        public int Id { get; set; }
        // Наименование заявки
        [Required(ErrorMessage="Поле 'Название заявки' должно быть заполнено")]
        [Display(Name = "Название заявки")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
        // Описание
        [Required(ErrorMessage = "Поле 'Описание' должно быть заполнено'")]
        [Display(Name = "Описание")]
        [MaxLength(200, ErrorMessage = "Превышена максимальная длина записи")]
        public string Description { get; set; }
        // Комментарий к заявке
        [Display(Name = "Комментарий")]
        [MaxLength(200, ErrorMessage = "Превышена максимальная длина записи")]
        public string Comment { get; set; }
        // Статус заявки
        [Display(Name = "Статус")]
        public int Status { get; set; }
        // Приоритет заявки
        [Display(Name = "Приоритет")]
        public int Priority { get; set; }
        // Номер кабинета
        [Display(Name = "Кабинет")]
        public int? ActivId { get; set; }
        public Activ Activ { get; set; }
        // Файл ошибки
        [Display(Name = "Файл с ошибкой")]
        public string File { get; set; }

        // Внешний ключ Категория
        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        // Внешний ключ
        // ID Пользователя - обычное свойство
        public int? UserId { get; set; }
        // Отдел пользователя - Навигационное свойство
        public User User { get; set; }

        // Внешний ключ
        // ID Пользователя - обычное свойство
        public int? ExecutorId { get; set; }
        // Отдел пользователя - Навигационное свойство
        public User Executor { get; set; }

        // Внешний ключ
        // ID жизненного цикла заявки - обычное свойство
        public int LifecycleId { get; set; }
        // Ссылка на жизненный цикл заявки - Навигационное свойство
        public Lifecycle Lifecycle { get; set; }
    }

    // Модель жизенного цикла заявки
    public class Lifecycle
    {
        // ID 
        public int Id { get; set; }
        // Дата открытия
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Opened { get; set; }
        // Дата распределения
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Distributed { get; set; }
        // Дата обработки
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Proccesing { get; set; }
        // Дата проверки
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Checking { get; set; }
        // Дата закрытия
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Closed { get; set; }
    }

    // Перечисление для статуса заявки
    public enum RequestStatus
    {
        Open = 1,
        Distributed = 2,
        Proccesing = 3,
        Checking = 4,
        Closed = 5
    }
    // Перечисление для приоритета заявки
    public enum RequestPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }
}