using DevFreela.API.Entities;
using System.Security.Claims;

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

        public static UserViewModel FromEntity(User user)
        {
            var skills = user.Skills.Select(s => s.Skill.Description).ToList();
            return new UserViewModel(user.FullName, user.Email, user.BirthDate, skills);
        }
    }
}
