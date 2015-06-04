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
        public int HitsToKill { get; set; }
        public int Value { get; set; }
        public string[] BrickImages { get; set; }
        public PowerType Power { get; set; }
        public ContentManager Content { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class.
        /// </summary>
        public Brick()
            : this(Vector2.Zero, Vector2.Zero, Vector2.Zero, 0) {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Brick(Vector2 position)
            : this(position, Vector2.Zero, Vector2.Zero, 0) {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="speed">The speed.</param>
        public Brick(Vector2 position, Vector2 size, Vector2 direction, float speed) : base(position, size, direction, speed) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Brick"/> class.
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
        public Brick(float positionX, float positionY, float width, float height, float directionX, float directionY, float speed, int screenWidth, int screenHeight)
            : this(positionX, positionY, width, height, directionX, directionY, speed,screenWidth, screenHeight,1, new string[]{"brick"}) {
        }
        public Brick(float positionX, float positionY, float width, float height, float directionX, float directionY, float speed, int screenWidth, int screenHeight, int hitsToKill,string[] brickImages)
            : base(positionX, positionY, width, height, directionX, directionY, speed) {
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;
            this.Destroyed = false;
            this.Power = PowerType.None;
            this.HitsToKill = hitsToKill;
            this.BrickImages = brickImages;
            this.Value = 100;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content, string assetName)
{
 	 base.LoadContent(content, assetName);
            this.Content = content;
}
        public void Hit() {
            this.HitsToKill -= 1;
            if (this.HitsToKill == 0) {
                this.Destroyed = true;
            } else {
                this.LoadContent(this.Content, this.BrickImages[this.HitsToKill - 1]);
            }
        }

    }
}
