using System;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Messaging
{
    public delegate void Message(MessageType type, object sender, object target, params object[] parameters);
    public class PostMan
    {
        private Game m_game;
        public event Message SendingMessage;

        /// <summary>
        /// Creates a new PostMan and registers it with the current PostOffice in the game.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="owner"></param>
        public PostMan(Game game)
        {
            m_game = game;
            IPostOffice postOffice = (IPostOffice) game.Services.GetService(typeof (IPostOffice));
            postOffice.RegisterPostMan(this);
        }

        ~PostMan()
        {
            IPostOffice postOffice = (IPostOffice) m_game.Services.GetService(typeof (IPostOffice));
            postOffice.UnregisterPostMan(this);
        }

        /// <summary>
        /// Allows us to send a message and impersonate someone else asynchronously.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        public void SendMessageAsync(MessageType type, object sender, object target, params object[] parameters)
        {
            SendingMessage.BeginInvoke(type, sender, target, parameters, MessageSent, null);
        }

        /// <summary>
        /// Allows us to send a message and impersonate someone else synchronously. Should be used when
        /// a message needs to be processed before the next Update. (E.g. collisions)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        public void SendMessage(MessageType type, object sender, object target, params object[] parameters)
        {
            SendingMessage.Invoke(type, sender, target, parameters);
        }

        /// <summary>
        /// Is called once the message is sent. This allows us to garbage collect our thread stuff sooner.
        /// </summary>
        /// <param name="result"></param>
        private void MessageSent(IAsyncResult result)
        {
            SendingMessage.EndInvoke(result);
        }
    }
}
