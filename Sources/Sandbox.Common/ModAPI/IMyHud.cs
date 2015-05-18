#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sandbox.Common;
using VRageMath;

#endregion

namespace Sandbox.ModAPI
{
    public interface IMyCustomMarker
    {
        string Text { get; set; }

        bool TextShadow { get; set; }

        bool TextOffset { get; set; }

        Vector3D Position { get; set; }

        string Icon { get; set; }

        Color IconColor { get; set; }

        Vector2 IconOffset { get; set; }

        Vector2 IconSize { get; set; }

        bool IgnoreHiddenHud { get; set; }

        IMyEntity MustBeVisible { get; set; }

        string TargetIcon { get; set; }

        float TargetIconScale { get; set; }

        float TargetIconRotateSpeed { get; set; }

        string DirectionIcon { get; set; }

        MyFontEnum Font { get; set; }

        Color HudColor { get; set; }

        bool ShowDistance { get; set; }

        bool AlphaCorrectionByDistance { get; set; }
    }

    public interface IMyHud
    {
        bool IsHidden { get; }

        IMyCustomMarker CreateMarkerInstance();

        long AddMarker(IMyCustomMarker marker);

        void RemoveMarker(long index);

        IMyCustomMarker GetMarker(long index);

        void GetMarkers(Dictionary<long, IMyCustomMarker> list);

        void Draw3DText(string text, Vector3D position);

        void DrawText(StringBuilder text, Vector2 position, float scale, Color? color, bool fullscreen = true, float maxWidth = float.PositiveInfinity);
    }
}
