using System;
using EmawEngineLibrary.Input;
using Microsoft.Xna.Framework;
using EmawEngineLibrary.Messaging;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Net;
using EmawEngineLibrary.Cameras;

namespace ExampleGame
{

    public enum GameMode
    {
        FFA,
        TEAM, 
        SINGLE,
        NONESELECTED,
        CAPTURETHEFLAG
    }

    public enum Team
    {
        RED,
        BLUE,
        NONE
    }

    class Player : IDisposable
    {


        private GameMode m_gameMode;
        public int PlayerScore { get; private set; }
        public Team _Team { get; set; }
        private PostMan m_postMan;
        private NetworkGamer m_gamer;

        private Game m_game;
        public Tank Tank { get; set; }
        public InputManager InputManager { get; set; }

        private Random random;
        private float m_respawnDelay;

        public Player(Game game, InputManager input, GameMode gameMode, NetworkGamer gamer)
        {
            m_game = game;
            //Tank = tank;
            //Tank._Player = this;//messy messy  =/
            InputManager = input;
            m_gameMode = gameMode;
            PlayerScore = 0;
            _Team = Team.NONE;
            m_postMan = new PostMan(game);
            m_gamer = gamer;
            random = new Random();

            spawn();
        }

        public void SetTeam(Team team)
        {
            _Team = team;
        }

        private void spawn()
        {
            Tank = new Tank(m_game, m_gameMode);
            Tank._Player = this;
            Tank._Random = random;
            Tank.Initialize();
            Tank.Position = new EmawEngineLibrary.Mixins.Position(new Vector3(random.Next(-200, 200), 10, random.Next(-200, 200)), 0);
        }

        public void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);

            TankInput currentInput = new TankInput();
            currentInput.ForwardBackward = InputManager.Movement.Z;
            currentInput.LeftRight = InputManager.Movement.X;
            currentInput.TurretUpDown = -InputManager.Rotation.X;
            currentInput.TurretLeftRight = InputManager.Rotation.Y;
            currentInput.Shoot = InputManager.ActionDone(Actions.FireShot);

            Tank.Input = currentInput;
            Tank.Update(gameTime, _Team);

            if (Tank.CurrentHP < 1)
            {
                m_respawnDelay += gameTime.ElapsedGameTime.Milliseconds;
                if (m_respawnDelay > 3000)
                {
                    Tank.Dispose();
                    spawn();
                    //Tank.Initialize();
                    if (m_gamer.IsLocal)
                    {
                        ICamera camera = m_game.Services.GetService(typeof(ICamera)) as ICamera;
                        camera.Focus(Tank);
                    }

                    m_respawnDelay = 0;
                }
            }
        }

        public void Update(GameTime gameTime, Vector3 tankPosition, float rotation, bool firing)
        {
            Tank.Update(gameTime, Matrix.CreateTranslation(tankPosition), rotation, firing, _Team);
        }

        public void HandleKill()
        {
            if (m_gameMode != GameMode.CAPTURETHEFLAG && m_gameMode != GameMode.NONESELECTED) PlayerScore++;
        }

        public void Dispose()
        {
            //m_game.Components.Remove(Tank);
            Tank.Dispose();
        }


        
    }
}