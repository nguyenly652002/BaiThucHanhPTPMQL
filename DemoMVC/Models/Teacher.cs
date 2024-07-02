using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    [Table("Teachers")]
    public class Teacher {
    
    [Key]
    public string? TeacherId {get; set;}
   public string? FullName {get; set;}
    
 

   
    }
}
