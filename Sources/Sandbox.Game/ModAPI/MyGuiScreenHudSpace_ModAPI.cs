#region Using

using System;
using System.Text;
using System.Collections.Generic;
using Sandbox.Common;
using Sandbox.Engine.Utils;
using Sandbox.Game.World;
using System.Diagnostics;

using Sandbox.ModAPI;
using Sandbox.Game.Entities;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using VRage;
using VRage.Utils;
using VRageMath;

using MyMarkerStyle = Sandbox.Game.GUI.HudViewers.MyHudMarkerRender.MyMarkerStyle;

#endregion

namespace Sandbox.Game.Gui
{
    public class MyCustomMarker : IMyCustomMarker
    {
        public string Text;
        public bool TextShadow;
        public bool TextOffset;
        public Vector3D Position;
        public Color IconColor;
        public Vector2 IconOffset;
        public Vector2 IconSize;
        public bool IgnoreHiddenHud;
        public IMyEntity MustBeVisible;

        public MyHudTexturesEnum? Icon;
        public MyMarkerStyle Style = new MyMarkerStyle(MyFontEnum.White, MyHudTexturesEnum.DirectionIndicator, MyHudTexturesEnum.Target_neutral, Color.White);
        public MyHudIndicatorFlagsEnum Flags = MyHudIndicatorFlagsEnum.SHOW_TEXT | MyHudIndicatorFlagsEnum.SHOW_ICON;

        string IMyCustomMarker.Text
        {
            get { return Text; }
            set { Text = value; }
        }

        bool IMyCustomMarker.TextShadow
        {
            get { return TextShadow; }
            set { TextShadow = value; }
        }

        bool IMyCustomMarker.TextOffset
        {
            get { return TextOffset; }
            set { TextOffset = value; }
        }

        Vector3D IMyCustomMarker.Position
        {
            get { return Position; }
            set { Position = value; }
        }

        Color IMyCustomMarker.IconColor
        {
            get { return IconColor; }
            set { IconColor = value; }
        }

        Vector2 IMyCustomMarker.IconOffset
        {
            get { return IconOffset; }
            set { IconOffset = value; }
        }

        Vector2 IMyCustomMarker.IconSize
        {
            get { return IconSize; }
            set { IconSize = value; }
        }

        bool IMyCustomMarker.IgnoreHiddenHud
        {
            get { return IgnoreHiddenHud; }
            set { IgnoreHiddenHud = value; }
        }

        IMyEntity IMyCustomMarker.MustBeVisible
        {
            get { return MustBeVisible; }
            set { MustBeVisible = value; }
        }

        string IMyCustomMarker.Icon
        {
            get { return Icon.HasValue ? Icon.Value.ToString() : null; }
            set
            {
                if (value != null)
                {
                    MyHudTexturesEnum tmp;
                    if (Enum.TryParse<MyHudTexturesEnum>(value, true, out tmp))
                        Icon = tmp;
                }
                else
                    Icon = null;
            }
        }

        string IMyCustomMarker.TargetIcon
        {
            get { return (Flags.HasFlag(MyHudIndicatorFlagsEnum.SHOW_FOCUS_MARK) ? Style.TextureDirectionIndicator.ToString() : null); }
            set
            {
                if (value != null)
                {
                    MyHudTexturesEnum tmp;
                    if (Enum.TryParse<MyHudTexturesEnum>(value, true, out tmp))
                    {
                        Flags |= MyHudIndicatorFlagsEnum.SHOW_FOCUS_MARK;
                        Style.TextureTarget = tmp;
                    }
                }
                else
                    Flags &= ~MyHudIndicatorFlagsEnum.SHOW_FOCUS_MARK;
            }
        }

        float IMyCustomMarker.TargetIconScale
        {
            get { return Style.TextureTargetScale; }
            set { Style.TextureTargetScale = value; }
        }

        float IMyCustomMarker.TargetIconRotateSpeed
        {
            get { return Style.TextureTargetRotationSpeed; }
            set { Style.TextureTargetRotationSpeed = value; }
        }

        string IMyCustomMarker.DirectionIcon
        {
            get { return (Flags.HasFlag(MyHudIndicatorFlagsEnum.SHOW_BORDER_INDICATORS) ? Style.TextureDirectionIndicator.ToString() : null); }
            set
            {
                if (value != null)
                {
                    MyHudTexturesEnum tmp;
                    if (Enum.TryParse<MyHudTexturesEnum>(value, true, out tmp))
                    {
                        Flags |= MyHudIndicatorFlagsEnum.SHOW_BORDER_INDICATORS;
                        Style.TextureDirectionIndicator = tmp;
                    }
                }
                else
                    Flags &= ~MyHudIndicatorFlagsEnum.SHOW_BORDER_INDICATORS;
            }
        }

        MyFontEnum IMyCustomMarker.Font
        {
            get { return Style.Font; }
            set { Style.Font = value; }
        }

        Color IMyCustomMarker.HudColor
        {
            get { return Style.Color; }
            set { Style.Color = value; }
        }

        bool IMyCustomMarker.ShowDistance
        {
            get { return Flags.HasFlag(MyHudIndicatorFlagsEnum.SHOW_DISTANCE); }
            set
            {
                if (value)
                    Flags |= MyHudIndicatorFlagsEnum.SHOW_DISTANCE;
                else
                    Flags &= ~MyHudIndicatorFlagsEnum.SHOW_DISTANCE;
            }
        }

        bool IMyCustomMarker.AlphaCorrectionByDistance
        {
            get { return Flags.HasFlag(MyHudIndicatorFlagsEnum.ALPHA_CORRECTION_BY_DISTANCE); }
            set
            {
                if (value)
                    Flags |= MyHudIndicatorFlagsEnum.ALPHA_CORRECTION_BY_DISTANCE;
                else
                    Flags &= ~MyHudIndicatorFlagsEnum.ALPHA_CORRECTION_BY_DISTANCE;
            }
        }
    }

    public partial class MyGuiScreenHudSpace : IMyHud
    {
        public static MyGuiScreenHudSpace Static { get; private set; }

        bool IMyHud.IsHidden
        {
            get { return MyHud.MinimalHud; }
        }

        Dictionary<long, MyCustomMarker> customMarkers = new Dictionary<long, MyCustomMarker>();
        long index = 0;

        IMyCustomMarker IMyHud.CreateMarkerInstance()
        {
            return new MyCustomMarker();
        }

        long IMyHud.AddMarker(IMyCustomMarker marker)
        {
            index++;
            customMarkers.Add(index, marker as MyCustomMarker);
            return index;
        }

        void IMyHud.RemoveMarker(long index)
        {
            customMarkers.Remove(index);
        }

        IMyCustomMarker IMyHud.GetMarker(long index)
        {
            return customMarkers.GetValueOrDefault(index, null);
        }

        void IMyHud.GetMarkers(Dictionary<long, IMyCustomMarker> output)
        {
            // TODO this
        }

        void IMyHud.Draw3DText(string text, Vector3D position)
        {
            VRageRender.MyRenderProxy.DebugDrawText3D(position, text, Color.SkyBlue, 1.0f, true, VRage.Utils.MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER);
        }

        void IMyHud.DrawText(StringBuilder text, Vector2 position, float scale, Color? color, bool fullscreen, float maxWidth)
        {
            MyGuiManager.DrawString(MyFontEnum.White, text, position, scale, color, MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER, fullscreen, maxWidth);
        }

        private void DrawCustomMarkers()
        {
            DrawTexts(); // in case there's any buffered text

            foreach (var marker in customMarkers.Values)
            {
                if (!marker.IgnoreHiddenHud && MyHud.MinimalHud)
                    continue;

                if (marker.MustBeVisible != null)
                {
                    LineD raycast = new LineD(MySector.MainCamera.Position, (Vector3)marker.MustBeVisible.PositionComp.WorldVolume.Center);
                    raycast.From += raycast.Direction;
                    var result = MyEntities.GetIntersectionWithLine(ref raycast, marker.MustBeVisible as MyEntity, MySession.ControlledEntity as MyEntity);
                    if (result.HasValue && !(result.Value.Entity == marker.MustBeVisible ||
                                             result.Value.Entity.Parent == marker.MustBeVisible ||
                                             result.Value.Entity == marker.MustBeVisible.Parent))
                        continue;
                }

                m_tmpHudEntityParams.FlagsEnum = marker.Flags;
                m_tmpHudEntityParams.OffsetText = marker.TextOffset;
                m_tmpHudEntityParams.Icon = marker.Icon;
                m_tmpHudEntityParams.IconSize = marker.IconSize;
                m_tmpHudEntityParams.IconColor = marker.IconColor;
                m_tmpHudEntityParams.IconOffset = marker.IconOffset;
                m_tmpHudEntityParams.Text.Clear().Append(marker.Text); // reuse a single instance to reduce overhead
                m_markerRender.DrawLocationMarker(
                    marker.Style,
                    marker.Position,
                    m_tmpHudEntityParams,
                    0, 0);

                if (m_texts.GetAllocatedCount() > 0)
                {
                    MyHudText text = m_texts.GetAllocatedItem(0);

                    var font = text.Font;
                    text.Position /= MyGuiManager.GetHudSize();
                    var normalizedCoord = ConvertHudToNormalizedGuiPosition(ref text.Position);

                    if (marker.TextShadow)
                    {
                        Vector2 textSize = MyGuiManager.MeasureString(font, text.GetStringBuilder(), MyGuiSandbox.GetDefaultTextScaleWithLanguage());
                        textSize.X *= 0.9f;
                        textSize.Y *= 0.7f;
                        MyGuiScreenHudBase.DrawFog(ref normalizedCoord, ref textSize);
                    }

                    MyGuiManager.DrawString(font, text.GetStringBuilder(), normalizedCoord, text.Scale, colorMask: text.Color, drawAlign: MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER);

                    m_texts.ClearAllAllocated();
                }
            }
        }
    }
}