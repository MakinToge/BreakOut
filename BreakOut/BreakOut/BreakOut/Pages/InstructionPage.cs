// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="InstructionPage.cs" company="">
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
    /// Class InstructionPage.
    /// </summary>
    public class InstructionPage : Page {

        /// <summary>
        /// Gets or sets the button return.
        /// </summary>
        /// <value>The button return.</value>
        public Button ButtonReturn { get; set; }
        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions.</value>
        public Sprite Instructions { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Page" /> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public InstructionPage(GraphicsDeviceManager graphics, int screenWidth, int screenHeight)
            : base(graphics, screenWidth, screenHeight) {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize() {
            this.Instructions = new Sprite(0, 0, this.ScreenWidth, this.ScreenHeight, 0, 0, 0);
            this.ButtonReturn = new Button(this.DefaultUnitX, 2 * this.DefaultUnitY, 2 * this.DefaultUnitX, this.DefaultUnitY);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(ContentManager content) {
            ButtonReturn.LoadContent(content, "return");
            Instructions.LoadContent(content, "instructions");
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {
            this.ButtonReturn.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);
        }

        /// <summary>
        /// Draws the page.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            Instructions.Draw(spriteBatch, gameTime);
            ButtonReturn.Draw(spriteBatch, gameTime);
        }
    }
}
