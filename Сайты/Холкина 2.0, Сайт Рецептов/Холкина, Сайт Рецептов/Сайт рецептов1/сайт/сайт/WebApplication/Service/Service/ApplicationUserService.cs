namespace WebApplication.Service
{
    using System.Linq;
    using WebApplication.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;

    public class ApplicationUserService
    {
        public static string GetUserLogin(string id)
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var res = db.Users.First(x => x.Id == id);
            return res.Name;
        }

        [Display(Name = "Роль пользователя:")]
        public static string GetUserRole(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            var user = db.Users.First(x => x.Id == id);
            var roles = user.Roles;
            var dbRoles = db.Roles;
            var result = "";
            foreach(var r in roles)
            {
                var res=db.Roles.First(x => x.Id == r.RoleId);
                result += res.Name+" ";
            }

            return result;
        }

        [Display(Name = "Пароль пользователя:")]
        public static string GetUserPassword(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var res = db.Users.First(x => x.Id == id);
            return res.Password;
        }

        [Display(Name = "Аватар пользователя:")]
        public static string GetUserAvatar(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var res = db.Users.First(x => x.Id == id);
            // Ничего не вернём, если фотки нет
            if(res.Avatar.Count==0)
            {
                return string.Empty;
            }
            else
            {
                var avatar = res.Avatar.Last();
                return avatar.Url;
            }
        }
        


        [Display(Name = "Раздел о себе пользователя")]
        public static string GetUserInfo(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var res = db.Users.First(x => x.Id == id).UserInfo;

            return res;

        }
    }
}