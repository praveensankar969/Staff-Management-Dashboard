using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Console_Application.Controller;
using ConsoleApplication.Persistence;
using Model.DTO;
using Newtonsoft.Json;

namespace ConsoleApplication.Controller
{
    public class StaffController : BaseStaffController
    {
       public override void AddStaff()
        {     
            base.AddStaff();
            Type = "Support Staff";
            Console.Write("Subject: ");
            Subject = Console.ReadLine();
            Type type = DataLayer.GetClass();
            var obj = Activator.CreateInstance(type);
            type.GetMethod("AddStaff").MakeGenericMethod(typeof(StaffController)).Invoke(obj, new object[] { this });
            
        }     
        
    }
}
