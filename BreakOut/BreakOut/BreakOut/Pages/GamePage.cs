// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="GamePage.cs" company="">
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
    /// Class GamePage.
    /// </summary>
    public class GamePage : Page {

        /// <summary>
        /// Gets or sets the ball.
        /// </summary>
        /// <value>The ball.</value>
        public Ball Ball { get; set; }
        /// <summary>
        /// Gets or sets the paddle.
        /// </summary>
        /// <value>The paddle.</value>
        public Paddle Paddle { get; set; }
        /// <summary>
        /// Gets or sets the bricks.
        /// </summary>
        /// <value>The bricks.</value>
        public List<Brick> Bricks { get; set; }
        public List<Power> Powers { get; set; }
        /// <summary>
        /// The lives
        /// </summary>
        private int lives;

        /// <summary>
        /// Gets or sets the lives.
        /// </summary>
        /// <value>The lives.</value>
        public int Lives {
            get { return lives; }
            set {
                lives = value;
                this.LivesSprite.Text = lives.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the lives sprite.
        /// </summary>
        /// <value>The lives sprite.</value>
        public TextSprite LivesSprite { get; set; }
        public TextSprite ScoreSprite { get; set; }
        private int score;

        /// <summary>
        /// Gets or sets the lives.
        /// </summary>
        /// <value>The lives.</value>
        public int Score {
            get { return score; }
            set {
                score = value;
                this.ScoreSprite.Text = score.ToString();
            }
        }
        /// <summary>
        /// The difficulty
        /// </summary>
        private Difficulty difficulty;
        /// <summary>
        /// Gets or sets the difficulty.
        /// </summary>
        /// <value>The difficulty.</value>
        public Difficulty Difficulty {
            get {
                return difficulty;
            }
            set {
                difficulty = value;
                this.Ball.setDifficulty(value);
                this.Paddle.setDifficulty(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GamePage"/> is launched.
        /// </summary>
        /// <value><c>true</c> if launched; otherwise, <c>false</c>.</value>
        public bool Launched { get; set; }
        public bool Paused { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }
        public ContentManager Content { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePage"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="lives">The lives.</param>
        public GamePage(GraphicsDeviceManager graphics, int screenWidth, int screenHeight, int lives)
            : base(graphics, screenWidth, screenHeight) {
            this.Bricks = new List<Brick>();
            this.Powers = new List<Power>();
            this.LivesSprite = new TextSprite(this.ScreenHeight / 18, this.ScreenHeight / 18, "", Color.White);
            this.Lives = lives;
            this.ScoreSprite = new TextSprite(this.ScreenHeight / 18, 2 * this.ScreenHeight / 18, "", Color.White);
            this.score = 0;
            this.Paused = false;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize() {
            float ballPositionX = this.ScreenWidth / 2 + this.ScreenHeight / 36;
            float ballPositionY = this.ScreenHeight - 3 * this.ScreenHeight / 18;
            float ballRadius = this.ScreenHeight / 18;
            this.Ball = new Ball(ballPositionX, ballPositionY, ballRadius, ballRadius, 1, -0.5f, 0.4f, this.ScreenWidth, this.ScreenHeight, this.Difficulty);
            this.Paddle = new Paddle(this.Difficulty, this.ScreenWidth, this.ScreenHeight);

            //Prepare Launch
            this.PrepareLaunch();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(ContentManager content) {
            Ball.LoadContent(content, "ball");
            Paddle.LoadContent(content, "paddle");
            LivesSprite.LoadContent(content, "Arial28");
            ScoreSprite.LoadContent(content, "Arial28");
            foreach (Brick item in this.Bricks) {
                item.LoadContent(content, "brick");
            }
            this.Content = content;
        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        /// <param name="previousKeyboardState">State of the previous keyboard.</param>
        /// <param name="currentKeyboardState">State of the current keyboard.</param>
        /// <param name="previousMouseState">State of the previous mouse.</param>
        /// <param name="currentMouseState">State of the current mouse.</param>
        public override void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState, MouseState previousMouseState, MouseState currentMouseState) {
            Paddle.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);

            if (!this.Launched
                && (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
                || (currentKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))) {
                this.Launched = true;
                this.Ball.Direction = new Vector2(0.5f, -0.5f);
            }

            if (!this.Paused
                && (currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released)
                || (currentKeyboardState.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))) {
                this.Paused = true;
            }

        }

        /// <summary>
        /// Updates the page.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        public override void Update(GameTime gametime) {
            Ball.Update(gametime);
            Paddle.Update(gametime);

            if (!this.Launched) {
                float ballPositionX = this.Paddle.Position.X + this.Paddle.Size.X / 2 - this.Ball.Size.X / 2;
                Ball.Position = new Vector2(ballPositionX, this.Ball.StartPosition.Y);
            }

            if (Ball.isOut()) {
                this.Lives -= 1;
                this.PrepareLaunch();
                this.Launched = false;
            }

            Rectangle rectangle = new Rectangle((int)Ball.Position.X, (int)(Ball.Position.Y + (Ball.Size.Y / 2)), (int)Ball.Size.X, (int)(Ball.Size.Y / 2));
            if (Ball.Direction.Y > 0 && Paddle.Rectangle.Intersects(rectangle)) {
                Ball.Direction = this.ComputeDirectionBall(Ball, Paddle);
                if (Ball.Speed < Ball.MaxSpeed) {
                    Ball.Speed += Ball.Acceleration;
                }
            }
            

            this.UpdateBrick(gametime);

            for (int i = 0; i < this.Powers.Count; i++) {
                this.Powers[i].Update(gametime);
                if (Paddle.Rectangle.Intersects(this.Powers[i].Rectangle)) {
                    this.ChargePower(this.Powers[i].PowerType);
                    this.Powers.RemoveAt(i);
                }
            }
        }

        public void ChargePower(PowerType powerType) {
            switch (powerType) {
                case PowerType.PlusOneLife:
                    this.Lives += 1;
                    break;
                case PowerType.Laser:
                    break;
                case PowerType.Larger:
                    this.Paddle.Size = new Vector2(this.Paddle.Size.X + this.Paddle.Size.X / 10, this.Paddle.Size.Y);
                    break;
                case PowerType.Indestructible:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Draws the page.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            Ball.Draw(spriteBatch, gameTime);
            Paddle.Draw(spriteBatch, gameTime);
            LivesSprite.Draw(spriteBatch, gameTime);
            ScoreSprite.Draw(spriteBatch, gameTime);
            foreach (Brick item in this.Bricks) {
                if (!item.Destroyed) {
                    item.Draw(spriteBatch, gameTime);
                }
            }
            foreach (Power item in this.Powers) {
                item.Draw(spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Computes the direction of the ball.
        /// </summary>
        /// <param name="ball">The ball.</param>
        /// <param name="paddle">The paddle.</param>
        /// <returns>Vector2.</returns>
        public Vector2 ComputeDirectionBall(Ball ball, Paddle paddle) {
            float directionX = 0;
            float directionY = 0;
            directionX = 1 + 1.5f* Math.Abs((ball.Position.X + ball.Size.X / 2) - (paddle.Position.X + paddle.Size.X / 2)) / (paddle.Size.X / 2);
            if (ball.Position.X + ball.Size.X / 2 < paddle.Position.X + paddle.Size.X / 2) {
                //directionX = -(ball.Position.X + ball.Size.X / 2 - paddle.Position.X + paddle.Size.X / 2) / paddle.Size.X / 2;
                directionX = -directionX;
            }
            else {
                //directionX = directionX = (ball.Position.X + ball.Size.X / 2 - paddle.Position.X + paddle.Size.X / 2) / paddle.Size.X / 2;

            }

            //directionY = -1 * Ball.Direction.Y;
            directionY = -1;
            return Vector2.Normalize(new Vector2(directionX, directionY));
        }

        /// <summary>
        /// Prepares the launch.
        /// </summary>
        public void PrepareLaunch() {
            this.Ball.Position = this.Ball.StartPosition;
            this.Ball.Direction = Vector2.Zero;
            this.Ball.Speed = this.Ball.StartSpeed;
        }

        /// <summary>
        /// Updates the brick.
        /// </summary>
        public void UpdateBrick(GameTime gametime) {
            float x, y;
            for (int i = 0; i < this.Bricks.Count; i++) {
                if (this.Ball.Rectangle.Intersects(this.Bricks[i].Rectangle) && !this.Bricks[i].Destroyed) {
                    this.Bricks[i].Hit();
                    this.Score += this.Bricks[i].Value;
                    //Ball Direction
                    x = (this.Bricks[i].Position.X + (this.Bricks[i].Size.X / 2)) - (this.Ball.Position.X + (this.Ball.Size.X / 2));
                    y = (this.Bricks[i].Position.Y + (this.Bricks[i].Size.Y / 2)) - (this.Ball.Position.Y + (this.Ball.Size.Y / 2));
                    float timeXCollision = (this.Ball.Position.X - this.Bricks[i].Position.X) / -this.Ball.Direction.X;
                    float timeYCollision = (this.Bricks[i].Position.Y - this.Ball.Position.Y) / this.Ball.Direction.Y;

                    if ((Math.Abs(x) > Math.Abs(y)) && (timeXCollision > timeYCollision)) {
                        this.Ball.Direction = new Vector2(-1 * this.Ball.Direction.X, this.Ball.Direction.Y);
                    }
                    else {
                        this.Ball.Direction = new Vector2(this.Ball.Direction.X, -1 * this.Ball.Direction.Y);
                    }
                    
                    //Power Brick
                    if (this.Bricks[i].Power != PowerType.None) {
                        Power power = new Power(this.Bricks[i].Position.X, this.Bricks[i].Position.Y, this.Bricks[i].Size.X / 2, this.Bricks[i].Size.Y / 2, 0, 1, 0.2f, this.ScreenWidth, this.ScreenHeight, this.Bricks[i].Power);
                        power.LoadContent(this.Content, "Power/" + this.Bricks[i].Power.ToString());
                        this.Powers.Add(power);
                    }

                    //Ball Destroyed
                    if (this.Bricks[i].Destroyed) {
                        this.Bricks.RemoveAt(i);
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Charges the level.
        /// </summary>
        /// <param name="level">The level.</param>
        public void ChargeLevel(int level) {
            this.Bricks = new List<Brick>();
            this.Powers = new List<Power>();
            float brickWidth = 2 * this.ScreenHeight / 18;
            float brickHeight = this.ScreenHeight / 18;
            float unitX = brickWidth;
            float unitY = brickHeight;

            switch (level) {
                //Level 1
                case 1:
                    for (int i = 0; i < 2; i++) {
                        for (int j = 0; j < 7; j++) {
                            Brick brick = new Brick(j * 2 * unitX + unitX, i * 2 * unitY + 5 * unitY, brickWidth, brickHeight, 0, 0, 0, this.ScreenWidth, this.ScreenHeight);
                            this.Bricks.Add(brick);
                        }

                    }
                    this.Bricks[5].Power = PowerType.Larger;
                    this.Bricks[7].Power = PowerType.PlusOneLife;
                    break;
                //Level 2
                case 2:
                    for (int i = 0; i < 7; i++) {
                        for (int j = 0; j < 9; j++) {
                            Brick brick = new Brick(j * unitX + 4 * unitX, i * unitY + 4 * unitY, brickWidth, brickHeight, 0, 0, 0, this.ScreenWidth, this.ScreenHeight);
                            this.Bricks.Add(brick);
                        }

                    }
                    break;
                //Level 3
                case 3:
                    for (int i = 0; i < 2; i++) {
                        for (int j = 0; j < 12; j++) {
                            Brick brick = new Brick(j * unitX + 2 * unitX, i * 7 * unitY + 2 * unitY, brickWidth, brickHeight, 0, 0, 0, this.ScreenWidth, this.ScreenHeight);
                            this.Bricks.Add(brick);
                        }
                    }
                    for (int i = 0; i < 2; i++) {
                        for (int j = 0; j < 6; j++) {
                            Brick brick = new Brick(j * unitX + 5 * unitX, i * unitY + 5 * unitY, brickWidth, brickHeight, 0, 0, 0, this.ScreenWidth, this.ScreenHeight, 2, new string[] { "brick2", "brick" });
                            this.Bricks.Add(brick);
                        }
                    }
                    break;
                //Level 4
                case 4:
                    for (int k = 0; k < 4; k++) {
                        for (int i = 0; i < 2; i++) {
                            for (int j = 0; j < 2; j++) {
                                Brick brick = new Brick(j * unitX + (7 - k * 2) * unitX, i * (7 - k * 2) * unitY + (k + 2) * unitY, brickWidth, brickHeight, 0, 0, 0, this.ScreenWidth, this.ScreenHeight);
                                this.Bricks.Add(brick);
                            }
                        }
                    }
                    for (int k = 0; k < 3; k++) {
                        for (int i = 0; i < 2; i++) {
                            for (int j = 0; j < 2; j++) {
                                Brick brick = new Brick(j * unitX + (13 - k * 2) * unitX, i * (1 + k * 2) * unitY + (5 - k) * unitY, brickWidth, brickHeight, 0, 0, 0, this.ScreenWidth, this.ScreenHeight);
                                this.Bricks.Add(brick);
                            }
                        }
                    }
                    break;
                //Level 5
                case 5:
                    break;
                //Level 6
                case 6:
                    break;
                //Level 7
                case 7:
                    break;
                //Level 8
                case 8:
                    break;
                default:
                    break;
            }

        }


        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset() {
            this.Launched = false;
            this.PrepareLaunch();
            this.Lives = 3;
            this.Score = 0;
            this.Paddle.setDifficulty(this.Difficulty);
        }

        
    }
}
