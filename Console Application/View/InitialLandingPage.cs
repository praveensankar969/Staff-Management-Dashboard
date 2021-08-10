using System;

namespace ConsoleApplication.View
{

    public class InitialLandingPage
    {
        public void LogonMain()
        {
            
            //initial landing page
            do{
                Console.Clear();
            Console.WriteLine("\t\tStaff Management System");
            Console.WriteLine("1. Staff Logon");
            Console.WriteLine("2. Staff Signup");
            Console.Write("Enter your choice now: ");
            int selectedOption = Convert.ToInt32(Console.ReadLine());
            Logon.LogonScreen(selectedOption);
            }while(Continue());

        }
        public bool Continue(){

            Console.Write("\nDo you wish to continue to Logon Page? (y/n) : ");
            string res = Console.ReadLine();
            return res=="y" || res=="Y" ? true:false;
        }
    }

}