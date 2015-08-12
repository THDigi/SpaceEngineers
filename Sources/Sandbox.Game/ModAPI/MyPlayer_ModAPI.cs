using Sandbox.ModAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMyControllableEntity = Sandbox.ModAPI.Interfaces.IMyControllableEntity;
using IMyGameControllableEntity = Sandbox.Game.Entities.IMyControllableEntity;
using VRageMath;

namespace Sandbox.Game.World
{
    partial class MyPlayer : IMyPlayer
    {
        IMyNetworkClient IMyPlayer.Client
        {
            get { return Client; }
        }

        Common.MyRelationsBetweenPlayerAndBlock IMyPlayer.GetRelationTo(long playerId)
        {
            return GetRelationTo(playerId);
        }

        HashSet<long> IMyPlayer.Grids 
        { 
            get { return Grids; } 
        }

        void IMyPlayer.RemoveGrid(long gridEntityId)
        {
            RemoveGrid(gridEntityId);
        }

        void IMyPlayer.AddGrid(long gridEntityId)
        {
            AddGrid(gridEntityId);
        }

        IMyEntityController IMyPlayer.Controller 
        { 
            get { return Controller;}
        }


        string IMyPlayer.DisplayName
        {
            get { return DisplayName; }
        }

        ulong IMyPlayer.SteamUserId
        {
            get { return this.Id.SteamId; }
        }


        VRageMath.Vector3 IMyPlayer.GetPosition()
        {
            return GetPosition();
        }

        // Warning: this is obsolete!
        long IMyPlayer.PlayerID
        {
            get { return Identity.IdentityId; }
        }

        long IMyPlayer.IdentityId
        {
            get { return Identity.IdentityId; }
        }

        int IMyPlayer.SelectedColorSlot
        {
            get { return SelectedBuildColorSlot; }
        }

        Vector3 IMyPlayer.SelectedColor
        {
            get { return SelectedBuildColor; }
        }

        Vector3 IMyPlayer.GetColorFromList(int slot)
        {
            if (m_buildColorHSVSlots.Count >= slot)
                throw new ArgumentOutOfRangeException("The slot must be smaller or equal to " + (m_buildColorHSVSlots.Count - 1) + "!");

            return m_buildColorHSVSlots[slot];
        }

        void IMyPlayer.GetColorList(List<Vector3> colors)
        {
            foreach(var color in m_buildColorHSVSlots)
            {
                colors.Add(color);
            }
        }

        void IMyPlayer.GetDefaultColorList(List<Vector3> colors)
        {
            foreach (var color in m_buildColorDefaults)
            {
                colors.Add(color);
            }
        }
    }
}
