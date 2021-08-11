using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Console_Application.Controller;
using ConsoleApplication.Persistence;
using Model.DTO;
using PersistenceLayer.Interfaces;

namespace ConsoleApplication.Controller
{
    public class StaffController : BaseStaffController
    {
       public override Staff AddStaff()
        {     
            Staff staff = new Staff();
            staff = base.AddStaff();
            staff.Type = "Support Staff";
            Console.Write("Subject: ");
            staff.Subject = Console.ReadLine();
            Type type = DataLayer.GetClass();
            IActions obj = Activator.CreateInstance(type) as IActions;
            obj.AddStaff(this);
            return staff;        
        }     
        
    }
}
