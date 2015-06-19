namespace EmawEngineLibrary.Physics
{
    public interface ICollisionManager
    {
        void RegisterCollisionObject(ICollidable collisionObject);
        void UnregisterCollisionObject(ICollidable collisionObject);

        void RegisterTerrainCollisionObject(ICollidable collisionObject);
        void UnregisterTerrainCollisionObject(ICollidable collisionObject);

        void ClearCollisionList();
    }
}