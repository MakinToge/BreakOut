// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="BreakOut.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BreakOut.Pages;

/// <summary>
/// The BreakOut namespace.
/// </summary>
namespace BreakOut {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BreakOut : Microsoft.Xna.Framework.Game {

        public const int DEFAULT_START_LIVES = 3;
        public const int DEFAULT_WINDOWS_WIDTH = 1280;
        public const int DEFAULT_WINDOWS_HEIGHT = 720;
        public const int TIME_LIMIT = 200;
        

        /// <summary>
        /// The graphics
        /// </summary>
        GraphicsDeviceManager graphics;
        /// <summary>
        /// The sprite batch
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// The background color
        /// </summary>
        Color BackgroundColor = new Color(60, 60, 63);
        /// <summary>
        /// The current game state
        /// </summary>
        GameState CurrentGameState = GameState.MainMenu;

        public SoundEffect effectVictory;
        public SoundEffect effectDefeat;

        public SoundEffect song;
        public SoundEffectInstance nyan;
        /// <summary>
        /// The screen width
        /// </summary>
        private int screenWidth;
        /// <summary>
        /// Gets or sets the width of the screen.
        /// </summary>
        /// <value>The width of the screen.</value>
        public int ScreenWidth {
            get { return screenWidth; }
            set {
                screenWidth = value;
                this.graphics.PreferredBackBufferWidth = value;
                this.graphics.ApplyChanges();
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
        public int ScreenHeight {
            get { return screenHeight; }
            set {
                screenHeight = value;
                this.graphics.PreferredBackBufferHeight = value;
                this.graphics.ApplyChanges();
            }
        }
        /// <summary>
        /// Gets or sets the state of the current mouse.
        /// </summary>
        /// <value>The state of the current mouse.</value>
        public MouseState CurrentMouseState { get; set; }
        /// <summary>
        /// Gets or sets the state of the previous mouse.
        /// </summary>
        /// <value>The state of the previous mouse.</value>
        public MouseState PreviousMouseState { get; set; }
        /// <summary>
        /// Gets or sets the state of the current key board.
        /// </summary>
        /// <value>The state of the current key board.</value>
        public KeyboardState CurrentKeyBoardState { get; set; }
        /// <summary>
        /// Gets or sets the state of the previous key board.
        /// </summary>
        /// <value>The state of the previous key board.</value>
        public KeyboardState PreviousKeyBoardState { get; set; }
        /// <summary>
        /// Gets or sets the main menu image.
        /// </summary>
        /// <value>The main menu image.</value>
        public Sprite MainMenuImage { get; set; }
        /// <summary>
        /// Gets or sets the difficulty page.
        /// </summary>
        /// <value>The difficulty page.</value>
        public DifficultyPage DifficultyPage { get; set; }
        /// <summary>
        /// Gets or sets the level page.
        /// </summary>
        /// <value>The level page.</value>
        public LevelPage LevelPage { get; set; }
        /// <summary>
        /// Gets or sets the game page.
        /// </summary>
        /// <value>The game page.</value>
        public GamePage GamePage { get; set; }
        /// <summary>
        /// Gets or sets the finish page.
        /// </summary>
        /// <value>The finish page.</value>
        public FinishPage FinishPage { get; set; }
        /// <summary>
        /// Gets or sets the pause page.
        /// </summary>
        /// <value>The pause page.</value>
        public PausePage PausePage { get; set; }
        /// <summary>
        /// Gets or sets the score page.
        /// </summary>
        /// <value>The score page.</value>
        public ScorePage ScorePage { get; set; }
        public InstructionPage InstructionPage { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakOut"/> class.
        /// </summary>
        public BreakOut() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            //Screen Settings
            this.ScreenWidth = DEFAULT_WINDOWS_WIDTH;
            this.ScreenHeight = DEFAULT_WINDOWS_HEIGHT;
            //Sprites
            this.MainMenuImage = new Sprite(0, 0, this.ScreenWidth, this.ScreenHeight, 0, 0, 0);
            //Pages
            this.DifficultyPage = new DifficultyPage(graphics, this.ScreenWidth, this.ScreenHeight);
            this.DifficultyPage.Initialize();
            this.LevelPage = new LevelPage(graphics, this.ScreenWidth, this.ScreenHeight);
            this.LevelPage.Initialize();
            this.GamePage = new GamePage(graphics, this.ScreenWidth, this.ScreenHeight, DEFAULT_START_LIVES);
            this.GamePage.Initialize();
            this.FinishPage = new FinishPage(graphics, this.ScreenWidth, this.ScreenHeight);
            this.FinishPage.Initialize();
            this.PausePage = new PausePage(graphics, this.ScreenWidth, this.ScreenHeight);
            this.PausePage.Initialize();
            this.ScorePage = new ScorePage(graphics, this.ScreenWidth, this.ScreenHeight, "highScores",8);
            this.ScorePage.Initialize();
            this.InstructionPage = new InstructionPage(graphics, this.ScreenWidth, this.ScreenHeight);
            this.InstructionPage.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loading the game content
            MainMenuImage.LoadContent(this.Content, "Breakout");
            DifficultyPage.LoadContent(this.Content);
            LevelPage.LoadContent(this.Content);
            GamePage.LoadContent(this.Content);
            FinishPage.LoadContent(this.Content);
            PausePage.LoadContent(this.Content);
            ScorePage.LoadContent(this.Content);
            InstructionPage.LoadContent(this.Content);

            //Inputs
            this.CurrentKeyBoardState = Keyboard.GetState();
            this.PreviousKeyBoardState = this.CurrentKeyBoardState;
            this.CurrentMouseState = Mouse.GetState();
            this.PreviousMouseState = this.CurrentMouseState;

            //Sounds
            effectVictory = Content.Load<SoundEffect>("Sound/win");
            effectDefeat = Content.Load<SoundEffect>("Sound/LAUGH");

            //Music
            song = Content.Load<SoundEffect>("Sound/415384_Nyan");
            nyan = song.CreateInstance();
            nyan.IsLooped = true;
            nyan.Volume = 0.5f;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Inputs
            this.PreviousMouseState = this.CurrentMouseState;
            this.CurrentMouseState = Mouse.GetState();
            this.PreviousKeyBoardState = this.CurrentKeyBoardState;
            this.CurrentKeyBoardState = Keyboard.GetState();

            if ((this.CurrentKeyBoardState.IsKeyDown(Keys.Escape) && this.PreviousKeyBoardState.IsKeyUp(Keys.Escape)))
            {
                nyan.Stop();
                this.Exit();
            }

            //Update logic
            switch (CurrentGameState) {
                case GameState.MainMenu:
                    if ((this.CurrentMouseState.LeftButton == ButtonState.Pressed && this.PreviousMouseState.LeftButton == ButtonState.Released)
                        || (this.CurrentKeyBoardState.IsKeyDown(Keys.Space) && this.PreviousKeyBoardState.IsKeyUp(Keys.Space))) {
                        CurrentGameState = GameState.DifficultySelection;
                    }
                    if (this.CurrentKeyBoardState.IsKeyDown(Keys.I)) {
                        CurrentGameState = GameState.Instruction;
                    }
                    break;
                case GameState.DifficultySelection:
                    this.UpdateDifficultySelection(gameTime);
                    break;
                case GameState.LevelSelection:
                    this.UpdateLevelSelection(gameTime);
                    break;
                case GameState.InPlay:
                    this.UpdateInPlayPage(gameTime);
                    break;
                case GameState.Finish:
                    UpdateFinishPage(gameTime);
                    break;
                case GameState.Pause:
                    UpdatePausePage(gameTime);
                    break;
                case GameState.Score:
                    ScorePage.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    if (this.IsGoingBack()) {
                        CurrentGameState = GameState.DifficultySelection;
                    }
                    if (ScorePage.ButtonReturn.IsClicked) {
                        ScorePage.ButtonReturn.IsClicked = false;
                        CurrentGameState = GameState.DifficultySelection;
                    }
                    break;
                case GameState.Instruction:
                    InstructionPage.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
                    if (this.IsGoingBack()) {
                        CurrentGameState = GameState.MainMenu;
            }
                    if (InstructionPage.ButtonReturn.IsClicked) {
                        InstructionPage.ButtonReturn.IsClicked = false;
                        CurrentGameState = GameState.MainMenu;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(this.BackgroundColor);

            switch (CurrentGameState) {
                case GameState.MainMenu:
                    MainMenuImage.Draw(this.spriteBatch, gameTime);
                    break;
                case GameState.DifficultySelection:
                    DifficultyPage.Draw(this.spriteBatch, gameTime);
                    break;
                case GameState.LevelSelection:
                    LevelPage.Draw(this.spriteBatch, gameTime);
                    break;
                case GameState.InPlay:
                    GamePage.Draw(this.spriteBatch, gameTime);
                    break;
                case GameState.Finish:
                    FinishPage.Draw(this.spriteBatch, gameTime);
                    break;
                case GameState.Pause:
                    PausePage.Draw(this.spriteBatch, gameTime);
                    break;
                case GameState.Score:
                    ScorePage.Draw(this.spriteBatch, gameTime);
                    break;
                case GameState.Instruction:
                    InstructionPage.Draw(this.spriteBatch, gameTime);
                    break;
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the difficulty selection.
        /// </summary>
        public void UpdateDifficultySelection(GameTime gameTime) {
            DifficultyPage.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);
            if (DifficultyPage.ButtonEasy.IsClicked) {
                GamePage.Difficulty = Difficulty.Easy;
                CurrentGameState = GameState.LevelSelection;
                DifficultyPage.ButtonEasy.IsClicked = false;
            }
            else if (DifficultyPage.ButtonNormal.IsClicked) {
                GamePage.Difficulty = Difficulty.Normal;
                CurrentGameState = GameState.LevelSelection;
                DifficultyPage.ButtonNormal.IsClicked = false;
            }
            else if (DifficultyPage.ButtonHard.IsClicked) {
                GamePage.Difficulty = Difficulty.Hard;
                CurrentGameState = GameState.LevelSelection;
                DifficultyPage.ButtonHard.IsClicked = false;
            }
            else if (DifficultyPage.ButtonHighScores.IsClicked) {
                CurrentGameState = GameState.Score;
                DifficultyPage.ButtonHighScores.IsClicked = false;
            }

            if (this.IsGoingBack()) {
                CurrentGameState = GameState.MainMenu;
            }
            if (DifficultyPage.ButtonReturn.IsClicked) {
                DifficultyPage.ButtonReturn.IsClicked = false;
                CurrentGameState = GameState.MainMenu;
            }
        }

        /// <summary>
        /// Updates the level selection.
        /// </summary>
        public void UpdateLevelSelection(GameTime gameTime) {
            LevelPage.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);

            if (this.IsGoingBack()) {
                CurrentGameState = GameState.DifficultySelection;
            }
            if (LevelPage.ButtonReturn.IsClicked) {
                LevelPage.ButtonReturn.IsClicked = false;
                CurrentGameState = GameState.DifficultySelection;
            }

            for (int i = 0; i < LevelPage.Levels.Length; i++) {
                if (LevelPage.Levels[i].IsClicked) {
                    GamePage.Level = i + 1;
                    GamePage.ChargeLevel(i + 1);
                    GamePage.LoadContent(this.Content);
                    CurrentGameState = GameState.InPlay;
                    LevelPage.Levels[i].IsClicked = false;

                    nyan.Play();
                }
            }
        }

        /// <summary>
        /// Updates the level selection.
        /// </summary>
        public void UpdateInPlayPage(GameTime gametime) {
            GamePage.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);

            GamePage.Update(gametime);
            if (GamePage.Bricks.Count == 0) {
                CurrentGameState = GameState.Finish;
                double brick = GamePage.Score;
                double time = 0;
                double difficulty = 0;
                if (Math.Truncate((double)GamePage.Chrono.TotalSeconds) < TIME_LIMIT) {
                    time = 10 * (TIME_LIMIT - GamePage.Chrono.TotalSeconds);
                    brick = GamePage.Score;
                    GamePage.Score += Convert.ToInt32(time);
                }
                if (GamePage.Difficulty == Difficulty.Normal) {
                    difficulty = 1000;
                }
                else if (GamePage.Difficulty == Difficulty.Hard) {
                    difficulty = 3500;
                }
                FinishPage.Title.Text = string.Format("Win! Difficulty: {0} Bricks : {1} Time:{2} Total : {3}",difficulty, brick, time,GamePage.Score);

                nyan.Stop();
                effectVictory.Play();
                ScorePage.SaveScore(GamePage.Level, GamePage.Score);
            }
            if (GamePage.Lives == 0) {
                CurrentGameState = GameState.Finish;
                effectDefeat.Play();
                FinishPage.Title.Text = string.Format("Try again ? Your Score : {0}", GamePage.Score);
                ScorePage.SaveScore(GamePage.Level, GamePage.Score);
                nyan.Stop();
            }
            if (GamePage.Paused) {
                CurrentGameState = GameState.Pause;
                GamePage.Paused = false;
                nyan.Pause();
            }
        }

        /// <summary>
        /// Updates the finish page.
        /// </summary>
        public void UpdateFinishPage(GameTime gameTime) {
            FinishPage.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);

            if (FinishPage.Replay.IsClicked) {
                GamePage.Reset();
                GamePage.ChargeLevel(GamePage.Level);
                GamePage.LoadContent(this.Content);
                CurrentGameState = GameState.InPlay;
                FinishPage.Replay.IsClicked = false;
                nyan.Play();
            }
            else if (FinishPage.ReturnToSelectLevel.IsClicked) {
                GamePage.Reset();
                GamePage.LoadContent(this.Content);
                CurrentGameState = GameState.LevelSelection;
                FinishPage.ReturnToSelectLevel.IsClicked = false;
            }
            else if (FinishPage.ReturnToSelectDifficulty.IsClicked) {
                GamePage.Reset();
                GamePage.LoadContent(this.Content);
                CurrentGameState = GameState.DifficultySelection;
                FinishPage.ReturnToSelectDifficulty.IsClicked = false;
            }
        }

        /// <summary>
        /// Updates the pause page.
        /// </summary>
        public void UpdatePausePage(GameTime gameTime) {
            PausePage.HandleInput(this.PreviousKeyBoardState, this.CurrentKeyBoardState, this.PreviousMouseState, this.CurrentMouseState);

            if (PausePage.Resume.IsClicked) {
                CurrentGameState = GameState.InPlay;
                PausePage.Resume.IsClicked = false;
                nyan.Play();
            }
            else if (PausePage.ReturnToSelectLevel.IsClicked) {
                GamePage.Reset();
                GamePage.LoadContent(this.Content);
                CurrentGameState = GameState.LevelSelection;
                PausePage.ReturnToSelectLevel.IsClicked = false;
                nyan.Stop();
            }
            else if (PausePage.ReturnToSelectDifficulty.IsClicked) {
                GamePage.Reset();
                GamePage.LoadContent(this.Content);
                CurrentGameState = GameState.DifficultySelection;
                PausePage.ReturnToSelectDifficulty.IsClicked = false;
                nyan.Stop();
            }
        }

        /// <summary>
        /// Determines whether [the user want to go back].
        /// </summary>
        /// <returns><c>true</c> if [is going back]; otherwise, <c>false</c>.</returns>
        public bool IsGoingBack() {
            if ((this.CurrentKeyBoardState.IsKeyDown(Keys.Back) && this.PreviousKeyBoardState.IsKeyUp(Keys.Back))
                        || (this.CurrentMouseState.RightButton == ButtonState.Pressed && this.PreviousMouseState.RightButton == ButtonState.Released)) {
                return true;
            }
            return false;
        }
    }
}
