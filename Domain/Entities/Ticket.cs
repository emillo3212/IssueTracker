using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime Created { get; set; }
        public bool Done { get; set; }

        public int AssignToId { get; set; }
        public User AssignTo { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }


    }
}
