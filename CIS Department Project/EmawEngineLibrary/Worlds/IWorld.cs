using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using EmawEngineLibrary.Input;
using EmawEngineLibrary.Logging;
using EmawEngineLibrary.Performance;
using EmawEngineLibrary.Physics;
using EmawEngineLibrary;
using EmawEngineLibrary.Graphics;
using EmawEngineLibrary.Graphics.Particles;
using EmawEngineLibrary.Graphics.Primitives;
using EmawEngineLibrary.Graphics.Billboards;
using EmawEngineLibrary.Cameras;
using EmawEngineLibrary.Worlds;
using EmawEngineLibrary.Terrain;

namespace EmawEngineLibrary.Worlds
{
    public interface IWorld
    {
        CollisionBox GetWorldbox();
    }
}
