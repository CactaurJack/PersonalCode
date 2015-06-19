using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Cameras
{
    public interface ICamera : IGameComponent
    {
        Matrix View { get; set; }
        Matrix Projection { get; set; }
        Vector3 Position { get; set; }

        void Focus(IPosition focus);
    }
}
