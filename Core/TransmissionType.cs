using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum TransmissionType
    {
        [Description("Automatyczna")]
        Automatic,
        [Description("Manualna")]
        Manual,
        [Description("Wszystkie")]
        All
    };
}
