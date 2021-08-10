using System;
using Console_Application.Controller;
using ConsoleApplication.Persistence;

namespace ConsoleApplication.Controller
{
    public class AdminControl : BaseStaffController
    {
       public override void AddStaff()
        {     
            base.AddStaff();
            Console.Write("Staff Type(Admin/Staff/Support): ");
            Type = Console.ReadLine();

            if (Type != "Admin")
            {
                Console.Write("Subject: ");
                Subject = Console.ReadLine();
            }
            Type type = DataLayer.GetClass();
            var obj = Activator.CreateInstance(type);
            type.GetMethod("AddStaff").MakeGenericMethod(typeof(AdminControl)).Invoke(obj, new object[] { this });
            
        }     
        
    }


}