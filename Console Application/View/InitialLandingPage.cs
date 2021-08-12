using System;

namespace ConsoleApplication.View
{

    public static class InitialLandingPage
    {
        public static void LoginMain()
        {

            //initial landing page
            do
            {
                Console.Clear();
                Console.WriteLine("\t\tStaff Management System");
                Console.WriteLine("1. Staff Signin");
                Console.WriteLine("2. Staff Signup");
                Console.Write("Enter your choice now: ");
                int selectedOption = Convert.ToInt32(Console.ReadLine());
                switch (selectedOption)
                {
                    case 1:
                        {
                            Login.Signin();
                            break;
                        }
                    case 2:
                        {
                            Login.Register();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice");
                            break;
                        }
                }
            } while (_Continue());

        }
        private static bool _Continue()
        {

            Console.Write("\nDo you wish to continue to Logon Page? (y/n) : ");
            string res = Console.ReadLine();
            return res.ToLower()=="y" ? true : false;
        }
    }

}