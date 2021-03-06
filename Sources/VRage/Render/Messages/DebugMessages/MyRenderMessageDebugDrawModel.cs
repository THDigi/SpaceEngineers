﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRageMath;

namespace VRageRender
{
    public class MyRenderMessageDebugDrawModel : MyRenderMessageBase
    {
        public string Model;
        public MatrixD WorldMatrix;
        public Color Color;
        public bool DepthRead;

        public override MyRenderMessageType MessageClass { get { return MyRenderMessageType.DebugDraw; } }
        public override MyRenderMessageEnum MessageType { get { return MyRenderMessageEnum.DebugDrawModel; } }
    }
}
