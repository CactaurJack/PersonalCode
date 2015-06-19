using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content;

namespace EmawContentPipelineExtension
{
    //This class was created using the GeneratedGeometry Example from 
    //http://create.msdn.com/en-US/education/catalog/sample/generated_geometry
    //as a refference
    [ContentSerializerRuntimeType("ExampleGame.Skybox, ExampleGame")]
    public class SkyboxContent
    {
        public ModelContent model;
        public TextureContent texture;
    }
}
