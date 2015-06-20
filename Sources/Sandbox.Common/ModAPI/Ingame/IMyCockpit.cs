using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.ModAPI.Ingame
{
    public interface IMyCockpit : IMyShipController
    {
        /// <summary>
        /// Cockpit's oxygen level, 0 to 1 range.
        /// </summary>
        float OxygenLevel { get; }

        /// <summary>
        /// Cockpit's max oxygen capacity, in O2 units.
        /// </summary>
        float OxygenCapacity { get; }
    }
}
