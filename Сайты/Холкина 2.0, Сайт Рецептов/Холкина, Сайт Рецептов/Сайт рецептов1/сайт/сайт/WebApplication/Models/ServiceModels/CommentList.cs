using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CommentList
    {
       public int? Seed {get;set;} //Корневой элемент
       public IEnumerable<Comment> Comments {get;set;} //Список комментариев
    }
}