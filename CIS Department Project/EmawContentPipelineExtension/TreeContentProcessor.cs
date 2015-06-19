using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework;

namespace EmawContentPipelineExtension
{
    [ContentProcessor]
    class TreeModelProcessor : ModelProcessor
    {
        public override ModelContent Process(
            NodeContent input, ContentProcessorContext context)
        {
            MeshHelper.TransformScene(input, Matrix.CreateScale(12.0f));
            MeshHelper.TransformScene(input, Matrix.CreateRotationX(MathHelper.PiOver2));
            return base.Process(input, context);
        }
    }

}
