using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Linq;

namespace Console_for_pgsql
{
    //Простая работа с бд через EntityFramework

    class Program
    {
        
        static void Main(string[] args)
        {
            //Добавление данных
            using(ApplicationContext db = new ApplicationContext())
            {
                Klients klient1 = new Klients { firstname = "Tarzan", lastname= "Jungle", email = "azhad@laka.ru", age = 33 };
                Klients klient2 = new Klients { firstname = "Nazar", lastname = "Frost", email = "Frosja@koleso.ru", age = 17 };

                //Добавляем в бд наши данные
                db.klients.AddRange(klient1, klient2);
                db.SaveChanges();
            }

            //Получение данных
            using (ApplicationContext db = new ApplicationContext())
            {
                //Получение объектов и вывод в консоль
                var klients = db.klients.ToList();
                Console.WriteLine("Users list: ");
                foreach (Klients u in klients)
                {
                    Console.WriteLine($"{u.id}.{u.firstname} {u.lastname}/{u.email} - {u.age}");
                }

            }

                
        }
    }

    //
    public class Klients
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public int age { get; set; }
    }

    //Класс контекста данных
    public class ApplicationContext : DbContext
    {
        static string strConnection = "Host=localhost;Port=5432;Database=newbd;Username=postgres;Password=sa";

        public DbSet<Klients> klients { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(strConnection);
        }


    }

}
