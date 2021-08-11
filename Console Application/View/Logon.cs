using System;
using ConsoleApplication.Controller;
using Model.DTO;

namespace ConsoleApplication.View
{
    public static class Login
    {
        public static void Signin()
        {
            Console.Clear();
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter Password: ");
            string pass = Console.ReadLine();
            AdminControl controller = new AdminControl();
            var res = controller.Login(new LoginDTO { UserName = username, Password = pass });
            if (res != null)
            {
                UserCapability user = new UserCapability();
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
                            user.StaffAction(new User { Id = res.Id, Type = res.Type });
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else{
                Console.WriteLine("Invalid username or password");
            }
        }

        public static void Register()
        {
            StaffController obj = new StaffController();
            obj.AddStaff();

        }

    }
}

