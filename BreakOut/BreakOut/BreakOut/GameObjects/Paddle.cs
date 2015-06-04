// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="Paddle.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The BreakOut namespace.
/// </summary>
namespace BreakOut {
    /// <summary>
    /// Class Paddle.
    /// </summary>
    public class Paddle : Sprite {
        /// <summary>
        /// Gets or sets the width of the screen.
        /// </summary>
        /// <value>The width of the screen.</value>
        public int ScreenWidth { get; set; }
        /// <summary>
        /// Gets or sets the height of the screen.
        /// </summary>
        /// <value>The height of the screen.</value>
        public int ScreenHeight { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Paddle"/> class.
        /// </summary>
        public Paddle()
            : this(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0) {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Paddle(Vector2 position)
            : this(position, Vector2.Zero, Vector2.Zero, 0) {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="speed">The speed.</param>
        public Paddle(Vector2 position, Vector2 size, Vector2 direction, float speed)
            : base(position, size, direction, speed) {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Paddle"/> class.
        /// </summary>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="directionX">The direction x.</param>
        /// <param name="directionY">The direction y.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public Paddle(float positionX, float positionY, float width, float height, float directionX, float directionY, float speed, int screenWidth, int screenHeight)
            : base(positionX, positionY, width, height, directionX, directionY, speed) {
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paddle"/> class.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public Paddle(Difficulty difficulty, int screenWidth, int screenHeight) {
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;
            this.setDifficulty(difficulty);
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(Microsoft.Xna.Framework.Input.KeyboardState previousKeyboardState, Microsoft.Xna.Framework.Input.KeyboardState currentKeyboardState, Microsoft.Xna.Framework.Input.MouseState previousMouseState, Microsoft.Xna.Framework.Input.MouseState currentMouseState) {
            //Mouse
            if (currentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) {
                if (currentMouseState.X < this.Size.X / 2) {
                    this.Position = new Vector2(0, this.Position.Y);
                }
                else if (currentMouseState.X > this.ScreenWidth - this.Size.X / 2) {
                    this.Position = new Vector2(this.ScreenWidth - this.Size.X, this.Position.Y);
                }
                else {
                    this.Position = new Vector2(currentMouseState.X - this.Size.X / 2, this.Position.Y);
                }

            }

            //Keyboard
            if ((currentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q)
                || currentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)
                || currentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                && this.Position.X > 0) {
                this.Direction = -Vector2.UnitX;
            }
            else if ((currentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)
                || currentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                && this.Position.X < this.ScreenWidth - this.Size.X) {
                this.Direction = Vector2.UnitX;
            }
            else {
                this.Direction = Vector2.Zero;
            }
        }

        /// <summary>
        /// Sets the difficulty.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void setDifficulty(Difficulty difficulty) {
            switch (difficulty) {
                case Difficulty.Easy:
                    this.Size = new Vector2(3 * this.ScreenWidth / 16, this.ScreenHeight / 18);
                    this.Position = new Vector2(this.ScreenWidth / 2 - this.ScreenWidth / 16, this.ScreenHeight - 2 * this.ScreenHeight / 18);
                    this.Speed = 0.75f;
                    break;
                case Difficulty.Normal:
                    this.Size = new Vector2(2 * this.ScreenWidth / 16, this.ScreenHeight / 18);
                    this.Position = new Vector2(this.ScreenWidth / 2 - this.ScreenWidth / 16, this.ScreenHeight - 2 * this.ScreenHeight / 18);
                    this.Speed = 1f;
                    break;
                case Difficulty.Hard:
                    this.Size = new Vector2(1 * this.ScreenWidth / 16, this.ScreenHeight / 18);
                    this.Position = new Vector2(this.ScreenWidth / 2 - this.ScreenWidth / 16, this.ScreenHeight - 2 * this.ScreenHeight / 18);
                    this.Speed = 1.25f;
                    break;
                case Difficulty.Impossible:
                    this.Size = new Vector2(1 * this.ScreenWidth / 16, this.ScreenHeight / 18);
                    this.Position = new Vector2(this.ScreenWidth / 2 - this.ScreenWidth / 16, this.ScreenHeight - 2 * this.ScreenHeight / 18);
                    this.Speed = 1.5f;
                    break;

            }
        }
    }
}
