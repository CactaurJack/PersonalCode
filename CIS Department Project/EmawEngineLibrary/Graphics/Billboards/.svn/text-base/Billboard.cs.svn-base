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
    public class Billboard 
    {
        #region Fields

        // A reference back to our parent game
        public Game Game;

        // The texture we want to use with this billboard
        public Texture2D Texture;

        // The position of our billboard in 3D space
        public Vector3 Position;

        // A Vector2 defining the billboard's width and height
        public Vector2 Size;

        // The color to render the billboard's surface
        public Color Color;

        // We'll use a custom billboard effect to render our billboard
        // on the GPU
        Effect effect;

        // A vertex buffer for holding our vertex data  
        VertexBuffer vertexBuffer;

        // An index buffer for holding our primitive data
        IndexBuffer indexBuffer;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public Billboard(Game game, Texture2D texture, Vector3 position, Vector2 size, Color color)
        {
            Game = game;
            Texture = texture;
            Position = position;
            Size = size;
            Color = color;

            // Load our billboard effect.  As every billboard instance will 
            // hold a reference to a billboard effect, clone it so we aren't
            // sharing with them
            Effect billboardEffect = Game.Content.Load<Effect>("Effects/Billboard");
            effect = billboardEffect.Clone();
            
            // Create the four vertices of our billboard
            BillboardVertex[] vertices = new BillboardVertex[4]; 
            for (int i = 0; i < 4; i++)
            {
                // Our shadere will acutally position our
                // corners, so we'll just use the same
                // position for all four vertices
                vertices[i].Position = Position;
                vertices[i].Normal = Vector3.Up;
            }
            vertices[0].TexCoord = new Vector2(0, 0); 
            vertices[1].TexCoord = new Vector2(1, 0);
            vertices[2].TexCoord = new Vector2(1, 1);
            vertices[3].TexCoord = new Vector2(0, 1);

            // Create and populate our vertex buffer using our vertex array
            vertexBuffer = new DynamicVertexBuffer(Game.GraphicsDevice, BillboardVertex.VertexDeclaration, 4, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);

            // Create an array of indices 
            ushort[] indices = new ushort[6];

            // Define our first triangle's corners
            indices[0] = (ushort)(0);
            indices[1] = (ushort)(1);
            indices[2] = (ushort)(2);

            // Define our second triangle's corners
            indices[3] = (ushort)(0);
            indices[4] = (ushort)(2);
            indices[5] = (ushort)(3);
            
            // Create and populate our index buffer with the indices we just created
            indexBuffer = new IndexBuffer(Game.GraphicsDevice, typeof(ushort), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        #endregion

        #region Drawing


        public void Draw(GameTime gameTime)
        {
            // Retrieve the active camera
            ICamera camera = Game.Services.GetService(typeof(ICamera)) as ICamera;

            // Retrieve the graphics device
            GraphicsDevice device = Game.GraphicsDevice;
            Vector3 lightDirection = Vector3.Normalize(new Vector3(3, -1, 1));
            Vector3 lightColor = new Vector3(0.3f, 0.4f, 0.2f);
            
            // Set the parameters for our billboard effect. 
            // We'll render in two passes - the first will render
            // our mostly opaque pixels, the second our transparent
            // pixels around the edges
            effect.Parameters["BillboardWidth"].SetValue(Size.X);
            effect.Parameters["BillboardHeight"].SetValue(Size.Y);
            effect.Parameters["Texture"].SetValue(Texture);
            effect.Parameters["View"].SetValue(camera.View);
            effect.Parameters["Projection"].SetValue(camera.Projection);
            effect.Parameters["Color"].SetValue(Color.ToVector3());
            effect.Parameters["AlphaTestDirection"].SetValue(1f);

            // Set our device states
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.RasterizerState = RasterizerState.CullNone;
            device.SamplerStates[0] = SamplerState.LinearClamp;

            // Render our primitives
            device.SetVertexBuffer(vertexBuffer);
            device.Indices = indexBuffer;
            effect.CurrentTechnique.Passes[0].Apply();
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 6, 0, 2);

            // Set our device state and effect parameters for the 
            // second, "translucent" pass and render again  
            device.BlendState = BlendState.NonPremultiplied;
            device.DepthStencilState = DepthStencilState.DepthRead;
            effect.Parameters["AlphaTestDirection"].SetValue(-1f);
            effect.CurrentTechnique.Passes[0].Apply();
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 6, 0, 2);
        }

        #endregion
    }
}
