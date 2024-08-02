using Microsoft.EntityFrameworkCore;
using ToDoList.TableModels;

namespace ToDoList.DBModel
{
    public class DBContext:DbContext
    {
        private readonly IConfiguration _config;

        public DBContext(IConfiguration config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Connection = _config.GetConnectionString("TodoList");
            optionsBuilder.UseSqlServer(Connection);
        }



        public DbSet<Todo>Todo { get; set; }
    }
}
