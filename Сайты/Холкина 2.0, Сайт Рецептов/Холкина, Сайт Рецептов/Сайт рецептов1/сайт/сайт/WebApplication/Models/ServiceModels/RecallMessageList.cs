using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class RecallMessageList
    {
       public int? Seed {get;set;} //Корневой элемент
       public IEnumerable<RecallMessage> RecallMessages { get;set;} //Список отзывов
    }
}