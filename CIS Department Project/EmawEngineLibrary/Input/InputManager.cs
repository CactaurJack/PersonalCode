using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EmawEngineLibrary.Input
{
    //TODO: Break this out into a GameInputManager and a MenuInputManager so we're not doing all this extra stuff during menu operation.

    public class InputManager : GameComponent
    {
        public InputManager(Game game) : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(InputManager), this);
        }

        public override void Initialize()
        {
            m_movementMappings = new Keys[9];
            m_actionMappings = new Keys[ACTIONS_COUNT];

            m_movementMappings[(int) Input.Movement.MoveForward]  = Keys.W;
            m_movementMappings[(int) Input.Movement.MoveBackward] = Keys.S;
            m_movementMappings[(int) Input.Movement.MoveLeft]     = Keys.A;
            m_movementMappings[(int) Input.Movement.MoveRight]    = Keys.D;
            m_movementMappings[(int) Input.Movement.RotateUp]     = Keys.Up;
            m_movementMappings[(int) Input.Movement.RotateDown]   = Keys.Down;
            m_movementMappings[(int) Input.Movement.RotateLeft]   = Keys.Left;
            m_movementMappings[(int) Input.Movement.RotateRight]  = Keys.Right;

            m_actionMappings[(int)Input.Actions.FireShot]      = Keys.Space;

            Movement = Vector3.Zero;
            Rotation = Vector3.Zero;

            base.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            Game.Components.Remove(this);
            Game.Services.RemoveService(typeof(InputManager));
            base.Dispose(disposing);
        }

        public bool ActionDone(Actions action)
        {
            if (action == Actions.FireShot)
            {
                return ShotFired;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private bool ShotFired;

        public Vector3 Movement { get; private set; }

        public Vector3 Rotation { get; private set; }

        public bool IsAnyButtonPressed()
        {
            if (Enum.GetValues(typeof (Keys)).Cast<Keys>().Any(key => currentKeyboardState.IsKeyDown(key)))
            {
                return true;
            }
            if (currentGamePadState.IsConnected)
                return Enum.GetValues(typeof (Buttons)).Cast<Buttons>().Any(button => currentGamePadState.IsButtonDown(button));
            return false;
        }

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Vector3 movement = Vector3.Zero;
            Vector3 rotation = Vector3.Zero;

            //I think this is right for the gamepad; I can't test it.
            if (currentGamePadState.IsConnected)
            {
                movement += Vector3.Forward*(currentGamePadState.ThumbSticks.Left.Y*time*0.01f);
                rotation += Vector3.Forward*(currentGamePadState.ThumbSticks.Right.X * time * 0.001f);
            }

            if (currentKeyboardState.IsKeyDown(m_movementMappings[(int)Input.Movement.MoveForward]))
            {
                movement -= Vector3.Forward * time * .01f;
            }
            if (currentKeyboardState.IsKeyDown(m_movementMappings[(int)Input.Movement.MoveBackward]))
            {
                movement += Vector3.Forward * time * .01f;
            }
            if (currentKeyboardState.IsKeyDown(m_movementMappings[(int)Input.Movement.MoveRight]))
            {
                movement -= Vector3.Right * time * .001f;
            }
            if (currentKeyboardState.IsKeyDown(m_movementMappings[(int)Input.Movement.MoveLeft]))
            {
                movement += Vector3.Right * time * .001f;
            }

            if (currentKeyboardState.IsKeyDown(m_movementMappings[(int)Input.Movement.RotateUp]))
            {
                rotation -= Vector3.Right * time * .001f;
            }
            if (currentKeyboardState.IsKeyDown(m_movementMappings[(int)Input.Movement.RotateDown]))
            {
                rotation += Vector3.Right * time * .001f;
            }
            if (currentKeyboardState.IsKeyDown(m_movementMappings[(int)Input.Movement.RotateRight]))
            {
                rotation -= Vector3.Up * time * .001f;
            }
            if (currentKeyboardState.IsKeyDown(m_movementMappings[(int)Input.Movement.RotateLeft]))
            {
                rotation += Vector3.Up * time * .001f;
            }


            ShotFired = currentKeyboardState.IsKeyDown(m_actionMappings[(int)Input.Actions.FireShot]);

            Movement = movement;

            Rotation = rotation;
        }


        KeyboardState currentKeyboardState;
        GamePadState currentGamePadState;


        private Keys[] m_actionMappings;
        private Keys[] m_movementMappings;

        /// <summary>
        /// Contains the count of elements in the Action enum.
        /// </summary>
        private const int ACTIONS_COUNT = 1;
    }

    /// <summary>
    /// Had to rename Actions because of a naming conflict with System.Action
    /// </summary>
    public enum Actions
    {
        FireShot = 0
    }

    public enum Movement
    {
        MoveForward = 0,
        MoveBackward,
        MoveLeft,
        MoveRight,
        RotateUp,
        RotateDown,
        RotateLeft,
        RotateRight,
        FireShot
    }
}
