
namespace WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Document : BaseModel
    {
        [Display(Name = "Ссылка")]
        public virtual string Url { get; set; }

        [Display(Name = "Размер")]
        public virtual int Size { get; set; }

        [Display(Name = "Тип")]
        public virtual string Type { get; set; }
    }
}