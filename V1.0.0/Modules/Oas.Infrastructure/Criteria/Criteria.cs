using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public abstract class Criteria
    {
        public Guid? Id { get; set; }
        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }

        public string SortColumn { get; set; }

        public string SortDirection { get; set; }
    }
}
