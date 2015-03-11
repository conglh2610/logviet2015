using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Setting
    {
        public Guid Id { get; set; }

        public float? DefaultGLng { get; set; }

        public float? DefaultGLa { get; set; }

        [DefaultValue(0.0125)]
        public double DefaultRadius { get; set; }

        [DefaultValue(true)]
        public bool IsEnableChat { get; set; }

        [DefaultValue(13)]
        public int DefaultZoom { get; set; }

        [DefaultValue(false)]
        public bool AllowLocationTracking { get; set; }
    }
}
