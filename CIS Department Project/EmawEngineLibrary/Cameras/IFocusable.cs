using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Cameras
{
    public interface IFocusable
    {
        Matrix Position { get; }
        float Facing { get; }
    }
}
