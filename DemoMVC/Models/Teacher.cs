using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    [Table("Teacher")]
    public class Teacher {
    
    [Key]
    public string? TeacherId {get; set;}
   public string? FullName {get; set;}
    
    public string? Address {get; set;}

   
    }
}