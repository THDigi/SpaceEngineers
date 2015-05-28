using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sandbox.ModAPI.Interfaces;
using VRageMath;
using VRage.Library.Utils;

namespace Sandbox.ModAPI.Ingame
{
    public interface IMyShipController : IMyTerminalBlock
    {
        /// <summary>
        /// Indicates whether a block is locally or remotely controlled.
        /// </summary>
        bool IsUnderControl { get; }
        bool ControlWheels { get; }
        bool ControlThrusters { get; }
        bool HandBrake { get; }
        bool DampenersOverride { get; }

        Vector3 GetThrusters();
        Vector3 GetThrustersOutput();
        bool SetThrusters(Vector3 direction);

        Vector3 GetGyros();
        Vector3 GetGyrosOutput();
        bool SetGyros(Vector3 direction);
        
        Vector3 GetWheels();
        bool SetWheels(Vector3 direction);

        Vector3 GetMovementInput { get; }
        Vector3 GetRotationInput { get; }

        MyControlsEnum GetPressedControls();
    }
}
