
using System;
using EmawEngineLibrary.Logging;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Messaging
{
    public abstract class PostOffice : GameComponent, IPostOffice
    {
        public PostOffice(Game game) : base(game)
        {
            m_log = (ILog) game.Services.GetService(typeof (ILog));
            game.Services.AddService(typeof(IPostOffice), this);
        }

        public void RegisterPostMan(PostMan postMan)
        {
            postMan.SendingMessage += DeliverMessage;
        }

        public void UnregisterPostMan(PostMan postMan)
        {
            postMan.SendingMessage -= DeliverMessage;
        }

        public abstract void DeliverMessage(MessageType type, object sender, object target, params object[] parameters);

        protected ILog m_log;
    }
}
