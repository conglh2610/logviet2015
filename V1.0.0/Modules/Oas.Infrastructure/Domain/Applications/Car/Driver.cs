﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Driver
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string DriverLevel { get; set; }

        public string DriverLicense { get; set; }
    }
}
