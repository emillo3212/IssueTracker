using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<ProjectUser> Projects { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
