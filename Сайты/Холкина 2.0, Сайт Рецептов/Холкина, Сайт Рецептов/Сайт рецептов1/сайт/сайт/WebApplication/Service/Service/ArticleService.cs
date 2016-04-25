namespace WebApplication.Service
{
    using System.Linq;
    using WebApplication.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;

    public class ArticleService
    {
        
        [Display(Name = "Фото блюда:")]
        public static string GetArticleAvatar(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            int ID = int.Parse(id);
            var res = db.Articles.Find(ID);
            // Ничего не вернём, если фотки нет
            if(res.Document==null)
            {
                return string.Empty;
            }
            else
            {
                return res.Document.Url;
            }
        }
        
    }
}