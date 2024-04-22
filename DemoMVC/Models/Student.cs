using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    [Table("Student")]
    public class Student : Person
    {
        public string? StudentCode { get; set; }
    }
}