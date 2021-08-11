using System;
using Console_Application.Controller;
using ConsoleApplication.Persistence;
using Model.DTO;
using PersistenceLayer.Interfaces;

namespace ConsoleApplication.Controller
{
    public class AdminControl : BaseStaffController
    {
       public override Staff AddStaff()
        {     
            Staff staff = base.AddStaff();
            Console.Write("Staff Type(Admin/Staff/Support): ");
            staff.Type = Console.ReadLine();

            if (staff.Type != "Admin")
            {
                Console.Write("Subject: ");
                staff.Subject = Console.ReadLine();
            }
            Type type = DataLayer.GetClass();
            IActions obj = Activator.CreateInstance(type) as IActions;
            obj.AddStaff(this); 
            return staff;                
        }     
        
    }


}