
namespace EmawEngineLibrary.Messaging
{
    public interface IPostOffice
    {
        void RegisterPostMan(PostMan postMan);
        void DeliverMessage(MessageType type, object sender, object target, params object[] parameters);
        void UnregisterPostMan(PostMan postMan);
    }
}