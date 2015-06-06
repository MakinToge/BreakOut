// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="Brick.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    /// Class Brick.
    /// </summary>
    public class Brick : Sprite
    {

        private const short DEFAULT_BRICK_VALUE = 100;

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
        /// Gets or sets a value indicating whether this <see cref="Brick"/> is destroyed.
        /// </summary>
        /// <value><c>true</c> if destroyed; otherwise, <c>false</c>.</value>
        public bool Destroyed { get; set; }

        private int hitsToKill;

        public int HitsToKill
        {
            get { return hitsToKill; }
            set
            {
                hitsToKill = value;
                this.BrickImage = string.Format("brick{0}", value);
            }
        }


        public int Value { get; set; }
        public string BrickImage { get; set; }
        public PowerType Power { get; set; }
        public ContentManager Content { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class.
        /// </summary>
        public Brick(int positionX, int positionY, float screenWidth, float screenHeight, int hitsToKill, PowerType powerType)
        {
            float brickWidth = screenWidth * (9 / 8) / 27;
            float brickHeight = screenHeight / 27;

            this.Position = new Vector2(positionX * brickWidth, positionY * brickHeight);
            this.Size = new Vector2(brickWidth, brickHeight);
            this.Destroyed = false;
            this.Power = powerType;
            this.HitsToKill = hitsToKill;
            this.Value = DEFAULT_BRICK_VALUE;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
            this.Content = content;
        }
        public void Hit()
        {
            this.HitsToKill -= 1;
            if (this.HitsToKill == 0)
            {
                this.Destroyed = true;
            }
            else
            {
                this.LoadContent(this.Content, this.BrickImage);
            }
        }

    }
}
