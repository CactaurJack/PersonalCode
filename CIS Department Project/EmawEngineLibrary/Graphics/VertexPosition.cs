#region File Description
//-----------------------------------------------------------------------------
// VertexPosition.cs
//
// Computing and Information Sciences @ Kansas State EMAW XNA Game Engine
// Copyright (C) Kansas State Univeristy Computing and Information Sciences. 
// All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Describes a custom vertex format structure that contains position and color information.
    /// </summary>
    /// <remarks>
    /// This struct is added by the EMAW Engine Library, but placed in the 
    /// Xna.Framework.Graphics namespace to locate it with its peers
    /// </remarks>
    [Serializable]
    public struct VertexPosition : IVertexType
    {
        public Vector3 Position;

        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0)
        );

        VertexDeclaration IVertexType.VertexDeclaration { get { return VertexDeclaration; } }
    };
}