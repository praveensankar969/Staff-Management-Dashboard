
using System;

namespace Model.DTO
{
    public class Staff
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        private int _experience;
        public int Experience
        {
            get
            {
                return _experience;
            }
            set
            {
                if (value > 30)
                {
                    throw new Exception("Experience cannot be greater than 30");
                }
                else if (value < 0)
                {
                    throw new Exception("Experience cannot be less than 0");
                }
                else
                {
                    _experience = value;
                }
            }
        }
        private DateTime _date;
        public DateTime DateOfJoining
        {
            get
            {
                return _date;
            }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new Exception("Joining Date cannot be in future");
                }
                else
                {
                    _date = value;
                }
            }
        }
        public string PhoneNumber
        {
           get; set;
        }
        public string Subject { get; set; }
        public string Type { get; set; }

    }


}