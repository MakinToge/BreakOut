// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="Page.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    /// Class Page.
    /// </summary>
    public class Page
    {

        /// <summary>
        /// The screen width
        /// </summary>
        private int screenWidth;
        /// <summary>
        /// Gets or sets the width of the screen.
        /// </summary>
        /// <value>The width of the screen.</value>
        public int ScreenWidth
        {
            get { return screenWidth; }
            set
            {
                screenWidth = value;
                this.Graphics.PreferredBackBufferWidth = value;
                this.Graphics.ApplyChanges();
            }
        }
        /// <summary>
        /// The screen height
        /// </summary>
        private int screenHeight;
        /// <summary>
        /// Gets or sets the height of the screen.
        /// </summary>
        /// <value>The height of the screen.</value>
        public int ScreenHeight
        {
            get { return screenHeight; }
            set
            {
                screenHeight = value;
                this.Graphics.PreferredBackBufferHeight = value;
                this.Graphics.ApplyChanges();
            }
        }
        public float DefaultUnitX { get; set; }
        public float DefaultUnitY { get; set; }
        public float DefaultButtonWidth { get; set; }
        public float DefaultButtonHeight { get; set; }
        /// <summary>
        /// Gets or sets the graphics.
        /// </summary>
        /// <value>The graphics.</value>
        public GraphicsDeviceManager Graphics { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public Page(GraphicsDeviceManager graphics, int screenWidth, int screenHeight)
        {
            this.Graphics = graphics;
            this.ScreenWidth = screenWidth;
            this.ScreenHeight = screenHeight;

            this.DefaultUnitX = screenWidth / 32;
            this.DefaultUnitY = screenHeight / 18;
            this.DefaultButtonWidth = 10 * ScreenWidth / 16;
            this.DefaultButtonHeight = screenHeight / 9;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public virtual void LoadContent(ContentManager content)
        {
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        protected virtual void UnloadContent()
        {
        }
        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public virtual void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState)
        {
        }
        /// <summary>
        /// Updates the pages.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        public virtual void Update(GameTime gametime)
        {
        }
        /// <summary>
        /// Draws the page.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
    }
}
