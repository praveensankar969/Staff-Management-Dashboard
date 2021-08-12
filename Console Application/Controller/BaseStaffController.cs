using System;
using System.Collections.Generic;
using ConsoleApplication.Persistence;
using Model.DTO;
using Persistence.Interfaces;

namespace Console_Application.Controller
{
    public abstract class BaseStaffController
    {
        public void GetAllStaff()
        {
            Console.WriteLine("Fetching all Data\n");
            Type type = Configuration.GetStorageType();
            IActions obj = Activator.CreateInstance(type) as IActions;
            List<Staff> staffs = obj.GetAllStaff();
            Console.WriteLine("Details are : ");
            for (int i = 0; i < staffs.Count; i++)
            {
                Console.WriteLine("Name: " + staffs[i].UserName + " \tDate of Joining: " + staffs[i].DateOfJoining + " \tExperience: " + staffs[i].Experience + " \tSubject: " + staffs[i].Subject + " \tPhone: " + staffs[i].PhoneNumber);
            }
        }

        public Staff GetStaff(int id)
        {
            Type type = Configuration.GetStorageType();
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

            return res;

        }
        public virtual Staff AddStaff()
        {
            
            Console.WriteLine("Enter details of new staff");

            Console.Write("UserName: ");
            var UserName = Console.ReadLine();

            Console.Write("Password: ");
            var Password = Console.ReadLine();

            Console.Write("Experience: ");
            int exp;
            bool flag = false;
            var Experience = 0;
            do
            {
                flag = true;
                while (!int.TryParse(Console.ReadLine(), out exp))
                {
                    Console.Write("This is an invalid input, please enter a number: ");
                }
                try
                {
                    Experience = exp;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    flag = false;

                }
            } while (!flag);
            flag = false;
            
            Console.Write("Phone: ");
            var PhoneNumber="";
            do
            {
                flag = true;
                try
                {
                    PhoneNumber = Console.ReadLine();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    flag = false;

                }
            } while (!flag);
            flag = false;

            Console.Write("Date of Joining: ");
            var DateOfJoining = DateTime.Now;
            do
            {
                flag = true;
                try
                {
                    DateOfJoining = DateTime.Parse(Console.ReadLine());

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    flag = false;

                }
            } while (!flag);

            Staff staff = new Staff(UserName, Password, Experience, DateOfJoining, PhoneNumber, "", "");
            
            return staff;
        }

        public void EditStaff(int id, Staff staffDTO)
        {
            
            Type type = Configuration.GetStorageType();
            IActions obj = Activator.CreateInstance(type) as IActions;
            obj.EditStaff(id, staffDTO);

        }
        public void DeleteStaff(int id)
        {

            Type type = Configuration.GetStorageType();
            IActions obj = Activator.CreateInstance(type) as IActions;
            obj.DeleteStaff(id);

        }

        public User Login(LoginDTO login)
        {
            Type type = Configuration.GetStorageType();
            IActions obj = Activator.CreateInstance(type) as IActions;
            return obj.Login(login);

        }
    }
}