using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OurProject_Part1
{
    // Пользователь
    class User
    {
        public int Id { get;set; }
        public Name Name { get; set; }
        public BirthDate BirthDate { get; set; }

        public User(int Id, Name Name, BirthDate BirthDate)
        {
            this.Id = Id;
            this.Name = Name;
            this.BirthDate = BirthDate;
        }
    }

    // Имя-Фамилия
    class Name
    {
        public string First{get;set;}
        public string Second{get;set;}

        public Name(string First, string Second)
        {
            this.First = First;
            this.Second = Second;
        }
    }

    //Дата рождения
    class BirthDate
    {
        public int Day{get;set;}
        public int Month{get;set;}
        public int Year{get;set;}

        public BirthDate(int Day, int Month, int Year)
        {
            this.Day = Day;
            this.Month = Month;
            this.Year = Year;
        }

        public string GetDate()
        {
            return this.Day.ToString() + this.Month.ToString() + this.Year.ToString();
        }
    }
}
