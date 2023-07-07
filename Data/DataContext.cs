using Microsoft.EntityFrameworkCore;
using dotnetAuthApp.Models;
namespace dotnetAuthApp.Data {

    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options){

        }

        public DbSet<UserModel> UserSet {get; set;}
        public DbSet<ExerciseModel> ExerciseSet {get; set;}
        
        public DbSet<DailyRepModel> DailyRepSet {get; set;}
    }
}