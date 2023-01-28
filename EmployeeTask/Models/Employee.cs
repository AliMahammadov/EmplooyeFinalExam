using EmployeeTask.Models.BaseEntity;

namespace EmployeeTask.Models
{
    public class Employee:Base
    {
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string About { get; set; }
        public string Tvitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }

    }
}
