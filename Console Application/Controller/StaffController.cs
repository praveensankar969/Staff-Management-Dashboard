using System;
using Console_Application.Controller;
using ConsoleApplication.Persistence;
using ConsoleApplication.Service;
using Model.DTO;
using Persistence.Interfaces;

namespace ConsoleApplication.Controller
{
    public class StaffController : BaseStaffController
    {
       public override Staff AddStaff()
        {     
            Staff staff = new Staff();
            staff = base.AddStaff();
            staff.Type = Privilege.Teacher.ToString();
            Console.Write("Subject: ");
            staff.Subject = Console.ReadLine();
            Type type = Configuration.GetStorageType();
            IActions obj = Activator.CreateInstance(type) as IActions;
            obj.AddStaff(staff);
            return staff;        
        }           
    }
}
