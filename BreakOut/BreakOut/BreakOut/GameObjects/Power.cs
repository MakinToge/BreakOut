// ***********************************************************************
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
    /// Class Power.
    /// </summary>
    public class Power : Sprite {
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
        /// Gets or sets the start position.
        /// </summary>
        /// <value>The start position.</value>
        public Vector2 StartPosition { get; set; }
        public PowerType PowerType { get; set; }
        public int Time { get; set; }

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
        public Power(float positionX, float positionY, float width, float height, float directionX, float directionY, float speed, int screenWidth, int screenHeight, PowerType power)
            : base(positionX, positionY, width, height, directionX, directionY, speed) {
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;
            this.PowerType = power;
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
