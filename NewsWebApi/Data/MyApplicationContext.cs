using Microsoft.EntityFrameworkCore;

namespace NewsWebApi.Data
{
    public class MyApplicationContext : DbContext
    {
         
        public DbSet<Source> Sources { get; set; }
        // Вместо DbSet<Student> делаем DbSet для новостей
        public DbSet<News> News { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Название файла базы данных можно назвать по вашему проекту, например news.db
                optionsBuilder.UseSqlite("Data Source=news.db");
            }
        }
    }
}