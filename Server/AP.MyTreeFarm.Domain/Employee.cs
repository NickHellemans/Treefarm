using System.Collections.Generic;

namespace AP.MyTreeFarm.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        
        public string Auth0Id { get; set; }
        public List<TreeTask> Tasks { get; set; }
    }

    
}