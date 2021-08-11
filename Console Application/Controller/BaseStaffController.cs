using System;
using System.Collections.Generic;
using ConsoleApplication.Persistence;
using Model.DTO;
using PersistenceLayer.Interfaces;

namespace Console_Application.Controller
{
    public abstract class BaseStaffController
    {
        public void GetAllStaff()
        {
            Console.WriteLine("Fetching all Data\n");
            Type type = DataLayer.GetClass();
            IActions obj = Activator.CreateInstance(type) as IActions;
            List<Staff> staffs = obj.GetAllStaff();
            Console.WriteLine("Details are : ");
            for (int i = 0; i < staffs.Count; i++)
            {
                Console.WriteLine("Name: " + staffs[i].UserName + " \tDate of Joining: " + staffs[i].DateOfJoining + " \tExperience: " + staffs[i].Experience + " \tSubject: " + staffs[i].Subject + " \tPhone: " + staffs[i].PhoneNumber);
            }
        }

        public void GetStaff()
        {
            Console.Write("Enter Id of staff to view : ");
            int id = Convert.ToInt32(Console.ReadLine());
            Type type = DataLayer.GetClass();
            IActions obj = Activator.CreateInstance(type) as IActions;
            Staff res = obj.GetStaff(id);
            if (res == null)
            {
                Console.WriteLine("no such ID..!");
            }
            else
            {
                Console.WriteLine("Details of Staff " + id + " is: ");
                Console.WriteLine("Id: " + res.Id + "Name: " + res.UserName + " \tDate of Joining: " + res.DateOfJoining + " \tExperience: " + res.Experience + " \tSubject: " + res.Subject + " \tPhone: " + res.PhoneNumber);
            }

        }
        public virtual Staff AddStaff()
        {
            Staff staff = new Staff();
            Console.WriteLine("Enter details of new staff");

            Console.Write("UserName: ");
            staff.UserName = Console.ReadLine();

            Console.Write("Password: ");
            staff.Password = Console.ReadLine();

            Console.Write("Experience: ");
            int exp;
            bool flag = false;
            do
            {
                flag = true;
                while (!int.TryParse(Console.ReadLine(), out exp))
                {
                    Console.Write("This is an invalid input, please enter a number: ");
                }
                try
                {
                    staff.Experience = exp;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    flag = false;

                }
            } while (!flag);
            flag = false;
            
            Console.Write("Phone: ");
            do
            {
                flag = true;
                try
                {
                    staff.PhoneNumber = Console.ReadLine();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    flag = false;

                }
            } while (!flag);
            flag = false;

            Console.Write("Date of Joining: ");
            do
            {
                flag = true;
                try
                {
                    staff.DateOfJoining = DateTime.Parse(Console.ReadLine());

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    flag = false;

                }
            } while (!flag);
            
            return staff;
        }

        public void EditStaff(int id, StaffUpdateDTO staffDTO)
        {
            
            Type type = DataLayer.GetClass();
            IActions obj = Activator.CreateInstance(type) as IActions;
            obj.EditStaff(id, staffDTO);

        }
        public void DeleteStaff(int id)
        {

            Type type = DataLayer.GetClass();
            IActions obj = Activator.CreateInstance(type) as IActions;
            obj.DeleteStaff(id);

        }

        public User Login(LoginDTO login)
        {
            Type type = DataLayer.GetClass();
            IActions obj = Activator.CreateInstance(type) as IActions;
            return obj.Login(login);

        }
    }
}