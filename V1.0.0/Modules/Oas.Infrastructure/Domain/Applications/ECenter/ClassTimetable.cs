using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Scheduler
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public Guid RoomId { get; set; }

        public Guid StudyDateId { get; set; }

        public Guid? StudyTimeId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [ForeignKey("StudyTimeId")]
        public StudyTime StudyTime { get; set; }
    }
}
