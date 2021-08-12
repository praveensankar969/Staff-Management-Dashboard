using System.Collections.Generic;
using Model.DTO;

namespace Persistence.Interfaces
{
    public interface IActions
    {
        public void AddStaff<T>(T Staff);
        public List<Staff> GetAllStaff();
        public Staff GetStaff(int id);
        public void EditStaff(int id, Staff staffDTO);
        public void DeleteStaff(int id);
        public User Login(LoginDTO login);
    }
}