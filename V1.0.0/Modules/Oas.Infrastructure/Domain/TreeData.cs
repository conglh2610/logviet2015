using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class TreeData
    {

        public Guid id { get; set; }
        public int? ParentId { get; set; }
        public string text { get; set; }
        public List<TreeData> children { get; set; }
    }
}
