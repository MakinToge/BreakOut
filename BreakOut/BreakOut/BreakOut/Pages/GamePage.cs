// ***********************************************************************
// Assembly         : BreakOut
// ***********************************************************************
// <copyright file="GamePage.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// The BreakOut namespace.
/// </summary>
namespace BreakOut
{
    /// <summary>
    /// Class GamePage.
    /// </summary>
    public class GamePage : Page
    {

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
        public int Lives
        {
            get { return lives; }
            set
            {
                lives = value;
            }
        }

        public SoundEffect effectBrick;
        public SoundEffect effectPaddle;
        public SoundEffect effectWall;
        public SoundEffect effectLose;

        /// <summary>
        /// Gets or sets the lives sprite.
        /// </summary>
        /// <value>The lives sprite.</value>
        public List<Sprite> LivesSprite { get; set; }
        public TextSprite ScoreSprite { get; set; }
        private int score;

        /// <summary>
        /// Gets or sets the lives.
        /// </summary>
        /// <value>The lives.</value>
        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                this.ScoreSprite.Text = score.ToString();
            }
        }
        public TextSprite ChronoSprite { get; set; }
        private double chrono;
        public double Chrono {
            get { return chrono; }
            set { chrono = value;
            this.ChronoSprite.Text = Math.Truncate(chrono / 1000).ToString();
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
        public Difficulty Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
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
            : base(graphics, screenWidth, screenHeight)
        {
            this.Bricks = new List<Brick>();
            this.Powers = new List<Power>();
            this.LivesSprite = new List<Sprite>();
            
            this.Lives = lives;
            this.ScoreSprite = new TextSprite(29*this.ScreenWidth / 32, this.ScreenHeight / 27, "", Color.White);
            this.Score = 0;
            this.ChronoSprite = new TextSprite(15*this.ScreenWidth / 32, this.ScreenHeight / 27, "", Color.White);
            this.Chrono = 0;
            this.Paused = false;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.Paddle = new Paddle(this.Difficulty, this.ScreenWidth, this.ScreenHeight);
            float ballRadius = this.ScreenHeight / 27;
            float ballPositionX = this.ScreenWidth / 2 + this.ScreenHeight / 36;
            float ballPositionY = this.Paddle.Position.Y - ballRadius;
            this.Ball = new Ball(ballPositionX, ballPositionY, ballRadius, ballRadius, 1, -0.5f, 0.4f, this.ScreenWidth, this.ScreenHeight, this.Difficulty);
            
            //Prepare Launch
            this.PrepareLaunch();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(ContentManager content) {
            this.Content = content;
            Ball.LoadContent(content, "ball");
            Paddle.LoadContent(content, "paddle");
            ScoreSprite.LoadContent(content, "Arial28");
            ChronoSprite.LoadContent(content, "Arial28");
            foreach (Brick item in this.Bricks) {
                item.LoadContent(content, item.BrickImage);
            }
            this.Content = content;
            effectBrick = Content.Load<SoundEffect>("Sound/hit");
            effectPaddle = Content.Load<SoundEffect>("Sound/paddle");
            effectWall = Content.Load<SoundEffect>("Sound/wall");
            effectLose = Content.Load<SoundEffect>("Sound/SHOTGUNRELOAD");

            this.CreateLifeSprite();
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
            Paddle.HandleInput(previousKeyboardState, currentKeyboardState, previousMouseState, currentMouseState);

            if (!this.Launched
                && ((currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
                || (currentKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))) {
                this.Launched = true;
                this.Ball.Direction = new Vector2(0.5f, -0.5f);
            }

            if (!this.Paused
                && (currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released)
                || (currentKeyboardState.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P)))
            {
                this.Paused = true;
            }

        }

        /// <summary>
        /// Updates the page.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
          
        public override void Update(GameTime gametime)
        {
            Ball.Update(gametime,effectWall);
            Paddle.Update(gametime);
            this.Chrono += gametime.ElapsedGameTime.Milliseconds;

            if (!this.Launched)
            {
                float ballPositionX = this.Paddle.Position.X + this.Paddle.Size.X / 2 - this.Ball.Size.X / 2;
                Ball.Position = new Vector2(ballPositionX, this.Ball.StartPosition.Y);
            }

            if (Ball.isOut())
            {
                this.Lives -= 1;
                if (this.Lives != 0)
                {
                    effectLose.Play();
                }
                this.LivesSprite.RemoveAt(this.Lives);
                this.PrepareLaunch();
                this.Launched = false;
                this.Score -= 200;
            }

            Rectangle rectangle = new Rectangle((int)Ball.Position.X, (int)(Ball.Position.Y + (Ball.Size.Y / 2)), (int)Ball.Size.X, (int)(Ball.Size.Y / 2));
            if (Ball.Direction.Y > 0 && Paddle.Rectangle.Intersects(rectangle)) { //touche paddle
                effectPaddle.Play();
                Ball.Direction = this.ComputeDirectionBall(Ball, Paddle);
                if (Ball.Speed < Ball.MaxSpeed)
                {
                    Ball.Speed += Ball.Acceleration;
                }
            }
            

            this.UpdateBrick(gametime,effectBrick);

            for (int i = 0; i < this.Powers.Count; i++)
            {
                this.Powers[i].Update(gametime);
                if (Paddle.Rectangle.Intersects(this.Powers[i].Rectangle))
                {
                    this.ChargePower(this.Powers[i].PowerType);
                    this.Powers.RemoveAt(i);
                }
            }
        }

        public void ChargePower(PowerType powerType)
        {
            switch (powerType)
            {
                case PowerType.PlusOneLife:
                    this.Lives += 1;

                    Vector2 position = this.LivesSprite.Last().Position;
                    Vector2 size = this.LivesSprite.Last().Size;
                    position.X += size.X;
                    this.AddLife(position, size);
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
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Ball.Draw(spriteBatch, gameTime);
            Paddle.Draw(spriteBatch, gameTime);
            
            foreach(Sprite sprite in this.LivesSprite)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
            ScoreSprite.Draw(spriteBatch, gameTime);
            ChronoSprite.Draw(spriteBatch, gameTime);
            foreach (Brick item in this.Bricks) {
                if (!item.Destroyed) {
                    item.Draw(spriteBatch, gameTime);
                }
            }
            foreach (Power item in this.Powers)
            {
                item.Draw(spriteBatch, gameTime);
            }
        }

        /// <summary>
        /// Computes the direction of the ball.
        /// </summary>
        /// <param name="ball">The ball.</param>
        /// <param name="paddle">The paddle.</param>
        /// <returns>Vector2.</returns>
        public Vector2 ComputeDirectionBall(Ball ball, Paddle paddle)
        {
            float directionX = 0;
            float directionY = 0;
            directionX = 1 + 1.5f* Math.Abs((ball.Position.X + ball.Size.X / 2) - (paddle.Position.X + paddle.Size.X / 2)) / (paddle.Size.X / 2);
            if (ball.Position.X + ball.Size.X / 2 < paddle.Position.X + paddle.Size.X / 2) {
                directionX = -directionX;
            }
            directionY = -1;
            return Vector2.Normalize(new Vector2(directionX, directionY));
        }

        /// <summary>
        /// Prepares the launch.
        /// </summary>
        public void PrepareLaunch()
        {
            this.Ball.Position = this.Ball.StartPosition;
            this.Ball.Direction = Vector2.Zero;
            this.Ball.Speed = this.Ball.StartSpeed;
        }

        /// <summary>
        /// Updates the brick.
        /// </summary>
        public void UpdateBrick(GameTime gametime, SoundEffect effect) {
            float x, y;
            for (int i = 0; i < this.Bricks.Count; i++) {
                if (this.Ball.Rectangle.Intersects(this.Bricks[i].Rectangle) && !this.Bricks[i].Destroyed) { // touche brique
                    effect.Play();
                    this.Bricks[i].Hit();
                    this.Score += this.Bricks[i].Value;
                    //Ball Direction
                    
                    x = (this.Bricks[i].Position.X + (this.Bricks[i].Size.X / 2)) - (this.Ball.Position.X + (this.Ball.Size.X / 2));
                    y = (this.Bricks[i].Position.Y + (this.Bricks[i].Size.Y / 2)) - (this.Ball.Position.Y + (this.Ball.Size.Y / 2));
                    float timeXCollision = (this.Ball.Position.X - this.Bricks[i].Position.X) / -this.Ball.Direction.X;
                    float timeYCollision = (this.Bricks[i].Position.Y - this.Ball.Position.Y) / this.Ball.Direction.Y;

                    if ((Math.Abs(x) > Math.Abs(y)) && (timeXCollision > timeYCollision))
                    {
                        this.Ball.Direction = new Vector2(-1 * this.Ball.Direction.X, this.Ball.Direction.Y);
                    }
                    else
                    {
                        this.Ball.Direction = new Vector2(this.Ball.Direction.X, -1 * this.Ball.Direction.Y);
                    }
                    
                    //Power Brick
                    if (this.Bricks[i].Power != PowerType.None) {
                        Power power = new Power(this.Bricks[i].Position.X, this.Bricks[i].Position.Y, this.Bricks[i].Size.X / 4, this.Bricks[i].Size.Y / 2, 0, 1, 0.2f, this.ScreenWidth, this.ScreenHeight, this.Bricks[i].Power);
                        power.LoadContent(this.Content, "Power/" + this.Bricks[i].Power.ToString());
                        this.Powers.Add(power);
                    }

                    //Ball Destroyed
                    if (this.Bricks[i].Destroyed)
                    {
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
        public void ChargeLevel(int level)
        {
            this.Bricks = new List<Brick>();
            this.Powers = new List<Power>();

            switch (level)
            {
                //Level 1
                case 1:
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            Brick brick = new Brick(j * 2 + 1, i * 2 + 5, this.ScreenWidth, this.ScreenHeight, 1, PowerType.None);
                            this.Bricks.Add(brick);
                        }

                    }
                    break;
                //Level 2
                case 2:
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            Brick brick = new Brick(j + 4, i + 4, this.ScreenWidth, this.ScreenHeight, 2, PowerType.None);
                            this.Bricks.Add(brick);
                        }

                    }
                    break;
                //Level 3
                case 3:
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            Brick brick = new Brick(j + 2, i * 7 + 2, this.ScreenWidth, this.ScreenHeight, 1, PowerType.None);
                            this.Bricks.Add(brick);
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            Brick brick = new Brick(j + 5, i + 5, this.ScreenWidth, this.ScreenHeight, 2, PowerType.None);
                            this.Bricks.Add(brick);
                        }
                    }
                    break;
                //Level 4
                case 4:
                    for (int k = 0; k < 4; k++)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                Brick brick = new Brick(j + (7 - k * 2), i * (7 - k * 2) + (k + 2), this.ScreenWidth, this.ScreenHeight, 1, PowerType.None);
                                this.Bricks.Add(brick);
                            }
                        }
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                Brick brick = new Brick(j + (13 - k * 2), i * (1 + k * 2) + (5 - k), this.ScreenWidth, this.ScreenHeight, 2, PowerType.None);
                                this.Bricks.Add(brick);
                            }
                        }
                    }
                    break;
                //Level 5
                case 5:
                    this.Bricks = LevelLoader("C:\\Users\\isen\\Source\\Repos\\BreakOut\\BreakOut\\BreakOut\\BreakOut\\test.lvl");
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
        public void Reset()
        {
            this.Launched = false;
            this.PrepareLaunch();
            this.LivesSprite.Clear();
            this.Lives = 3;
            this.Score = 0;
            this.Chrono = 0;
            this.Paddle.setDifficulty(this.Difficulty);
        }

        public List<Brick> LevelLoader(string levelPath)
        {
            List<Brick> bricks = new List<Brick>();
            if (Path.GetExtension(levelPath) != "lvl")
            {
                //maxX = 24   maxY = 24 (real max = 27, but place for paddle)
                string level = File.ReadAllText(levelPath);
                char[] columnSeparator = { ' ' };
                //char[] rowSeparator = Environment.NewLine.ToCharArray();
                char[] rowSeparator = { '\r' };
                string[] rows = level.Split(rowSeparator, 24);//width limit
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] columns = rows[i].Split(columnSeparator, 23);//height limit
                    for (int j = 0; j < columns.Length; j++)
                    {
                        int brickHitPoint;
                        try
                        {
                            brickHitPoint = Convert.ToInt32(columns[j]);
                        }
                        catch (Exception)
                        {

                            brickHitPoint = 0;
                        }

                        if (brickHitPoint != 0)
                        {
                            Brick brick = new Brick(j, i, this.ScreenWidth, this.ScreenHeight, brickHitPoint, PowerType.None);
                            bricks.Add(brick);
                        }
                    }
                }
            }
            return bricks;
        }
       /// <summary>
        /// Add a life to the LifeSprite, ie add a heart on screen.
        /// </summary>
        public void AddLife(Vector2 position, Vector2 size)
        {
            Sprite tmp = new Sprite(position, size, Vector2.Zero, 0);
            tmp.LoadContent(this.Content, "Power/PlusOneLife");
            this.LivesSprite.Add(tmp);
        }

        /// <summary>
        /// Create the LifeSprite with the adequate number of heart.
        /// </summary>
        public void CreateLifeSprite()
        {
            this.LivesSprite.Clear();
        
            for (int i = 0; i < this.lives; i++)
            {
                this.AddLife(new Vector2((i + 1) * this.ScreenHeight / 18, this.ScreenHeight / 18), new Vector2(this.ScreenHeight / 18, this.ScreenHeight / 18));
            }
        }
    }
}
