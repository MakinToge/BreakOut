// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="DifficultyPage.cs" company="">
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
    /// Class DifficultyPage.
    /// </summary>
    public class DifficultyPage : Page
    {

        /// <summary>
        /// Gets or sets the select difficulty.
        /// </summary>
        /// <value>The select difficulty.</value>
        public TextSprite SelectDifficulty { get; set; }
        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>The line.</value>
        public Texture2D Line { get; set; }
        /// <summary>
        /// Gets or sets the button return.
        /// </summary>
        /// <value>The button return.</value>
        public Button ButtonReturn { get; set; }
        /// <summary>
        /// Gets or sets the button easy.
        /// </summary>
        /// <value>The button easy.</value>
        public Button ButtonEasy { get; set; }
        /// <summary>
        /// Gets or sets the button normal.
        /// </summary>
        /// <value>The button normal.</value>
        public Button ButtonNormal { get; set; }
        /// <summary>
        /// Gets or sets the button hard.
        /// </summary>
        /// <value>The button hard.</value>
        public Button ButtonHard { get; set; }
        /// <summary>
        /// Gets or sets the button high scores.
        /// </summary>
        /// <value>The button high scores.</value>
        public Button ButtonHighScores { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Page" /> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public DifficultyPage(GraphicsDeviceManager graphics, int screenWidth, int screenHeight)
            : base(graphics, screenWidth, screenHeight)
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            //Texts
            this.SelectDifficulty = new TextSprite(6 * this.DefaultUnitX, 2 * this.DefaultUnitY, "Select Difficulty", Color.White);
            this.ButtonReturn = new Button(this.DefaultUnitX, 2 * this.DefaultUnitY, 2 * this.DefaultUnitX, this.DefaultUnitY);

            //Line
            this.Line = new Texture2D(this.Graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixel = { 0xFFFFFF };
            this.Line.SetData<Int32>(pixel, 0, this.Line.Width * this.Line.Height);

            //Button Position
            float buttonPositionX = 6 * this.DefaultUnitX;
            float buttonPositionY = 6 * this.DefaultUnitY;
            float buttonWidth = 6 * this.DefaultUnitX;
            float buttonHeight = 6 * this.DefaultUnitY;
            this.ButtonEasy = new Button(buttonPositionX, buttonPositionY, buttonWidth, buttonHeight);
            this.ButtonNormal = new Button(13 * this.DefaultUnitX, buttonPositionY, buttonWidth, buttonHeight);
            this.ButtonHard = new Button(20 * this.DefaultUnitX, buttonPositionY, buttonWidth, buttonHeight);

            this.ButtonHighScores = new Button(6 * this.DefaultUnitX, 15 * this.DefaultUnitY, this.DefaultButtonWidth, this.DefaultButtonHeight);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(ContentManager content)
        {
            ButtonEasy.LoadContent(content, "easy");
            ButtonNormal.LoadContent(content, "normal");
            ButtonHard.LoadContent(content, "hard");
            SelectDifficulty.LoadContent(content, "Arial28");
            ButtonReturn.LoadContent(content, "return");
            ButtonHighScores.LoadContent(content, "highscores");
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState)
        {
            this.ButtonEasy.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            this.ButtonNormal.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            this.ButtonHard.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            this.ButtonReturn.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            this.ButtonHighScores.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
        }

        /// <summary>
        /// Draws the page.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            SelectDifficulty.Draw(spriteBatch, gameTime);
            ButtonEasy.Draw(spriteBatch, gameTime);
            ButtonNormal.Draw(spriteBatch, gameTime);
            ButtonHard.Draw(spriteBatch, gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(this.Line, new Rectangle((int)(6 * this.DefaultUnitX), (int)(4 * this.DefaultUnitY), (int)(20 * this.DefaultUnitX), 1), Color.White);
            spriteBatch.End();
            ButtonReturn.Draw(spriteBatch, gameTime);
            ButtonHighScores.Draw(spriteBatch, gameTime);
        }
    }
}
