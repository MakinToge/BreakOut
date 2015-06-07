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
namespace BreakOut {
    /// <summary>
    /// Class Brick.
    /// </summary>
    public class Brick : Sprite {

        /// <summary>
        /// The default brick value
        /// </summary>
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
        /// Gets or sets a value indicating whether this <see cref="Brick" /> is destroyed.
        /// </summary>
        /// <value><c>true</c> if destroyed; otherwise, <c>false</c>.</value>
        public bool Destroyed { get; set; }

        /// <summary>
        /// The number of hits to kill
        /// </summary>
        private int hitsToKill;

        /// <summary>
        /// Gets or sets the hits to kill.
        /// </summary>
        /// <value>The number of hits to kill.</value>
        public int HitsToKill {
            get { return hitsToKill; }
            set {
                hitsToKill = value;
                this.BrickImage = string.Format("brick{0}", value);
            }
        }


        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value { get; set; }
        /// <summary>
        /// Gets or sets the brick image.
        /// </summary>
        /// <value>The brick image.</value>
        public string BrickImage { get; set; }
        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>The power.</value>
        public PowerType Power { get; set; }
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public ContentManager Content { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Brick" /> class.
        /// </summary>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="hitsToKill">The hits to kill.</param>
        /// <param name="powerType">Type of the power.</param>
        public Brick(int positionX, int positionY, float screenWidth, float screenHeight, int hitsToKill, PowerType powerType) {
            float brickWidth = screenWidth * (9 / 8) / 27;
            float brickHeight = screenHeight / 27;

            this.Position = new Vector2(positionX * brickWidth, positionY * brickHeight);
            this.Size = new Vector2(brickWidth, brickHeight);
            this.Destroyed = false;
            this.Power = powerType;
            this.HitsToKill = hitsToKill;
            this.Value = DEFAULT_BRICK_VALUE;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="assetName">Name of the asset.</param>
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, string assetName) {
            base.LoadContent(content, assetName);
            this.Content = content;
        }
        /// <summary>
        /// Hits this instance.
        /// </summary>
        public void Hit() {
            this.HitsToKill -= 1;
            if (this.HitsToKill == 0) {
                this.Destroyed = true;
            }
            else {
                this.LoadContent(this.Content, this.BrickImage);
            }
        }

    }
}
