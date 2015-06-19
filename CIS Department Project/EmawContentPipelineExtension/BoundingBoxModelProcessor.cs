using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

//This program is a slightly modified version of the one found here:
//http://andyq.no-ip.com/blog/?p=16

//And in turn, this model processor was found at
//http://www.harding.edu/dsteil/xna/notes/Making%20Bounding%20Boxes%20For%20Models.htm
//It places a minimum bounding box in the model's Tag property.
//Slightly modified to account for the Scale parameter in the ModelProcessor and
//to make sure it covers all the model.

namespace EmawContentPipelineExtension
{
    [ContentProcessor]
    public class BoundingBoxModelProcessor : ModelProcessor
    {
        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            ModelContent modelContent = base.Process(input, context);
            
            NodeContentCollection nodeContentCollection = input.Children;
            nodeContentCollection.Add(input);

            //This is a recursive function in case the input's children have children.
            parseChildren(nodeContentCollection);

            //Make the bottom of our bounding box be at 0.
            Vector3 min = new Vector3(minX, 0, minZ);
            Vector3 max = new Vector3(maxX, Math.Abs(maxY) + Math.Abs(minY), maxZ);

            modelContent.Tag = new BoundingBox(min, max);

            return modelContent;
        }


        private void parseChildren(NodeContentCollection nodeContentCollection)
        {
            foreach (NodeContent nodeContent in nodeContentCollection)
            {
                if (nodeContent is MeshContent)
                {
                    MeshContent meshContent = (MeshContent) nodeContent;

                    foreach (Vector3 vector in meshContent.Positions)
                    {
                        if (vector.X < minX)
                            minX = vector.X;

                        if (vector.Y < minY)
                            minY = vector.Y;

                        if (vector.Z < minZ)
                            minZ = vector.Z;

                        if (vector.X > maxX)
                            maxX = vector.X;

                        if (vector.Y > maxY)
                            maxY = vector.Y;

                        if (vector.Z > maxZ)
                            maxZ = vector.Z;
                    }
                }

                else
                {
                    parseChildren(nodeContent.Children);
                }
            }
        }

        private float maxX = float.MinValue;
        private float maxY = float.MinValue;
        private float maxZ = float.MinValue;
        private float minX = float.MaxValue;
        private float minY = float.MaxValue;
        private float minZ = float.MaxValue;
    }
}