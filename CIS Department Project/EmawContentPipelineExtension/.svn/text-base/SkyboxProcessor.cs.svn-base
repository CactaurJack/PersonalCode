using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace EmawContentPipelineExtension
{
    //This class was created using the GeneratedGeometry Example from 
    //http://create.msdn.com/en-US/education/catalog/sample/generated_geometry
    //as a refference
    [ContentProcessor]
    public class SkyboxProcessor : ContentProcessor<Texture2DContent, SkyboxContent>
    {
        const float cylinderSize = 100;

        const int cylinderSegments = 32;

        const float texCoordTop = 0.1f;
        const float texCoordBottom = 0.9f;

        public override SkyboxContent Process(Texture2DContent input, ContentProcessorContext context)
        {
            MeshBuilder builder = MeshBuilder.StartMesh("Skybox/sky");

            List<int> topVertices = new List<int>();
            List<int> bottomVertices = new List<int>();

            for (int i = 0; i < cylinderSegments; i++)
            {
                float angle = MathHelper.TwoPi * i / cylinderSegments;

                float x = (float)Math.Cos(angle) * cylinderSize;
                float z = (float)Math.Sin(angle) * cylinderSize;

                topVertices.Add(builder.CreatePosition(x, cylinderSize, z));
                bottomVertices.Add(builder.CreatePosition(x, -cylinderSize, z));
            }

            int topCenterVertex = builder.CreatePosition(0, cylinderSize * 2, 0);
            int bottomCenterVertex = builder.CreatePosition(0, -cylinderSize * 2, 0);

            int texCoordId = builder.CreateVertexChannel<Vector2>(VertexChannelNames.TextureCoordinate(0));

            builder.SetMaterial(new BasicMaterialContent());

            for (int i = 0; i < cylinderSegments; i++)
            {
                int j = (i + 1) % cylinderSegments;

                float u1 = (float)i / (float)cylinderSegments;
                float u2 = (float)(i + 1) / (float)cylinderSegments;

                AddVertex(builder, topVertices[i], texCoordId, u1, texCoordTop);
                AddVertex(builder, topVertices[j], texCoordId, u2, texCoordTop);
                AddVertex(builder, bottomVertices[i], texCoordId, u1, texCoordBottom);

                AddVertex(builder, topVertices[j], texCoordId, u2, texCoordTop);
                AddVertex(builder, bottomVertices[j], texCoordId, u2, texCoordBottom);
                AddVertex(builder, bottomVertices[i], texCoordId, u1, texCoordBottom);

                AddVertex(builder, topCenterVertex, texCoordId, u1, 0);
                AddVertex(builder, topVertices[j], texCoordId, u2, texCoordTop);
                AddVertex(builder, topVertices[i], texCoordId, u1, texCoordTop);

                AddVertex(builder, bottomCenterVertex, texCoordId, u1, 1);
                AddVertex(builder, bottomVertices[i], texCoordId, u1, texCoordBottom);
                AddVertex(builder, bottomVertices[j], texCoordId, u2, texCoordBottom);
            }
            SkyboxContent sky = new SkyboxContent();

            MeshContent skyMesh = builder.FinishMesh();

            sky.model = context.Convert<MeshContent, ModelContent>(skyMesh,"ModelProcessor");

            sky.texture = context.Convert<TextureContent, TextureContent>(input, "TextureProcessor");

            return sky; 
        }

        static void AddVertex(MeshBuilder builder, int vertex,
                      int texCoordId, float u, float v)
        {
            builder.SetVertexChannelData(texCoordId, new Vector2(u, v));

            builder.AddTriangleVertex(vertex);
        }
    }
}
