using System;
using System.ComponentModel.DataAnnotations;

namespace Model.DTO{
     public class StaffWebAPI{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; } 
    public string Password { get; set; }
    public string  Subject { get; set; }
    public int Experience { get; set; }
    public DateTime DateOfJoining { get; set; }
    public string PhoneNumber { get; set; }
    public string Type {get; set;}

   }
}