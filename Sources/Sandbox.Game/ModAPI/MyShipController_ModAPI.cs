using Sandbox.ModAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sandbox.Engine.Utils;

using VRageMath;
using VRage.Input;
using VRage.Library.Utils;

using IMyShipController = Sandbox.ModAPI.Ingame.IMyShipController;

namespace Sandbox.Game.Entities
{
    partial class MyShipController
    {
        public MyControlsEnum pressedControls = MyControlsEnum.NONE;
        public Vector3 movementInput = Vector3.Zero;
        public Vector3 rotationInput = Vector3.Zero;

        private static Matrix tmpMatrix;

        ModAPI.IMyEntity ModAPI.Interfaces.IMyControllableEntity.Entity
        {
            get { return Entity; }
        }

        void ModAPI.Interfaces.IMyControllableEntity.DrawHud(ModAPI.Interfaces.IMyCameraController camera, long playerId)
        {
            if (camera is IMyCameraController)
                DrawHud(camera as IMyCameraController, playerId);
        }

        Vector3 IMyShipController.GetThrusters()
        {
            return GridThrustSystem == null ? Vector3.Zero : GridThrustSystem.ControlThrust;
        }

        Vector3 IMyShipController.GetThrustersOutput()
        {
            return GridThrustSystem == null ? Vector3.Zero : GridThrustSystem.Thrust;
        }

        bool IMyShipController.SetThrusters(Vector3 direction)
        {
            if (!m_enableShipControl
                || GridPowerDistributor == null
                || GridThrustSystem != null
                || (IsMainCockpit == false && CubeGrid.HasMainCockpit()))
                return false;

            // TODO local coords instead?
            // Vector3.Transform(moveIndicator, orientMatrix);

            GridThrustSystem.ControlThrust = direction;
            //CubeGrid.SyncObject.SendControlThrust(GridThrustSystem.ControlThrust); // not sure if required
            return true;
        }

        Vector3 IMyShipController.GetGyros()
        {
            return GridGyroSystem == null ? Vector3.Zero : GridGyroSystem.ControlTorque;
        }

        Vector3 IMyShipController.GetGyrosOutput()
        {
            return GridGyroSystem == null ? Vector3.Zero : GridGyroSystem.Torque;
        }

        bool IMyShipController.SetGyros(Vector3 direction)
        {
            if (!m_enableShipControl
                || GridPowerDistributor == null
                || GridGyroSystem != null
                || (IsMainCockpit == false && CubeGrid.HasMainCockpit()))
                return false;

            return true;
        }

        Vector3 IMyShipController.GetWheels()
        {
            return GridWheels == null ? Vector3.Zero : GridWheels.AngularVelocity;
        }

        bool IMyShipController.SetWheels(Vector3 direction)
        {
            if (!m_enableShipControl
                || GridPowerDistributor == null
                || GridWheels != null
                || (IsMainCockpit == false && CubeGrid.HasMainCockpit())
                || !ControlWheels)
                return false;

            if (MyFakes.ENABLE_WHEEL_CONTROLS_IN_COCKPIT && GridWheels != null && ControlWheels)
            {
                Orientation.GetMatrix(out tmpMatrix);
                GridWheels.AngularVelocity = direction;
                GridWheels.CockpitMatrix = tmpMatrix;
            }

            return true;
        }

        Vector3 IMyShipController.GetMovementInput
        {
            get { return movementInput; }
        }

        Vector3 IMyShipController.GetRotationInput
        {
            get { return rotationInput; }
        }

        //bool IMyShipController.IsControlPressed(MyStringId controlId)
        //{
        //    return pressedControls.Contains(controlId);
        //}

        MyControlsEnum IMyShipController.GetPressedControls()
        {
            return pressedControls;
        }
    }
}
