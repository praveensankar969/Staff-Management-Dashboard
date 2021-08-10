using System;
using ConsoleApplication.Controller;
using Model.DTO;

namespace ConsoleApplication.View
{
    public static class Logon
    {
        public static void LogonScreen(int selectedOption)
        {
            AdminCapability user = new AdminCapability();
            AdminControl controller = new AdminControl();
            switch (selectedOption)
            {
                case 1:
                    {
                        Console.Clear();
                        Console.Write("Enter Username: ");
                        string username = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Enter Password: ");
                        string pass = Console.ReadLine();
                        var res = controller.Login(new LoginDTO{UserName=username, Password=pass});
                        if(res.Id <1){
                            Console.WriteLine("Invalid username or password");
                            break;
                        }
                        user.AdminActions(new User{Id = res.Id, Type=res.Type});
                        break;
                    }
                case 2:
                    {
                        user.StaffAction();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Input, Start Over!!");
                        break;
                    }
            }

        }

    }
}

