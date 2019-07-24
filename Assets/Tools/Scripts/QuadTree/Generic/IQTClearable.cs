using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    public interface IQTClearable
    {
        bool isEnable { get; }
        bool @static { get; }
    }
}
