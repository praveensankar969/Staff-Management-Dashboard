
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

        public void AddStaff(Staff staff)
        {
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            staffs.Add(staff);
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
                res.DateOfJoining = staffDTO.DateOfJoining ?? res.DateOfJoining;
                res.Password = staffDTO.Password ?? res.Password;
                res.PhoneNumber = staffDTO.PhoneNumber ?? res.PhoneNumber;
                res.Subject = staffDTO.Subject ?? res.Subject;
                res.Type = staffDTO.Type ?? res.Type;
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