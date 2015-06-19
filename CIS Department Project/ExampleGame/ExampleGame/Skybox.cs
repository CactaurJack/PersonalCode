using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ExampleGame
{
    //This class was created using the GeneratedGeometry Example from 
    //http://create.msdn.com/en-US/education/catalog/sample/generated_geometry
    //as a refference
    public class Skybox
    {
        public Model Model;
        public Texture2D Texture;

        static readonly SamplerState WrapUClampV = new SamplerState
        {
            AddressU = TextureAddressMode.Wrap,
            AddressV = TextureAddressMode.Clamp,
        };

        public void Draw(GameTime gameTime, Matrix view)
        {
            GraphicsDevice device = Texture.GraphicsDevice;
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, 1, 10000);
          
            device.DepthStencilState = DepthStencilState.DepthRead;
            device.SamplerStates[0] = WrapUClampV;
            device.BlendState = BlendState.Opaque;
            
            view.Translation = Vector3.Zero;

            projection.M13 = projection.M14;
            projection.M23 = projection.M24;
            projection.M33 = projection.M34;
            projection.M43 = projection.M44;

            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.View = view;
                    effect.Projection = projection;
                    effect.Texture = Texture;
                    effect.TextureEnabled = true;
                }
                mesh.Draw();
            }
            device.DepthStencilState = DepthStencilState.Default;
            device.SamplerStates[0] = SamplerState.LinearWrap;
        }
    }
}
