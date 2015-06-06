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
        public List<Ball> Balls { get; set; }
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
        public Chrono Chrono { get; set; }


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
                foreach (Ball ball in this.Balls)
                {
                    ball.setDifficulty(value);
                }
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

        public bool isInvincible { get; set; }
        public float invincibleTimer { get; set; }
        public TextSprite invincibilityTimeTextSprite { get; set; }

        public bool ballIsOnFire { get; set; }
        public float fireTimer { get; set; }

        private short LIMIT_TIMER = 10;

        private short MINIMUM_PADDLE_SIZE = 10;
        private short MAXIMUM_PADDLE_SIZE = 300;
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
            this.Balls = new List<Ball>();
            this.Bricks = new List<Brick>();
            this.Powers = new List<Power>();
            this.LivesSprite = new List<Sprite>();

            this.Lives = lives;
            this.ScoreSprite = new TextSprite(29 * this.ScreenWidth / 32, this.ScreenHeight / 27, "", Color.White);
            this.Score = 0;
            this.ChronoSprite = new TextSprite(15 * this.ScreenWidth / 32, this.ScreenHeight / 27, "", Color.White);
            this.Chrono = new Chrono();
            this.Paused = false;
            this.isInvincible = false;
            this.ballIsOnFire = false;

            this.invincibilityTimeTextSprite = new TextSprite(20 * this.ScreenWidth / 32, this.ScreenHeight / 27, "", Color.White);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            this.Paddle = new Paddle(this.Difficulty, this.ScreenWidth, this.ScreenHeight);

            //Prepare Launch
            this.initFirstBall();
            this.PrepareLaunch();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        public override void LoadContent(ContentManager content)
        {
            this.Content = content;

            foreach (Ball ball in this.Balls)
            {
                ball.LoadContent(content, "ball");
            }

            Paddle.LoadContent(content, "paddle");
            ScoreSprite.LoadContent(content, "Arial28");
            ChronoSprite.LoadContent(content, "Arial28");
            invincibilityTimeTextSprite.LoadContent(content, "Arial28");

            foreach (Brick item in this.Bricks)
            {
                item.LoadContent(content, item.BrickImage);
            }

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
                || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)))
            {
                this.Launched = true;

                foreach (Ball ball in this.Balls)
                {
                    ball.Direction = new Vector2(0.5f, -0.5f);
                }
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
            foreach (Ball ball in this.Balls)
            {
                ball.Update(gametime, effectWall, this.isInvincible);
            }

            Paddle.Update(gametime);
            this.Chrono.Milliseconds += gametime.ElapsedGameTime.Milliseconds;
            this.ChronoSprite.Text = this.Chrono.ToString();

            if (this.isInvincible)
            {
                this.invincibleTimer += (float)gametime.ElapsedGameTime.TotalSeconds;

                if (this.invincibleTimer >= this.LIMIT_TIMER)
                {
                    this.isInvincible = false;
                    this.invincibleTimer = 0;
                }
            }

            if (this.ballIsOnFire)
            {
                this.fireTimer += (float)gametime.ElapsedGameTime.TotalSeconds;

                if (this.fireTimer >= this.LIMIT_TIMER)
                {
                    foreach (Ball ball in this.Balls)
                    {
                        ball.isOnFire = false;
                    }
                    this.ballIsOnFire = false;
                    this.fireTimer = 0;
                }
            }

            List<Ball> toRemove = new List<Ball>();

            foreach (Ball ball in this.Balls)
            {
                if (!this.Launched)
                {
                    float ballPositionX = this.Paddle.Position.X + this.Paddle.Size.X / 2 - ball.Size.X / 2;
                    ball.Position = new Vector2(ballPositionX, ball.StartPosition.Y);
                }

                Rectangle rectangle = new Rectangle((int)ball.Position.X, (int)(ball.Position.Y + (ball.Size.Y / 2)), (int)ball.Size.X, (int)(ball.Size.Y / 2));
                if (ball.Direction.Y > 0 && Paddle.Rectangle.Intersects(rectangle))
                { //touche paddle
                    effectPaddle.Play();
                    ball.Direction = this.ComputeDirectionBall(ball, Paddle);
                    if (ball.Speed < ball.MaxSpeed)
                    {
                        ball.Speed += ball.Acceleration;
                    }
                }

                if (ball.isOut() && !this.isInvincible)
                {
                    if (this.Balls.Count == 1)
                    {
                        this.removeOneLife();

                        this.PrepareLaunch();
                        this.Launched = false;
                        this.Score -= 200;
                    }
                    else
                    {
                        toRemove.Add(ball);
                    }
                }
            }

            foreach (Ball ball in toRemove)
            {
                this.Balls.Remove(ball);
            }

            this.UpdateBrick(gametime, effectBrick);

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
            float newSize = 0;

            switch (powerType)
            {
                case PowerType.PlusOneLife:
                    this.Lives += 1;

                    Vector2 position = this.LivesSprite.Last().Position;
                    Vector2 size = this.LivesSprite.Last().Size;
                    position.X += size.X;
                    this.AddLife(position, size);
                    break;
                case PowerType.MinusOneLife:
                    this.removeOneLife();
                    break;
                case PowerType.OnFire:
                    this.ballIsOnFire = true;

                    foreach (Ball ball in this.Balls)
                    {
                        ball.isOnFire = true;
                    }

                    this.fireTimer = 0;
                    break;
                case PowerType.Faster:
                    foreach (Ball ball in this.Balls)
                    {
                        ball.Speed = ball.MaxSpeed;
                    }
                    break;
                case PowerType.Slower:
                    foreach (Ball ball in this.Balls)
                    {
                        ball.Speed = ball.StartSpeed;
                    }
                    break;
                case PowerType.MultiBall:
                    Ball newBall;
                    List<Ball> tmpBalls = new List<Ball>();

                    foreach (Ball ball in this.Balls)
                    {
                        float ballPositionX = ball.Position.X;
                        float ballPositionY = ball.Position.Y;
                        float ballRadius = ball.Size.X;
                        float speed = ball.Speed;

                        newBall = new Ball(ballPositionX, ballPositionY, ballRadius, ballRadius, 1, -0.5f, speed, this.ScreenWidth, this.ScreenHeight, this.Difficulty);
                        newBall.LoadContent(this.Content, "ball");
                        newBall.StartPosition = ball.StartPosition;
                        newBall.StartSpeed = ball.StartSpeed;
                        tmpBalls.Add(newBall);
                        newBall = new Ball(ballPositionX, ballPositionY, ballRadius, ballRadius, -1, -0.5f, speed, this.ScreenWidth, this.ScreenHeight, this.Difficulty);
                        newBall.LoadContent(this.Content, "ball");
                        newBall.StartPosition = ball.StartPosition;
                        newBall.StartSpeed = ball.StartSpeed;
                        tmpBalls.Add(newBall);
                    }

                    this.Balls = this.Balls.Concat(tmpBalls).ToList();
                    break;
                case PowerType.SmallerPaddle:
                    newSize = this.Paddle.Size.X - this.Paddle.Size.X / 10;
                    if (newSize > MINIMUM_PADDLE_SIZE)
                    {
                        this.Paddle.Size = new Vector2(newSize, this.Paddle.Size.Y);
                    }
                    break;
                case PowerType.LargerPaddle:
                    newSize = this.Paddle.Size.X + this.Paddle.Size.X / 10;
                    if (newSize < MAXIMUM_PADDLE_SIZE)
                    {
                        this.Paddle.Size = new Vector2(newSize, this.Paddle.Size.Y);
                    }
                    break;
                case PowerType.Invicibility:
                    this.isInvincible = true;
                    this.invincibleTimer = 0;
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
            foreach (Brick item in this.Bricks)
            {
                if (!item.Destroyed)
                {
                    item.Draw(spriteBatch, gameTime);
                }
            }

            foreach (Ball ball in this.Balls)
            {
                ball.Draw(spriteBatch, gameTime);
            }

            Paddle.Draw(spriteBatch, gameTime);

            foreach (Sprite sprite in this.LivesSprite)
            {
                sprite.Draw(spriteBatch, gameTime);
            }

            ScoreSprite.Draw(spriteBatch, gameTime);
            ChronoSprite.Draw(spriteBatch, gameTime);
            
            foreach (Power item in this.Powers)
            {
                item.Draw(spriteBatch, gameTime);
            }

            if(this.isInvincible)
            {
                this.invincibilityTimeTextSprite.Text = string.Format("Invinciblity time : {0}",
                    (short)(this.LIMIT_TIMER - this.invincibleTimer));
                this.invincibilityTimeTextSprite.Draw(spriteBatch, gameTime);
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
            directionX = 1 + 1.5f * Math.Abs((ball.Position.X + ball.Size.X / 2) - (paddle.Position.X + paddle.Size.X / 2)) / (paddle.Size.X / 2);
            if (ball.Position.X + ball.Size.X / 2 < paddle.Position.X + paddle.Size.X / 2)
            {
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
            this.Balls[0].Position = this.Balls[0].StartPosition;
            this.Balls[0].Direction = Vector2.Zero;
            this.Balls[0].Speed = this.Balls[0].StartSpeed;
        }

        /// <summary>
        /// Updates the brick.
        /// </summary>
        public void UpdateBrick(GameTime gametime, SoundEffect effect)
        {
            float x, y;

            foreach (Ball ball in this.Balls)
            {
                for (int i = 0; i < this.Bricks.Count; i++)
                {
                    if (ball.Rectangle.Intersects(this.Bricks[i].Rectangle) && !this.Bricks[i].Destroyed)
                    { // touche brique
                        effect.Play();
                        this.Bricks[i].Hit();
                        this.Score += this.Bricks[i].Value;

                        if (!this.ballIsOnFire)
                        {
                            //Ball Direction
                            x = (this.Bricks[i].Position.X + (this.Bricks[i].Size.X / 2)) - (ball.Position.X + (ball.Size.X / 2));
                            y = (this.Bricks[i].Position.Y + (this.Bricks[i].Size.Y / 2)) - (ball.Position.Y + (ball.Size.Y / 2));
                            float timeXCollision = (ball.Position.X - this.Bricks[i].Position.X) / -ball.Direction.X;
                            float timeYCollision = (this.Bricks[i].Position.Y - ball.Position.Y) / ball.Direction.Y;

                            if ((Math.Abs(x) > Math.Abs(y)) && (timeXCollision > timeYCollision))
                            {
                                ball.Direction = new Vector2(-1 * ball.Direction.X, ball.Direction.Y);
                            }
                            else
                            {
                                ball.Direction = new Vector2(ball.Direction.X, -1 * ball.Direction.Y);
                            }
                        }

                        //Ball Destroyed
                        if (this.Bricks[i].Destroyed)
                        {
                            //Power Brick
                            if (this.Bricks[i].Power != PowerType.None)
                            {
                                Power power = new Power(this.Bricks[i].Position.X, this.Bricks[i].Position.Y, this.Bricks[i].Size.X / 2, this.Bricks[i].Size.Y, 0, 1, 0.2f, this.ScreenWidth, this.ScreenHeight, this.Bricks[i].Power);
                                power.LoadContent(this.Content, "Power/" + this.Bricks[i].Power.ToString());
                                this.Powers.Add(power);
                            }

                            this.Bricks.RemoveAt(i);
                        }
                        return;
                    }
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
            this.Bricks = LevelLoader(string.Format("../../../../BreakOutContent/LevelScript/{0}.lvl", level));
        }


        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            this.Launched = false;
            this.initFirstBall();
            this.PrepareLaunch();
            this.LivesSprite.Clear();
            this.Lives = 3;
            this.Score = 0;
            this.Chrono = new Chrono();
            this.Paddle.setDifficulty(this.Difficulty);
        }

        public List<Brick> LevelLoader(string levelPath)
        {
            string level;
            List<Brick> bricks = new List<Brick>();

            try
            {
                if (Path.GetExtension(levelPath) != "lvl")
                {
                    level = File.ReadAllText(levelPath);
                }
                else
                {
                    level = null;
                }
            }
            catch (Exception)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Brick brick = new Brick(j * 2 + 1, i * 2 + 5, this.ScreenWidth, this.ScreenHeight, 1, PowerType.None);
                        brick.Power = PowerType.Invicibility;
                        bricks.Add(brick);
                    }
                }
                return bricks;
            }

            char[] columnSeparator = { ' ' };
            char[] rowSeparator = { '\r' };
            string[] rows = level.Split(rowSeparator, 25);
            for (int i = 2; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(columnSeparator, 29);
                for (int j = 1; j < columns.Length; j++)
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

                    if (brickHitPoint > 0 && brickHitPoint < 8)
                    {
                        Brick brick = new Brick(j - 1, i - 2, this.ScreenWidth, this.ScreenHeight, brickHitPoint, PowerType.None);
                        bricks.Add(brick);
                    }
                }
            }
            PowerType[] powers = (PowerType[])Enum.GetValues(typeof(PowerType));
            string[] probas = rows[0].Split(columnSeparator, powers.Length);
            Random rand = new Random();
            short proba;
            for (int i = 0; i < probas.Length || i < powers.Length - 1; i++)
            {
                foreach (Brick brick in bricks)
                {
                    try
                    {
                        proba = Convert.ToInt16(probas[i]);
                    }
                    catch (Exception)
                    {
                        proba = 0;
                    }

                    if (brick.Power == PowerType.None && rand.Next(99) < proba)
                    {
                        brick.Power = powers[i + 1];
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

        public void removeOneLife()
        {
            this.Lives -= 1;
            if (this.Lives != 0)
            {
                effectLose.Play();
            }
            this.LivesSprite.RemoveAt(this.Lives);
        }

        public void initFirstBall()
        {
            this.Balls.Clear();

            float ballRadius = this.ScreenHeight / 27;
            float ballPositionX = this.ScreenWidth / 2 + this.ScreenHeight / 36;
            float ballPositionY = this.Paddle.Position.Y - ballRadius;

            Ball ball = new Ball(ballPositionX, ballPositionY, ballRadius, ballRadius, 1, -0.5f, 0.4f, this.ScreenWidth, this.ScreenHeight, this.Difficulty);
            this.Balls.Add(ball);
        }
    }
}
