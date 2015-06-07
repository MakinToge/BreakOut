// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="FinishPage.cs" company="">
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
    /// Class FinishPage.
    /// </summary>
    public class FinishPage : Page {

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public TextSprite Title { get; set; }
        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>The line.</value>
        public Texture2D Line { get; set; }
        /// <summary>
        /// Gets or sets the button replay.
        /// </summary>
        /// <value>The button replay.</value>
        public Button Replay { get; set; }
        /// <summary>
        /// Gets or sets the button return to select level.
        /// </summary>
        /// <value>The button return to select level.</value>
        public Button ReturnToSelectLevel { get; set; }
        /// <summary>
        /// Gets or sets the button return to select difficulty.
        /// </summary>
        /// <value>The button return to select difficulty.</value>
        public Button ReturnToSelectDifficulty { get; set; }
        /// <summary>
        /// Gets or sets the button exit.
        /// </summary>
        /// <value>The button exit.</value>
        public Button Exit { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Page" /> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public FinishPage(GraphicsDeviceManager graphics, int screenWidth, int screenHeight)
            : base(graphics, screenWidth, screenHeight) {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize() {
            //Texts
            this.Title = new TextSprite(6 * this.DefaultUnitX, 2f * this.DefaultUnitY, "Congratulation !", Color.White);

            //Line
            this.Line = new Texture2D(this.Graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixel = { 0xFFFFFF };
            this.Line.SetData<Int32>(pixel, 0, this.Line.Width * this.Line.Height);

            //Button Position
            float buttonPositionX = 6 * this.DefaultUnitX;
            float buttonPositionY = this.DefaultUnitY;

            this.Replay = new Button(buttonPositionX, 6 * buttonPositionY, this.DefaultButtonWidth, this.DefaultButtonHeight);
            this.ReturnToSelectLevel = new Button(buttonPositionX, 9 * buttonPositionY, this.DefaultButtonWidth, this.DefaultButtonHeight);
            this.ReturnToSelectDifficulty = new Button(buttonPositionX, 12 * buttonPositionY, this.DefaultButtonWidth, this.DefaultButtonHeight);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(ContentManager content) {
            Replay.LoadContent(content, "replay");
            ReturnToSelectLevel.LoadContent(content, "level");
            ReturnToSelectDifficulty.LoadContent(content, "difficulty");
            Title.LoadContent(content, "Arial28");
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {
            this.Replay.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            this.ReturnToSelectLevel.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            this.ReturnToSelectDifficulty.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
        }

        /// <summary>
        /// Draws the page.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            Title.Draw(spriteBatch, gameTime);

            Replay.Draw(spriteBatch, gameTime);
            ReturnToSelectLevel.Draw(spriteBatch, gameTime);
            ReturnToSelectDifficulty.Draw(spriteBatch, gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(this.Line, new Rectangle((int)(6 * this.DefaultUnitX), (int)(4 * this.DefaultUnitY), (int)(20 * this.DefaultUnitX), 1), Color.White);
            spriteBatch.End();

        }
    }
}
