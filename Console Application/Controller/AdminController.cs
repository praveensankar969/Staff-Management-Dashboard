using System;
using Console_Application.Controller;
using ConsoleApplication.Persistence;
using ConsoleApplication.Service;
using Model.DTO;
using Persistence.Interfaces;

namespace ConsoleApplication.Controller
{
    public class AdminControl : BaseStaffController
    {
        public override Staff AddStaff()
        {
            Staff staff = base.AddStaff();
            Console.Write("Staff Type(Admin/Teacher/Support): ");
            bool flag = false;
            do
            {
                Privilege prev;
                if (!Enum.TryParse(Console.ReadLine(), out prev))
                {
                    Console.WriteLine("Invalid user access type");
                    Console.WriteLine("Enter Staff Type: ");
                    flag = false;
                }
                else
                {
                    staff.Type = prev.ToString();
                    flag = true;
                }
            } while (!flag);

            if (staff.Type != "Admin")
            {
                Console.Write("Subject: ");
                staff.Subject = Console.ReadLine();
            }
            Type type = Configuration.GetStorageType();
            IActions obj = Activator.CreateInstance(type) as IActions;
            obj.AddStaff(staff);
            return staff;
        }

    }


}