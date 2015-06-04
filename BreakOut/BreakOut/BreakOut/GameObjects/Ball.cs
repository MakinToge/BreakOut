﻿// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="Ball.cs" company="">
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
    /// Class Ball.
    /// </summary>
    public class Ball : Sprite {
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
        /// Gets or sets the acceleration.
        /// </summary>
        /// <value>The acceleration.</value>
        public float Acceleration { get; set; }
        /// <summary>
        /// Gets or sets the start speed.
        /// </summary>
        /// <value>The start speed.</value>
        public float StartSpeed { get; set; }
        /// <summary>
        /// Gets or sets the maximum speed.
        /// </summary>
        /// <value>The maximum speed.</value>
        public float MaxSpeed { get; set; }
        /// <summary>
        /// Gets or sets the start position.
        /// </summary>
        /// <value>The start position.</value>
        public Vector2 StartPosition { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Ball"/> class.
        /// </summary>
        public Ball()
            : this(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0) {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Ball(Vector2 position)
            : this(position, Vector2.Zero, Vector2.Zero, 0) {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="speed">The speed.</param>
        public Ball(Vector2 position, Vector2 size, Vector2 direction, float speed) : base(position, size, direction, speed) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Ball"/> class.
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
        /// <param name="difficulty">The difficulty.</param>
        public Ball(float positionX, float positionY, float width, float height, float directionX, float directionY, float speed, int screenWidth, int screenHeight, Difficulty difficulty)
            : base(positionX, positionY, width, height, directionX, directionY, speed) {
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;

            this.Acceleration = 0.05f;
            this.MaxSpeed = 1.5f;

            this.StartPosition = this.Position;
            this.StartSpeed = this.Speed;
        }

        /// <summary>
        /// Updates the ball.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime) {
            //Bords Left and Right
            if ((this.Position.X <= 0 && this.Direction.X < 0) || (this.Position.X > this.ScreenWidth - this.Size.X && this.Direction.X > 0)) {
                this.Direction = new Vector2(-1 * this.Direction.X, this.Direction.Y);
            }//Bord Up
            else if (this.Position.Y <= 0 && this.Direction.Y < 0) {
                this.Direction = new Vector2(this.Direction.X, -1 * this.Direction.Y);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Sets the difficulty.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void setDifficulty(Difficulty difficulty) {
            switch (difficulty) {
                case Difficulty.Easy:
                    this.Acceleration = 0.05f;
                    this.MaxSpeed = 0.9f;
                    break;
                case Difficulty.Normal:
                    this.Acceleration = 0.06f;
                    this.MaxSpeed = 1.0f;
                    break;
                case Difficulty.Hard:
                    this.Acceleration = 0.07f;
                    this.MaxSpeed = 1.1f;
                    break;
                case Difficulty.Impossible:
                    this.Acceleration = 0.09f;
                    this.MaxSpeed = 1.2f;
                    break;

            }
        }

        /// <summary>
        /// Determines whether this instance is out.
        /// </summary>
        /// <returns><c>true</c> if this instance is out; otherwise, <c>false</c>.</returns>
        public bool isOut() {
            if (this.Position.Y > this.ScreenHeight) {
                return true;
            }
            return false;
        }


    }
}