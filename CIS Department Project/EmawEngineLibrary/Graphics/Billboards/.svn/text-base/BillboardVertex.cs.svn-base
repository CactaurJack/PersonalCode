#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.Collections.Generic;
using EmawEngineLibrary.Cameras;
#endregion

namespace EmawEngineLibrary.Graphics.Billboards
{
    public struct BillboardVertex
    {
        // Stores the position of the billboard
        public Vector3 Position;

        // Stores the normal, used in lighting calculations
        public Vector3 Normal;

        // Stores the texture coordinates
        public Vector2 TexCoord;


        // Describe the layout of this vertex structure.
        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3,
                                 VertexElementUsage.Position, 0),

            new VertexElement(12, VertexElementFormat.Vector3,
                                  VertexElementUsage.Normal, 0),

            new VertexElement(24, VertexElementFormat.Vector2,
                                  VertexElementUsage.TextureCoordinate, 0)

        );


        // Describe the size of this vertex structure.
        public const int SizeInBytes = 32;
    }
}
