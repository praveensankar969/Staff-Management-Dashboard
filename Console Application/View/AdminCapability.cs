using System;
using ConsoleApplication.Controller;
using ConsoleApplication.Service;
using Model.DTO;

namespace ConsoleApplication.View
{
    public class AdminCapability
    {
        public void AdminActions()
        {
            AdminControl client = new AdminControl();
            int latestId = IdCalculate.ComputeId();
            do
            {
                Console.Clear();
                Console.WriteLine("\t\t Admin Dashboard");
                Console.WriteLine("1. View all Staffs");
                Console.WriteLine("2. View Staff Details");
                Console.WriteLine("3. Add Staff Details");
                Console.WriteLine("4. Edit Staff Details");
                Console.WriteLine("5. Delete Staff");
                Console.WriteLine("6. Exit Application");

                Console.Write("Enter your choice now: ");
                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        {
                            client.GetAllStaff();
                            break;
                        }
                    case 2:
                        {
                            client.GetStaff();
                            break;
                        }
                    case 3:
                        {
                            client.AddStaff();
                            break;
                        }
                    case 4:
                        {
                            int id;
                            bool flag = false;
                            int exp;
                            var staffDTO = new StaffUpdateDTO();
                            Console.WriteLine("Enter Id of staff to edit");
                            id = Convert.ToInt32(Console.ReadLine());
                            do
                            {
                                Console.Write("Enter one property to edit: (UserName , Password , Subject , Experience, Phone, DateOfJoining, Type): ");
                                var property = Console.ReadLine();
                                Console.Write("Enter new value: ");
                                var value = Console.ReadLine();
                                var staffObj = new StaffUpdateDTO();
                                var propInfo = staffObj.GetType().GetProperty(property);
                                if (propInfo == null)
                                {
                                    Console.WriteLine("Wrong PropertyName");
                                    break;
                                }
                                if (property == "Experience")
                                {
                                    do
                                    {
                                        flag = true;
                                        Console.Write("Enter new value: ");
                                        while (!int.TryParse(Console.ReadLine(), out exp))
                                        {
                                            Console.Write("This is an invalid input, please enter a number: ");
                                        }
                                        try
                                        {
                                            propInfo.SetValue(staffObj, exp);

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            flag = false;

                                        }
                                    } while (!flag);

                                }
                                else if(property == "DateOfJoining")
                                {
                                    do
                                    {
                                        flag = true;
                                        Console.Write("Enter new value: ");
                                        
                                        try
                                        {
                                            var val = DateTime.Parse(Console.ReadLine());
                                            propInfo.SetValue(staffObj, value);

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            flag = false;
                                        }
                                    }
                                    while (!flag);
                                }
                                else 
                                {
                                    do
                                    {
                                        flag = true;
                                        Console.Write("Enter new value: ");
                                        value = Console.ReadLine();
                                        try
                                        {
                                            propInfo.SetValue(staffObj, value);

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            flag = false;
                                        }
                                    }
                                    while (!flag);
                                }

                                client.EditStaff(id, staffObj);

                            } while (Continue("edit"));
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter details of staff to delete");
                            Console.Write("Enter Id of staff: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            client.DeleteStaff(id);
                            break;
                        }
                    case 6:
                        {
                            Environment.Exit(0);
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid choice, Retuning to Dashboard");
                            break;
                        }

                }

            } while (Continue());
        }


        public void StaffAction(User user)
        {

            StaffController client = new StaffController();
            int latestId = IdCalculate.ComputeId();
            do
            {
                Console.Clear();
                Console.WriteLine("\t\t Staff Dashboard");
                Console.WriteLine("1. Add New Staff");
                Console.WriteLine("2. Edit Details");
                Console.WriteLine("3. Delete Data");
                Console.WriteLine("4. Exit Application");

                Console.Write("Enter your choice now: ");
                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        {
                            client.AddStaff();
                            break;
                        }
                    case 2:
                        {
                            int id = user.Id;
                            bool flag = false;
                            int exp;
                            var staffDTO = new StaffUpdateDTO();
                            Console.WriteLine("Enter Id of staff to edit");
                            id = Convert.ToInt32(Console.ReadLine());
                            do
                            {
                                Console.Write("Enter one property to edit: (UserName , Password , Subject , Experience, Phone, DateOfJoining): ");
                                var property = Console.ReadLine();
                                Console.Write("Enter new value: ");
                                var value = Console.ReadLine();
                                var staffObj = new StaffUpdateDTO();
                                var propInfo = staffObj.GetType().GetProperty(property);
                                if (propInfo == null)
                                {
                                    Console.WriteLine("Wrong PropertyName");
                                    break;
                                }
                                if (property == "Experience")
                                {
                                    do
                                    {
                                        flag = true;
                                        Console.Write("Enter new value: ");
                                        while (!int.TryParse(Console.ReadLine(), out exp))
                                        {
                                            Console.Write("This is an invalid input, please enter a number: ");
                                        }
                                        try
                                        {
                                            propInfo.SetValue(staffObj, exp);

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            flag = false;

                                        }
                                    } while (!flag);

                                }
                                else if(property == "DateOfJoining")
                                {
                                    do
                                    {
                                        flag = true;
                                        Console.Write("Enter new value: ");
                                        
                                        try
                                        {
                                            var val = DateTime.Parse(Console.ReadLine());
                                            propInfo.SetValue(staffObj, value);

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            flag = false;
                                        }
                                    }
                                    while (!flag);
                                }
                                else
                                {
                                    do
                                    {
                                        flag = true;
                                        Console.Write("Enter new value: ");
                                        value = Console.ReadLine();
                                        try
                                        {
                                            propInfo.SetValue(staffObj, value);

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            flag = false;
                                        }
                                    }
                                    while (!flag);
                                }

                                client.EditStaff(id, staffObj);

                            } while (Continue("edit"));
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Are you Sure you want to delete your account? (Y/N)");
                            string ree = Console.ReadLine();
                            if (ree.Equals("Y") || ree.Equals("y"))
                            {
                                client.DeleteStaff(latestId);
                            }
                            break;
                        }
                    case 6:
                        {
                            Environment.Exit(0);
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Invalid choice, Retuning to Dashboard");
                            break;
                        }

                }

            } while (Continue());


        }

        public bool Continue()
        {

            Console.Write("\nDo you wish to continue operations in this dashboard ? (y/n): ");
            string res = Console.ReadLine();
            return res == "y" || res == "Y" ? true : false;
        }

        public static bool Continue(string edit)
        {

            Console.Write("\nDo you wish to edit another detail of this staff? (y/n) : ");
            string res = Console.ReadLine();
            return res == "y" || res == "Y" ? true : false;
        }


    }
}