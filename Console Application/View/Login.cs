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
                Privilege prv;
                if (!Enum.TryParse(res.Type, out prv))
                {
                    Console.WriteLine("Error on staff type!!");
                }
                else
                {
                    switch (prv)
                    {
                        case Privilege.Admin:
                            {
                                user.AdminActions();
                                break;
                            }
                        case Privilege.Support:
                        case Privilege.Teacher:
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

            }
            else
            {
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

