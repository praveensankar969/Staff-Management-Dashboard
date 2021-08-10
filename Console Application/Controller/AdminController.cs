using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using PersistenceLayer.DTO;
using System;

namespace ConsoleApplication.Controller
{
    public class AdminControl : Staff
    {
        string filePath = @"C:\D\Work\Dotnet\Staff Management\DataBase\DataStore.json";
        public void GetAllStaff()
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json); ;
            Console.WriteLine("Details are : ");
            for (int i = 0; i < staffs.Count; i++)
            {
                Console.WriteLine("Name: " + staffs[i].UserName + " \tDate of Joining: " + staffs[i].DateOfJoining + " \tExperience: " + staffs[i].Experience + " \tSubject: " + staffs[i].Subject + " \tPhone: " + staffs[i].PhoneNumber);
            }
        }

        public void GetStaff(int id)
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            var res = staffs.FirstOrDefault(x => x.Id == id);
            Console.WriteLine("Details of Staff " + id + " are:");
            Console.WriteLine("Name: " + res.UserName + " \tDate of Joining: " + res.DateOfJoining + " \tExperience: " + res.Experience + " \tSubject: " + res.Subject + " \tPhone: " + res.PhoneNumber);
        }

        public void AddStaff(Staff staff)
        {

            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            staffs.Add(staff);
            string jsonResult = JsonConvert.SerializeObject(staffs);
            File.WriteAllText(filePath, jsonResult);

        }

        public void EditStaff(int id, StaffUpdateDTO staffDTO)
        {

            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            var res = staffs.FirstOrDefault(x => x.Id == id);
            if (res != null)
            {
                res.UserName = staffDTO.UserName ?? res.UserName;
                res.DateOfJoining = staffDTO.DateOfJoining ?? res.DateOfJoining;
                res.Password = staffDTO.Password ?? res.Password;
                res.PhoneNumber = staffDTO.PhoneNumber ?? res.PhoneNumber;
                res.Subject = staffDTO.Subject ?? res.Subject;
                res.Type = staffDTO.Type ?? res.Type;
            }
            string jsonResult = JsonConvert.SerializeObject(staffs);
            File.WriteAllText(filePath, jsonResult);


        }
        public void DeleteStaff(int id)
        {

            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            var res = staffs.FirstOrDefault(x => x.Id == id);
            staffs.Remove(res);
            string jsonResult = JsonConvert.SerializeObject(staffs);
            File.WriteAllText(filePath, jsonResult);

        }

        public User Login(LoginDTO login)
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            var res = staffs.FirstOrDefault(x => x.UserName == login.UserName);
            if (res == null)
            {
                return new User { Id = -1, Type = " " };
            }
            else
            {
                if (res.Password == login.Password)
                {
                    return new User { Id = res.Id, Type = res.Type };
                }
                return new User { Id = -1, Type = " " };
            }

        }
    }


}