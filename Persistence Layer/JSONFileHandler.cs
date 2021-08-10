
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model.DTO;
using Newtonsoft.Json;
using PersistenceLayer.Interfaces;

namespace PersistenceLayer.JSONHandler
{
    public class JSONFileHandler : IActions
    {
        string filePath = @"C:\D\Work\Dotnet\Staff Management\DataBase\DataStore.json";

        public void AddStaff<T>(T admin)
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            Staff newStaff = new Staff {
                Id= (int)typeof(T).GetProperty("Experience").GetValue(admin),
                UserName = (string) typeof(T).GetProperty("UserName").GetValue(admin),
                Password = (string) typeof(T).GetProperty("Password").GetValue(admin),
                Experience = (int)typeof(T).GetProperty("Experience").GetValue(admin),
                DateOfJoining = (DateTime)typeof(T).GetProperty("DateOfJoining").GetValue(admin),
                PhoneNumber = (string)typeof(T).GetProperty("PhoneNumber").GetValue(admin),
                Subject = (string)typeof(T).GetProperty("Subject").GetValue(admin),
                Type = (string)typeof(T).GetProperty("Type").GetValue(admin)
            };
            staffs.Add(newStaff);
            string jsonResult = JsonConvert.SerializeObject(staffs);
            File.WriteAllText(filePath, jsonResult);
        }

        public void DeleteStaff(int id)
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            var res = staffs.FirstOrDefault(x => x.Id == id);
            if (res != null)
            {
                staffs.Remove(res);
                string jsonResult = JsonConvert.SerializeObject(staffs);
                File.WriteAllText(filePath, jsonResult);
            }

        }

        public void EditStaff(int id, StaffUpdateDTO staffDTO)
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            var res = staffs.FirstOrDefault(x => x.Id == id);
            if (res != null)
            {
                res.UserName = staffDTO.UserName ?? res.UserName;
                res.Password = staffDTO.Password ?? res.Password;
                res.PhoneNumber = staffDTO.PhoneNumber ?? res.PhoneNumber;
                res.Subject = staffDTO.Subject ?? res.Subject;
                res.Type = staffDTO.Type ?? res.Type;
            }
            if(typeof(StaffUpdateDTO).GetProperty("DateOfJoining").GetValue(staffDTO) !=null){
                res.DateOfJoining = staffDTO.DateOfJoining;
            }
            if(typeof(StaffUpdateDTO).GetProperty("Experience").GetValue(staffDTO) !=null){
                res.Experience = staffDTO.Experience;
            }
            string jsonResult = JsonConvert.SerializeObject(staffs);
            File.WriteAllText(filePath, jsonResult);
        }

        public List<Staff> GetAllStaff()
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            return staffs;
        }

        public Staff GetStaff(int id)
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            var res = staffs.FirstOrDefault(x => x.Id == id);
            return res;
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