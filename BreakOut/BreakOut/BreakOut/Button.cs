// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="Button.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The BreakOut namespace.
/// </summary>
namespace BreakOut {
    /// <summary>
    /// Class Button.
    /// </summary>
    public class Button : Sprite {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is clicked.
        /// </summary>
        /// <value><c>true</c> if this instance is clicked; otherwise, <c>false</c>.</value>
        public bool IsClicked { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
            : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Button(float positionX, float positionY, float width, float height)
            : base(positionX, positionY, width, height, 0, 0, 0) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="directionX">The direction x.</param>
        /// <param name="directionY">The direction y.</param>
        /// <param name="speed">The speed.</param>
        public Button(float positionX, float positionY, float width, float height, float directionX, float directionY, float speed)
            : base(positionX, positionY, width, height, directionX, directionY, speed) { }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {
            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released) {
                Rectangle mouseRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1);
                if (mouseRectangle.Intersects(this.Rectangle)) {
                    this.IsClicked = true;
                }
            }
        }

    }
}
