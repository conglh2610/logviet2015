using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class ClassScheduleExam
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClassStudyId { get; set; }

        public int ExamType { get; set; }

        public int ScoreMethod { get; set; }

        public DateTime ExamDate { get; set; }

        public int ExamTime { get; set; }

        public Guid ClassRoomId { get; set; }

        public int ExamSize { get; set; }

        public Guid TeacherId { get; set; }

        public Guid TeacherExaminerId { get; set; }
    }
}
