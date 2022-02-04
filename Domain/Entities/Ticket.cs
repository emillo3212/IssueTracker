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
        public string Priority { get; set; }
        public string Created { get; set; }
        public bool Done { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public int AssignToId { get; set; }
        public User AssignTo { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }


    }
}
