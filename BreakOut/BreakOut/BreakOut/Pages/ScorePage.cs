// ***********************************************************************
// Assembly         : BreakOut
// Author           : Floriel
// Created          : 06-05-2015
//
// Last Modified By : Floriel
// Last Modified On : 06-05-2015
// ***********************************************************************
// <copyright file="ScorePage.cs" company="">
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
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// The Pages namespace.
/// </summary>
namespace BreakOut.Pages
{
    /// <summary>
    /// Class ScorePage.
    /// </summary>
    public class ScorePage : Page
    {
        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>The line.</value>
        public Texture2D Line { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public TextSprite Title { get; set; }
        /// <summary>
        /// Gets or sets the button return.
        /// </summary>
        /// <value>The button return.</value>
        public Button ButtonReturn { get; set; }
        /// <summary>
        /// Gets or sets the button reset.
        /// </summary>
        /// <value>The button reset.</value>
        public Button ButtonReset { get; set; }
        /// <summary>
        /// Gets or sets the scores.
        /// </summary>
        /// <value>The scores.</value>
        public string[] Scores { get; set; }
        /// <summary>
        /// Gets or sets the scores sprites.
        /// </summary>
        /// <value>The scores sprites.</value>
        public List<TextSprite> ScoresSprites { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Page" /> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="nbLevel">The nb level.</param>
        public ScorePage(GraphicsDeviceManager graphics, int screenWidth, int screenHeight, string fileName, int nbLevel)
            : base(graphics, screenWidth, screenHeight)
        {
            this.FileName = fileName;
            this.ScoresSprites = new List<TextSprite>();
            this.Scores = new string[8];
            if (!File.Exists(fileName))
            {
                for (int i = 0; i < nbLevel; i++)
                {
                    this.Scores[i] = "0";
                }
                File.WriteAllLines(fileName, this.Scores);
            }
            this.Scores = File.ReadAllLines(fileName);
            for (int i = 0; i < this.Scores.Length; i++)
            {
                string text = string.Format("Level {0} : {1}", i + 1, this.Scores[i]);
                float x = 6 * this.DefaultUnitX;
                float y = (4 + i) * this.DefaultUnitY;
                this.ScoresSprites.Add(new TextSprite(x, y, text, Color.White));
            }

        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            //Texts
            this.Title = new TextSprite(6 * this.DefaultUnitX, 2f * this.DefaultUnitY, "Score", Color.White);

            //Line
            this.Line = new Texture2D(this.Graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixel = { 0xFFFFFF };
            this.Line.SetData<Int32>(pixel, 0, this.Line.Width * this.Line.Height);

            this.ButtonReturn = new Button(this.DefaultUnitX, 2f * this.DefaultUnitY, 2f * this.DefaultUnitX, this.DefaultUnitY);
            this.ButtonReset = new Button(6 * this.DefaultUnitX, 15 * this.DefaultUnitY, this.DefaultButtonWidth, this.DefaultButtonHeight);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(ContentManager content)
        {
            Title.LoadContent(content, "Arial28");
            ButtonReturn.LoadContent(content, "return");
            ButtonReset.LoadContent(content, "resetHighScores");
            foreach (TextSprite item in this.ScoresSprites)
            {
                item.LoadContent(content, "Arial28");
            }
        }
        public override void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState)
        {
            ButtonReturn.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            ButtonReset.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
            if (ButtonReset.IsClicked)
            {
                this.ResetScore();
                ButtonReset.IsClicked = false;
            }
        }
        /// <summary>
        /// Draws the page.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Title.Draw(spriteBatch, gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(this.Line, new Rectangle((int)(6 * this.DefaultUnitX), (int)(4 * this.DefaultUnitY), (int)(20 * this.DefaultUnitX), 1), Color.White);
            spriteBatch.End();
            foreach (TextSprite item in this.ScoresSprites)
            {
                item.Draw(spriteBatch, gameTime);
            }
            ButtonReturn.Draw(spriteBatch, gameTime);
            ButtonReset.Draw(spriteBatch, gameTime);
        }

        /// <summary>
        /// Saves the score.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="score">The score.</param>
        public void SaveScore(int level, int score)
        {
            if (Convert.ToInt32(this.Scores[level - 1]) < score)
            {
                this.Scores[level - 1] = score.ToString();
                this.ScoresSprites[level - 1].Text = string.Format("Level {0} : {1}", level, score.ToString());
                System.IO.File.WriteAllLines(this.FileName, this.Scores);
            }
        }

        /// <summary>
        /// Resets the score.
        /// </summary>
        public void ResetScore()
        {
            for (int i = 0; i < this.Scores.Length; i++)
            {
                this.Scores[i] = "0";
                string text = string.Format("Level {0} : 0", i + 1);
                this.ScoresSprites[i].Text = text;
            }
            System.IO.File.WriteAllLines(this.FileName, this.Scores);
        }
    }
}
