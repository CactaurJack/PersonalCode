using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Messaging;
using EmawEngineLibrary.Terrain;
using Microsoft.Xna.Framework;
using EmawEngineLibrary.Physics;

namespace ExampleGame
{
    class MessageHandler : PostOffice
    {
        public MessageHandler(Game game) : base(game)
        {
        }

        public override void DeliverMessage(MessageType type, object sender, object target, params object[] parameters)
        {
            m_log.WriteLog("Sent message {0} from {1} to {2}.", type.ToString(), sender == null ? "" : sender.GetType().ToString(), target == null ? "" : target.GetType().ToString());

            Tank tank;
            int amount;
            switch(type) 
            {
                case MessageType.Fire:
                    Ray ray = new Ray();
                    float velocity = 0f;
                    try
                    {
                        ray = (Ray)parameters[0];
                        velocity = (float)parameters[1];
                    }
                    catch (InvalidCastException)
                    {
                        break;
                    }
                    new Bullet(Game, ray, velocity, (Tank)sender);
                    break;
                case MessageType.Collision:
                   
                    ICollidable collidableTarget = target as ICollidable;
                    ICollidable collider = sender as ICollidable;
                    if (collidableTarget == null)
                    {
                        break;
                    }
                    collidableTarget.HandleCollision(collider, new Ray());
                    
                    break;
                case MessageType.Heal:
                    tank = target as Tank;
                    amount = (int) parameters[0];
                    if (tank == null)
                    {
                        break;
                    }
                    tank.RecoverHealth(amount);
                    break;
                case MessageType.Damage:
                    tank = target as Tank;
                    amount = (int)parameters[0];
                    if (tank == null)
                    {
                        break;
                    }
                    tank.TakeDamage(sender, amount);
                    break;
                case MessageType.Ammo:
                    tank = target as Tank;
                    amount = (int)parameters[0];
                    if (tank == null)
                    {
                        break;
                    }
                    tank.AddAmmo(amount);
                    break;
                case MessageType.Kill:
                    Bullet bullet = target as Bullet;
                    if (bullet == null)
                    {
                        break;
                    }
                    bullet.FiredByTank._Player.HandleKill();
                    break;
                case MessageType.SpawnPowerup:
                    Vector3 position = (Vector3) parameters[0];
                    new Powerup(Game, position);
                    break;

            }
        }
    }
}
