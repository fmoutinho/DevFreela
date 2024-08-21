using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class UserViewModel
    {
        public UserViewModel(string fullName, string email, DateTime birthDate, List<string> skills)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Skills = skills;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public List<string> Skills { get; set; }
    }
}
