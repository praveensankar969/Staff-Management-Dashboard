using System;
using ConsoleApplication.Controller;
using ConsoleApplication.Service;
using Model.DTO;

namespace ConsoleApplication.View
{
    public class AdminCapability
    {
        public void AdminActions(User user)
        {
            AdminControl client = new AdminControl();
            StaffController controller = new StaffController();
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
                            Console.WriteLine("Fetching all Data\n");
                            client.GetAllStaff();
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter Id of staff to view : ");
                            string id = Console.ReadLine();
                            client.GetStaff(Convert.ToInt32(id));
                            break;
                        }
                    case 3:
                        {
                            
                                Console.WriteLine("Enter details of staff");
                                var staff = new Staff();
                                staff.Id = latestId;

                                Console.Write("UserName: ");
                                staff.UserName = Console.ReadLine();

                                Console.Write("Password: ");
                                staff.Password = Console.ReadLine();

                                Console.Write("Subject: ");
                                staff.Subject = Console.ReadLine();

                                Console.Write("Experience: ");
                                staff.Experience = Convert.ToInt32(Console.ReadLine());

                                Console.Write("Phone: ");
                                staff.PhoneNumber = Console.ReadLine();

                                Console.Write("Date of Joining: ");
                                staff.DateOfJoining = Console.ReadLine();

                                staff.Type = "Teaching Staff";

                                client.AddStaff(staff);
                            
                            break;
                        }
                    case 4:
                        {
                            int id = user.Id;
                            var staffDTO = new StaffUpdateDTO(); 
                            Console.WriteLine("Enter Id of staff to edit");
                            id = Convert.ToInt32(Console.ReadLine());      
                            Console.WriteLine("Enter property name and new value");
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
                                propInfo.SetValue(staffObj, value);

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


        public void StaffAction()
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
                            Console.WriteLine("Enter details of staff");
                            var staff = new Staff();
                            staff.Id = latestId;

                            Console.Write("UserName: ");
                            staff.UserName = Console.ReadLine();

                            Console.Write("Password: ");
                            staff.Password = Console.ReadLine();

                            Console.Write("Subject: ");
                            staff.Subject = Console.ReadLine();

                            Console.Write("Experience: ");
                            staff.Experience = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Phone: ");
                            staff.PhoneNumber = Console.ReadLine();

                            Console.Write("Date of Joining: ");
                            staff.DateOfJoining = Console.ReadLine();

                            staff.Type = "Teaching Staff";

                            client.AddStaff(staff);
                            break;
                        }
                    case 2:
                        {
                            var staffDTO = new StaffUpdateDTO();
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
                                propInfo.SetValue(staffObj, value);
                                client.EditStaff(latestId, staffObj);

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