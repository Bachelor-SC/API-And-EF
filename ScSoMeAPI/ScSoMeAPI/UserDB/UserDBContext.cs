using ScSoMeAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ScSoMeAPI.UserDB
{
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public UserDBContext(DbContextOptions options) : base(options)
        {

        }

    }
}
