// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="Ball.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The BreakOut namespace.
/// </summary>
namespace BreakOut
{
    /// <summary>
    /// Class Ball.
    /// </summary>
    public class Ball : Sprite
    {
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

        public bool changed { get; set; }
        private bool isOnFire;
        public bool IsOnFire
        {
            get
            {
                return isOnFire;
            }
            set
            {
                isOnFire = value;
                changed = true;
            }
        }

        private const float DEFAULT_BALL_MAX_SPEED = 1.5f;
        private const float EASY_BALL_MAX_SPEED = 0.6f;
        private const float NORMAL_BALL_MAX_SPEED = 0.85f;
        private const float HARD_BALL_MAX_SPEED = 1.0f;

        private const float DEFAULT_BALL_ACCELERATION = 0.05f;
        private const float EASY_BALL_ACCELERATION = 0.01f;
        private const float NORMAL_BALL_ACCELERATION = 0.06f;
        private const float HARD_BALL_ACCELERATION = 0.07f;

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
            : base(positionX, positionY, width, height, directionX, directionY, speed)
        {
            this.isOnFire = false;

            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;

            this.Acceleration = DEFAULT_BALL_ACCELERATION;
            this.MaxSpeed = DEFAULT_BALL_MAX_SPEED;

            this.StartPosition = this.Position;
            this.StartSpeed = this.Speed;
        }

        /// <summary>
        /// Updates the ball.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime, SoundEffect effect, bool isInvicible)
        { //touche mur
            //Bords Left and Right
            if ((this.Position.X <= 0 && this.Direction.X < 0)
                || (this.Position.X > this.ScreenWidth - this.Size.X && this.Direction.X > 0))
            {
                this.Direction = new Vector2(-1 * this.Direction.X, this.Direction.Y);
                effect.Play();
            }//Bord Up
            else if ((this.Position.Y <= 0 && this.Direction.Y < 0)
                || (this.Position.Y > this.ScreenHeight - this.Size.Y && this.Direction.Y > 0 && isInvicible))
            {
                this.Direction = new Vector2(this.Direction.X, -1 * this.Direction.Y);
                effect.Play();
            }


            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (this.changed == true)
            {
                Color[] data = new Color[this.Texture.Width * this.Texture.Height];
                this.Texture.GetData(data);

                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].A >= 200)
                    {
                        if (this.isOnFire)
                        {
                            data[i] = Color.Red;
                        }
                        else
                        {
                            data[i] = Color.White;
                        }
                    }
                }

                try
                {
                    this.Texture.SetData(data);
                }
                catch (InvalidOperationException ioe)
                {
                }
                changed = false;
            }

            base.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Sets the difficulty.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public void setDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    this.Acceleration = EASY_BALL_ACCELERATION;
                    this.MaxSpeed = EASY_BALL_MAX_SPEED;
                    break;
                case Difficulty.Normal:
                    this.Acceleration = NORMAL_BALL_ACCELERATION;
                    this.MaxSpeed = NORMAL_BALL_MAX_SPEED;
                    break;
                case Difficulty.Hard:
                    this.Acceleration = HARD_BALL_ACCELERATION;
                    this.MaxSpeed = HARD_BALL_MAX_SPEED;
                    break;
            }
        }

        /// <summary>
        /// Determines whether this instance is out.
        /// </summary>
        /// <returns><c>true</c> if this instance is out; otherwise, <c>false</c>.</returns>
        public bool isOut()
        {
            if (this.Position.Y > this.ScreenHeight)
            {
                return true;
            }
            return false;
        }


    }
}
