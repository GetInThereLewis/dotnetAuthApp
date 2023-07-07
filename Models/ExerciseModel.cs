using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnetAuthApp.Models {
    public class ExerciseModel {
        [Key]
        public int ExerciseId{get; set;}
        
        public int UserId{get; set;}
    
        public DateTime NextCommit{get; set;}

        public DateTime LastCommit{get; set;}

        public DailyRepModel[] DailyReps{get; set;}    
        [ForeignKey("UserId")]   
        public UserModel User{get; set;}
    }
public class DailyRepModel {
        [Key]
        public int RepId{get; set;}
        
        public int ExerciseId{get; set;}
        public DateTime Day{get; set;}
        public int Repetitions{get; set;}

        [ForeignKey("ExerciseId")]
        public ExerciseModel Exercise{get; set;}
    }

}