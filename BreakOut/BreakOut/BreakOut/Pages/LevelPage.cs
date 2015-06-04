// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="LevelPage.cs" company="">
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
namespace BreakOut {
    /// <summary>
    /// Class LevelPage.
    /// </summary>
    public class LevelPage : Page {

        /// <summary>
        /// Gets or sets the select level.
        /// </summary>
        /// <value>The select level.</value>
        public TextSprite SelectLevel { get; set; }
        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>The line.</value>
        public Texture2D Line { get; set; }
        /// <summary>
        /// Gets or sets the levels.
        /// </summary>
        /// <value>The levels.</value>
        public Button[] Levels { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Page" /> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public LevelPage(GraphicsDeviceManager graphics, int screenWidth, int screenHeight)
            : base(graphics, screenWidth, screenHeight) {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize() {
            //Texts
            this.SelectLevel = new TextSprite(6 * this.ScreenWidth / 32, 1 * this.ScreenHeight / 9, "Select Level", Color.White);

            //Line
            this.Line = new Texture2D(this.Graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixel = { 0xFFFFFF };
            this.Line.SetData<Int32>(pixel, 0, this.Line.Width * this.Line.Height);

            //Button Position
            float buttonPositionX = 9 * this.ScreenWidth / 48;
            float unitWidth = this.ScreenWidth / 48;
            float buttonPositionY = 3 * this.ScreenHeight / 9;
            float buttonWidth = 2 * this.ScreenWidth / 16;
            float buttonHeight = 2 * this.ScreenHeight / 9;

            this.Levels = new Button[] {
                new Button(buttonPositionX, buttonPositionY, buttonWidth, buttonHeight),
                new Button(17 * unitWidth, buttonPositionY, buttonWidth, buttonHeight),
                new Button(25 * unitWidth, buttonPositionY, buttonWidth, buttonHeight),
                new Button(33 * unitWidth, buttonPositionY, buttonWidth, buttonHeight),
                new Button(buttonPositionX, 2 * buttonPositionY, buttonWidth, buttonHeight),
                new Button(17 * unitWidth, 2 * buttonPositionY, buttonWidth, buttonHeight),
                new Button(25 * unitWidth, 2 * buttonPositionY, buttonWidth, buttonHeight),
                new Button(33 * unitWidth, 2 * buttonPositionY, buttonWidth, buttonHeight)
            };
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(ContentManager content) {
            SelectLevel.LoadContent(content, "Arial28");
            for (int i = 0; i < this.Levels.Length; i++) {
                this.Levels[i].LoadContent(content, "Level/" + (i + 1));
            }
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {
            foreach (Button item in Levels) {
                item.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            }
        }

        /// <summary>
        /// Draws the page.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            SelectLevel.Draw(spriteBatch, gameTime);
            foreach (Button item in Levels) {
                item.Draw(spriteBatch, gameTime);
            }
            spriteBatch.Begin();
            spriteBatch.Draw(this.Line, new Rectangle(6 * this.ScreenWidth / 32, 2 * this.ScreenHeight / 9, 20 * this.ScreenWidth / 32, 1), Color.White);
            spriteBatch.End();
        }
    }
}
