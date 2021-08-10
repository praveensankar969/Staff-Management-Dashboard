namespace PersistenceLayer.Interfaces
{
    public interface Actions
    {
        public void AddStaff(Staff Staff);
        public void GetAllStaff();
        public void GetStaff(int id);
        public void EditStaff(int id, StaffUpdateDTO staffDTO);
        public void DeleteStaff(int id);
    }
}