using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sandbox.ModAPI.Ingame;

namespace Sandbox.Game.Entities
{
    public partial class MyCockpit : Sandbox.ModAPI.IMyCockpit
    {
        float IMyCockpit.OxygenLevel
        {
            get { return OxygenLevel; }
        }

        float IMyCockpit.OxygenCapacity
        {
            get { return BlockDefinition.OxygenCapacity; }
        }
    }
}
