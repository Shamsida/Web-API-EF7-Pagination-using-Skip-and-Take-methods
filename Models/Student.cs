using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudTask.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName ="varchar(50)")]
        public string StudentName { get; set; } ="";
        public decimal ContactNumber { get; set;}
        public int StudentAge { get; set;}
    }
}
