using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Student : Person
    {

        public Parent Parent { get; set; }

        public virtual ICollection<ClassStudent> ClassStudents { get; set; }

        
    }
}
