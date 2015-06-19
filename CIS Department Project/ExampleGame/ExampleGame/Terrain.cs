using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Cameras;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleGame
{
    //This class was designed using the terrain tutorials at
    //http://www.riemers.net/eng/Tutorials/XNA/Csharp/Series1/Terrain_from_file.php
    //as a reference.
    class Terrain : DrawableGameComponent, ITerrain
    {
        public Terrain(Game game, String mapType) : base(game)
        {
            Map = mapType;
            DrawOrder = 0;
            m_level = 5f; 
        }

        public override void Initialize()
        {
            setMap(Map);

            LoadHeightMap(heightMap);

            xoffset = width / 2.0f;
            zoffset = height / 2.0f;
           
            world = Matrix.CreateTranslation(-xoffset, 0, zoffset);
            CreateBuffers(vertices, indices);

            Game.Services.AddService(typeof(ITerrain), this);

            base.Initialize();
        }

        protected override void UnloadContent()
        {
            Game.Services.RemoveService(typeof(ITerrain));

            base.UnloadContent();
        }

        private void LoadHeightMap(Texture2D heightMap)
        {
            width = heightMap.Width;
            height = heightMap.Height;

            m_height = new float[width][];

            Color[] heightMapColor = new Color[width * height];
            heightMap.GetData(heightMapColor);

            for (int i = 0; i < width; i++)
            {
                m_height[i] = new float[height];
                for (int j = 0; j < height; j++)
                {
                    m_height[i][j] = heightMapColor[i + (j * width)].R / m_level;

                }
            }


            vertices = new VertexPositionNormalTexture[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    vertices[x + y * width].Position = new Vector3(x, m_height[x][y], -y);
                    vertices[x + y * width].TextureCoordinate.X = (float) x / width;
                    vertices[x + y * width].TextureCoordinate.Y = (float) y / height;
                    vertices[x + y * width].Normal = Vector3.Zero;
                }
            }


            indices = new int[(width - 1) * (height - 1) * 6];
            int counter = 0;
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    int lowerLeft = x + y * width;
                    int lowerRight = (x + 1) + y * width;
                    int topLeft = x + (y + 1) * width;
                    int topRight = (x + 1) + (y + 1) * width;

                    indices[counter++] = topLeft;
                    indices[counter++] = lowerRight;
                    indices[counter++] = lowerLeft;

                    indices[counter++] = topLeft;
                    indices[counter++] = topRight;
                    indices[counter++] = lowerRight;
                }
            }

            for (int i = 0; i < indices.Length / 3; i++)
            {
                int index1 = indices[i * 3];
                int index2 = indices[i * 3 + 1];
                int index3 = indices[i * 3 + 2];

                Vector3 side1 = vertices[index1].Position - vertices[index3].Position;
                Vector3 side2 = vertices[index1].Position - vertices[index2].Position;
                Vector3 normal = Vector3.Cross(side1, side2);

                vertices[index1].Normal += normal;
                vertices[index2].Normal += normal;
                vertices[index3].Normal += normal;
            }

            //Make sure each of our normals are actually normals.
            for (int i = 0; i < vertices.Length; i++ )
            {
                vertices[i].Normal.Normalize();
            }
        }
    

        private void CreateBuffers(VertexPositionNormalTexture[] vertices, int[] indices)
        {
            terrainVertexBuffer = new VertexBuffer(Game.GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            terrainVertexBuffer.SetData(vertices);

            terrainIndexBuffer = new IndexBuffer(Game.GraphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);
            terrainIndexBuffer.SetData(indices);
        }

        public override void Draw(GameTime gameTime)
        {
            if (camera == null)
                camera = (ICamera)Game.Services.GetService(typeof(ICamera));
            if (camera != null)
            {
                GraphicsDevice.BlendState = BlendState.Opaque;
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                //Matrix worldMatrix = Matrix.CreateTranslation(-width / 2.0f, 0, height / 2.0f);
                effect.Parameters["xView"].SetValue(camera.View);
                effect.Parameters["xProjection"].SetValue(camera.Projection);
                effect.Parameters["xWorld"].SetValue(world);
                effect.Parameters["xTexture"].SetValue(terrainTexture);
                effect.CurrentTechnique = effect.Techniques["Textured"];


                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.Indices = terrainIndexBuffer;
                    GraphicsDevice.SetVertexBuffer(terrainVertexBuffer);

                    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0,
                                                         indices.Length / 3);
                    //GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3, VertexPositionColor.VertexDeclaration);

                }
            }
            base.Draw(gameTime);
        }

        public float GetHeight(float x, float z)
        {
            //Code from TankOnAHeightMap example, modified to provide for single purpose functions.
            float xCoord = x + xoffset;
            float zCoord = height - z - zoffset;

            int left, top;
            left = (int)xCoord;
            top = (int)zCoord;

            float topHeight;
            float bottomHeight;
            try
            {
                topHeight = MathHelper.Lerp(m_height[left][top],
                                            m_height[left + 1][top],
                                            xCoord - left);

                bottomHeight = MathHelper.Lerp(m_height[left][top + 1],
                                               m_height[left + 1][top + 1],
                                               xCoord - left);
            }
            catch
            {
                return 0;
            }

            return MathHelper.Lerp(topHeight, bottomHeight, zCoord - top);
            //End code from example.
        }



        public Vector3 GetNormal(float x, float z)
        {
            float xCoord = x + xoffset;
            float zCoord = height - z - zoffset;

            int left, top;
            left = (int)xCoord;
            top = (int)zCoord;

            Vector3 topNormal;
            Vector3 bottomNormal;

            try
            {
                topNormal = Vector3.Lerp(vertices[left + (top * height)].Normal,
                                         vertices[left + 1 + (top * height)].Normal,
                                         xCoord - left);

                bottomNormal = Vector3.Lerp(vertices[left + ((top + 1) * height)].Normal,
                                            vertices[left + 1 + ((top + 1) * height)].Normal,
                                            xCoord - left);
            }
            catch
            {
                return Vector3.Up;
            }

            Vector3 normal = Vector3.Lerp(topNormal, bottomNormal, zCoord - top);
            normal.Normalize();
            return normal;
        }

        public void setMap(String Map)
        {
            effect = Game.Content.Load<Effect>("Terrain/" + Map + "/terrainEffect");
            heightMap = Game.Content.Load<Texture2D>("Terrain/" + Map + "/heightmap");
            lightMap = Game.Content.Load<Texture2D>("Terrain/" + Map + "/lightmap");
            normals = Game.Content.Load<Texture2D>("Terrain/" + Map + "/terrainNormals");
            terrainTexture = Game.Content.Load<Texture2D>("Terrain/" + Map + "/terrainTexture");
        }

        private int height;
        private int width;
        public int GetWidth() { return width; }

        private float[][] m_height;
        private int[] indices;
        private VertexPositionNormalTexture[] vertices;

        private Effect effect;
        private float xoffset;
        private float zoffset;
        private Matrix world;
        private ICamera camera;
        private VertexBuffer terrainVertexBuffer;
        private IndexBuffer terrainIndexBuffer;
        private Texture2D terrainTexture;
        private Texture2D normals;
        private Texture2D lightMap;
        private Texture2D heightMap;
        private String Map;
        private float m_level;
    }
}
