using System;
using ConsoleApplication.Controller;
using Model.DTO;

namespace ConsoleApplication.View
{
    public static class Logon
    {
        public static void LogonScreen()
        {
            AdminCapability user = new AdminCapability();
            AdminControl controller = new AdminControl();
            Console.Clear();
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter Password: ");
            string pass = Console.ReadLine();
            var res = controller.Login(new LoginDTO {UserName= username, Password = pass});
            switch (res.Type)
            {
                case "Admin":
                    {
                        user.AdminActions();
                        break;
                    }
                case "Teaching Staff":
                case "Support Staff":
                    {
                        user.StaffAction(new User { Id = res.Id, Type = res.Type});
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong username or password");
                        break;
                    }
            }

        }

        public static void Register()
        {
            StaffController obj = new StaffController();
            obj.AddStaff();

        }

    }
}

