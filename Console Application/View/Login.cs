using System;
using ConsoleApplication.Controller;
using ConsoleApplication.Service;
using Model.DTO;

namespace ConsoleApplication.View
{
    public static class Login
    {
        //private enum Privilege { Admin, Teacher, Support }
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
                UserAccess.Privilege prv = (UserAccess.Privilege)Enum.Parse(typeof(UserAccess.Privilege), res.Type);
                switch (prv)
                {
                    case UserAccess.Privilege.Admin:
                        {
                            user.AdminActions();
                            break;
                        }
                    case UserAccess.Privilege.Support:
                    case UserAccess.Privilege.Teacher:
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

