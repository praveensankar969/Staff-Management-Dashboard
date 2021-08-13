using System;
using ConsoleApplication.Controller;
using Model.DTO;

namespace ConsoleApplication.View
{
    public class UserCapability
    {
        public void AdminActions()
        {
            AdminControl client = new AdminControl();
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
                            Console.Write("Enter Id of staff to view : ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            client.GetStaff(id);
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
                            Console.WriteLine("Enter Id of staff to edit: ");
                            id = Convert.ToInt32(Console.ReadLine());
                            Staff staffObj = client.GetStaff(id);
                            _EditStaffConsoleView("Enter one property to edit: (UserName , Password , Subject , Experience, Phone, DateOfJoining, Type): ", staffObj);
                            client.EditStaff(id, staffObj);
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter details of staff to delete");
                            Console.Write("Enter Id of staff: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Are you Sure you want to delete this account? (Y/N)");
                            string ree = Console.ReadLine();
                            if (ree.Equals("Y") || ree.Equals("y"))
                            {
                                client.DeleteStaff(id);
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

            } while (_Continue("Move back to Admin Dashboard? (Y/N)"));
        }


        public void StaffAction(User user)
        {
            StaffController client = new StaffController();
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
                            Staff staffObj = client.GetStaff(user.Id);
                            _EditStaffConsoleView("Enter one property to edit: (UserName , Password , Subject , Experience, Phone, DateOfJoining): ", staffObj);
                            client.EditStaff(user.Id, staffObj);
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Are you Sure you want to delete your account? (Y/N)");
                            string ree = Console.ReadLine();
                            if (ree.Equals("Y") || ree.Equals("y"))
                            {
                                client.DeleteStaff(user.Id);
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

            } while (_Continue("Move back to Staff Dashboard? (Y/N)"));


        }

        private static bool _Continue(string editText)
        {

            Console.Write(editText);
            string res = Console.ReadLine();
            return res.ToLower() == "y" ? true : false;
        }

        private static Staff _EditStaffConsoleView(string viewProperty, Staff staff)
        {
            const string ExperienceProperty = "Experience";
            const string DateProperty = "DateOfJoining";
            
            do
            {
                Console.Write(viewProperty);
                var property = Console.ReadLine();
                var propInfo = staff.GetType().GetProperty(property);
                if (propInfo == null)
                {
                    Console.WriteLine("Wrong Property Name");
                    break;
                }
                if (property == ExperienceProperty)
                {
                    _SetPropertyValue(propInfo, staff, typeof(int));

                }
                else if (property == DateProperty)
                {
                    _SetPropertyValue(propInfo, staff, typeof(DateTime));
                }
                else
                {
                    _SetPropertyValue(propInfo, staff, typeof(string));
                }


            } while (_Continue("\nDo you wish to edit another detail of this staff? (y/n) : "));
            

            return staff;
        }

        private static void _SetPropertyValue(System.Reflection.PropertyInfo propInfo, Staff obj, Type type)
        {
            bool flag = true;
            do
            {
                flag = true;
                Console.Write("Enter new value: ");
                try
                {
                    if (type == typeof(string))
                    {
                        var value = Console.ReadLine();
                        propInfo.SetValue(obj, value);
                    }
                    else if (type == typeof(DateTime))
                    {
                        var value = DateTime.Parse(Console.ReadLine());
                        propInfo.SetValue(obj, value);
                    }
                    else if (type == typeof(int))
                    {
                        int exp;
                        while (!int.TryParse(Console.ReadLine(), out exp))
                        {
                            Console.Write("This is an invalid input, please enter a number: ");
                        }
                        propInfo.SetValue(obj, exp);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    flag = false;
                }
            } while (!flag);
        }


    }
}