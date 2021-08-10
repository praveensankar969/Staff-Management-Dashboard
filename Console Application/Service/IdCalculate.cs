using System.Collections.Generic;
using System.IO;
using Model.DTO;
using Newtonsoft.Json;

namespace ConsoleApplication.Service
{
    public static class IdCalculate
    {
        public static int ComputeId(){
            string filePath = @"C:\D\Work\Dotnet\Staff Management\DataBase\DataStore.json";
            var json = File.ReadAllText(filePath);
            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(json);
            return staffs.Count + 1;
        }
    }
}