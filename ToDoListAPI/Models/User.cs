using Microsoft.AspNetCore.Identity;

namespace ToDoListAPI.Models
{
    public class User:IdentityUser
    {
        public ICollection<task>? Tasks { get; set; }
    }
}
