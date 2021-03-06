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
            var userName = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            Console.Write("Experience: ");
            int exp;
            bool flag = false;
            var experience = 0;
            do
            {
                flag = true;
                while (!int.TryParse(Console.ReadLine(), out exp))
                {
                    Console.Write("This is an invalid input, please enter a number: ");
                }
                experience = exp;
                if (exp > 30)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    Console.WriteLine("Experience cannot be grater than 30");
                }
            } while (!flag);
            flag = false;

            Console.Write("Phone: ");
            var phoneNumber = "";
            do
            {
                phoneNumber = Console.ReadLine();
                if (phoneNumber.Length == 10)
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Phonenumber should be 10 digits");
                    flag = false;
                }

            } while (!flag);
            flag = false;

            Console.Write("Date of Joining: ");
            var dateOfJoining = DateTime.Now;
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out dateOfJoining))
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Invalid date");
                    flag = false;
                }
            } while (!flag);

            Staff staff = new Staff(userName, password, experience, dateOfJoining, phoneNumber, "", "");

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