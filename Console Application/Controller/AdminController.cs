using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System;
using Model.DTO;
using PersistenceLayer.JSONHandler;
using ConsoleApplication.Persistence;

namespace ConsoleApplication.Controller
{
    public class AdminControl : Staff
    {
       

        
        public void GetAllStaff()
        {
            Type type = DataLayer.GetClass();
            var obj = Activator.CreateInstance(type);
            List<Staff> staffs =(List<Staff>)type.GetMethod("GetAllStaff").Invoke(obj, null);
            Console.WriteLine("Details are : ");
            for (int i = 0; i < staffs.Count; i++)
            {
                Console.WriteLine("Name: " + staffs[i].UserName + " \tDate of Joining: " + staffs[i].DateOfJoining + " \tExperience: " + staffs[i].Experience + " \tSubject: " + staffs[i].Subject + " \tPhone: " + staffs[i].PhoneNumber);
            }
        }

        public void GetStaff(int id)
        {
            Type type = DataLayer.GetClass();
            var obj = Activator.CreateInstance(type);
            Staff res = (Staff)type.GetMethod("GetStaff").Invoke(obj, new object[] {id});
            if(res ==null){
                Console.WriteLine("no such ID");
            }
            else{
                Console.WriteLine("Details of Staff " + id + " are:");
            Console.WriteLine("Name: " + res.UserName + " \tDate of Joining: " + res.DateOfJoining + " \tExperience: " + res.Experience + " \tSubject: " + res.Subject + " \tPhone: " + res.PhoneNumber);
            }
            
        }

        public void AddStaff(Staff staff)
        {
            Type type = DataLayer.GetClass();
            var obj = Activator.CreateInstance(type);
            type.GetMethod("AddStaff").Invoke(obj, new object[] {staff});

        }

        public void EditStaff(int id, StaffUpdateDTO staffDTO)
        {
            Type type = DataLayer.GetClass();
            var obj = Activator.CreateInstance(type);
            type.GetMethod("EditStaff").Invoke(obj, new object[] {id, staffDTO});

        }
        public void DeleteStaff(int id)
        {

            Type type = DataLayer.GetClass();
            var obj = Activator.CreateInstance(type);
            type.GetMethod("DeleteStaff").Invoke(obj, new object[] {id});

        }

        public User Login(LoginDTO login)
        {
            Type type = DataLayer.GetClass();
            var obj = Activator.CreateInstance(type);
            return (User)type.GetMethod("Login").Invoke(obj, new object[] {login});

        }
    }


}