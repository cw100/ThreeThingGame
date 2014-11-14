#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace RoboticRainbowCats
{
    public class Game1 : Game
    {
        Menu menu;
        EndScreen endScreen;
        string winner;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int VirtualScreenWidth = 1920;
        int VirtualScreenHeight = 1080;
        Collision collision;
        Rainbow playerOneRainbow;
        List<Rainbow> playerOneRainbowList;
        Rainbow playerTwoRainbow;
        List<Rainbow> playerTwoRainbowList;
        Rainbow playerThreeRainbow;
        List<Rainbow> playerThreeRainbowList;
        Rainbow playerFourRainbow;
        List<Rainbow> playerFourRainbowList;

        List<Rectangle> wallHitBoxes;

        Vector2 PlayerOneLastDirection;
        Vector2 PlayerTwoLastDirection;
        Vector2 PlayerThreeLastDirection;
        Vector2 PlayerFourLastDirection;
        Texture2D logo;
        Projectile bullet;
        List<Projectile> playerOneBulletList = new List<Projectile>();
        List<Projectile> playerTwoBulletList= new List<Projectile>();
        List<Projectile> playerThreeBulletList = new List<Projectile>();
        List<Projectile> playerFourBulletList = new List<Projectile>();
        Texture2D particle;
        Wall wall;
        TimeSpan shootSpeed;
        TimeSpan playerOnePreviousFireTime;
        TimeSpan playerTwoPreviousFireTime;
        TimeSpan playerThreePreviousFireTime;
        TimeSpan playerFourPreviousFireTime;
        Animation platformAnimation;
        Vector3 screenScale;
        Player playerOne;
        Vector2 playerOneSpawn;
        Animation playerOneAnimation;
        List<string> playerOneTextureNames;
        List<Texture2D> playerOneTextures;
        Player playerTwo;
        Vector2 playerTwoSpawn;
        Animation playerTwoAnimation;
        List<string> playerTwoTextureNames;
        List<Texture2D> playerTwoTextures;
        Player playerThree;
        Vector2 playerThreeSpawn;
        Animation playerThreeAnimation;
        List<string> playerThreeTextureNames;
        List<Texture2D> playerThreeTextures;
        Player playerFour;
        Vector2 playerFourSpawn;
        Animation playerFourAnimation;
        List<string> playerFourTextureNames;
        List<Texture2D> playerFourTextures;
        FrameRateCounter frameRateCounter;
        SpriteFont fpsFont;
        float playerSpeed;
        int numberOfPlayers = 4;
        bool running = false;
        Texture2D background;
        KeyboardState keyboardState;



        List<Wall> walls = new List<Wall>();


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1080;

            graphics.PreferredBackBufferWidth = 1920;

            
            
           
          

        }
        public void InitializePlayerOne()
        {

            playerOneSpawn = new Vector2(100, 100);
            playerOneAnimation = new Animation();
            playerOne = new Player();
            playerOne.Initialize(playerOneAnimation, playerOneSpawn, PlayerIndex.One, playerSpeed,Color.Red);
        }
        public void InitializePlayerTwo()
        {
            playerTwoSpawn = new Vector2(1920, 100);
            playerTwoAnimation = new Animation();
            playerTwo = new Player();
            playerTwo.Initialize(playerTwoAnimation, playerTwoSpawn, PlayerIndex.Two, playerSpeed,Color.Yellow);
        }
        public void InitializePlayerThree()
        {
            playerThreeSpawn = new Vector2(100 , 980);
            playerThreeAnimation = new Animation();
            playerThree = new Player();
            playerThree.Initialize(playerThreeAnimation, playerThreeSpawn, PlayerIndex.Three, playerSpeed, Color.Blue);
        }
        public void InitializePlayerFour()
        {
            playerFourSpawn = new Vector2(1920, 980);
            playerFourAnimation = new Animation();
            playerFour = new Player();
            playerFour.Initialize(playerFourAnimation, playerFourSpawn, PlayerIndex.Four, playerSpeed, Color.Green);
        }
        public void AddWall()
        {

            wall = new Wall();
            wall.Initialize(graphics.GraphicsDevice, new Vector2(300, 300), 1320, 100);
            walls.Add(wall);
            wall = new Wall();
            wall.Initialize(graphics.GraphicsDevice, new Vector2(300, 700), 1320, 100);
            walls.Add(wall);
            wall = new Wall();
            wall.Initialize(graphics.GraphicsDevice, new Vector2(300, 300), 100, 500);
            walls.Add(wall);
            for(int i = 0; i< walls.Count;i++)
            {
                wallHitBoxes.Add(walls[i].hitBox);
            }

        }
        protected override void Initialize()
        {

            wallHitBoxes = new List<Rectangle>();
            
            endScreen=new EndScreen();
            menu = new Menu();
            logo = Content.Load<Texture2D>("Title.png");
            keyboardState = new KeyboardState();
            AddWall();
            background = Content.Load<Texture2D>("Background.png");
                playerOneRainbowList = new List<Rainbow>();

                playerTwoRainbowList = new List<Rainbow>();

                playerThreeRainbowList = new List<Rainbow>();

                playerFourRainbowList = new List<Rainbow>();
                collision = new Collision();
                shootSpeed = TimeSpan.FromSeconds(.05f);
                playerSpeed = 0.5f;
                float scaleX = (float)GraphicsDevice.Viewport.Width / (float)VirtualScreenWidth;
                float scaleY = (float)GraphicsDevice.Viewport.Height / (float)VirtualScreenHeight;
                screenScale = new Vector3(scaleX, scaleY, 1.0f);
                frameRateCounter = new FrameRateCounter();
                platformAnimation = new Animation();

                particle = Content.Load<Texture2D>("particle.png");



                switch (numberOfPlayers)
                {
                    case 1:

                        InitializePlayerOne();
                        break;
                    case 2:
                        InitializePlayerOne();
                        InitializePlayerTwo();
                        break;
                    case 3:
                        InitializePlayerOne();
                        InitializePlayerTwo();
                        InitializePlayerThree();
                        break;
                    case 4:
                        InitializePlayerOne();
                        InitializePlayerTwo();
                        InitializePlayerThree();
                        InitializePlayerFour();
                        break;
                    default:

                        InitializePlayerOne();
                        break;
                }
            
            base.Initialize();
        }
      
        public void LoadPlayerOne()
        {
            playerOneTextureNames = new List<string>();
            playerOneTextures = new List<Texture2D>();
            playerOneTextureNames.Add("RobotCatHead.png");
            playerOneTextures = playerOneAnimation.LoadContent(this.Content, playerOneTextureNames, playerOneTextureNames.Count);
            playerOneAnimation.Initialize(0, playerOneTextures, playerOneSpawn, 1, 1, 1, 1, Color.White, 1f, false, 0f);


        }
        public void LoadPlayerTwo()
        {
            playerTwoTextureNames = new List<string>();
            playerTwoTextures = new List<Texture2D>();
            playerTwoTextureNames.Add("RobotCatHead.png");
            playerTwoTextures = playerTwoAnimation.LoadContent(this.Content, playerTwoTextureNames, playerTwoTextureNames.Count);
            playerTwoAnimation.Initialize(0, playerTwoTextures, playerTwoSpawn, 1, 1, 1, 1, Color.White, 1f, false, 0f);


        }
        public void LoadPlayerThree()
        {
            playerThreeTextureNames = new List<string>();
            playerThreeTextures = new List<Texture2D>();
            playerThreeTextureNames.Add("RobotCatHead.png");
            playerThreeTextures = playerThreeAnimation.LoadContent(this.Content, playerThreeTextureNames, playerThreeTextureNames.Count);
            playerThreeAnimation.Initialize(0, playerThreeTextures, playerThreeSpawn, 1, 1, 1, 1, Color.White, 1f, false, 0f);


        }
        public void LoadPlayerFour()
        {
            playerFourTextureNames = new List<string>();
            playerFourTextures = new List<Texture2D>();
            playerFourTextureNames.Add("RobotCatHead.png");
            playerFourTextures = playerFourAnimation.LoadContent(this.Content, playerFourTextureNames, playerFourTextureNames.Count);
            playerFourAnimation.Initialize(0, playerFourTextures, playerFourSpawn, 1, 1, 1, 1, Color.White, 1f, false, 0f);


        }

        public bool CheckWin()
        {
            if (playerOne.active == false && playerTwo.active == false && playerThree.active == false)
            {
                winner = "Player Four";
                return true;
            }
            if (playerFour.active == false && playerTwo.active == false && playerThree.active == false)
            {
                winner = "Player One";
                return true;
            }
            if (playerOne.active == false && playerFour.active == false && playerThree.active == false)
            {
                winner = "Player Two";
                return true;
            }
            if (playerOne.active == false && playerTwo.active == false && playerFour.active == false)
            {
                winner = "Player Three";
                return true;
            }
            return false;
        }
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
         
                switch (numberOfPlayers)
                {
                    case 1:

                        LoadPlayerOne();
                        break;
                    case 2:

                        LoadPlayerOne();
                        LoadPlayerTwo();
                        break;
                    case 3:
                        LoadPlayerOne();
                        LoadPlayerTwo();
                        LoadPlayerThree();
                        break;
                    case 4:
                        LoadPlayerOne();
                        LoadPlayerTwo();
                        LoadPlayerThree();
                        LoadPlayerFour();
                        break;
                    default:

                        LoadPlayerOne();
                        break;
                }
            
            fpsFont = Content.Load<SpriteFont>("Arial");
            menu.Initialize(logo, fpsFont);
            endScreen.Initialize(logo, fpsFont);
        }

        protected override void UnloadContent()
        {
        }
        public void Collision()
        {
            Collision bulletCollision = new Collision();


            for (int i = 0; i < playerOneBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerOneBulletList[i].hitBox, playerTwo.bigHitBox))
                {
                    playerTwo.health -= playerOneBulletList[i].projectileDamage;
                    playerOneBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerOneBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerOneBulletList[i].hitBox, playerThree.bigHitBox))
                {
                    playerThree.health -= playerOneBulletList[i].projectileDamage;
                    playerOneBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerOneBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerOneBulletList[i].hitBox, playerFour.bigHitBox))
                {
                    playerFour.health -= playerOneBulletList[i].projectileDamage;
                    playerOneBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerTwoBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerTwoBulletList[i].hitBox, playerOne.bigHitBox))
                {
                    playerOne.health -= playerTwoBulletList[i].projectileDamage;
                    playerTwoBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerTwoBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerTwoBulletList[i].hitBox, playerThree.bigHitBox))
                {
                    playerThree.health -= playerTwoBulletList[i].projectileDamage;
                    playerTwoBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerTwoBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerTwoBulletList[i].hitBox, playerFour.bigHitBox))
                {
                    playerFour.health -= playerTwoBulletList[i].projectileDamage;
                    playerTwoBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerThreeBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerThreeBulletList[i].hitBox, playerOne.bigHitBox))
                {
                    playerOne.health -= playerThreeBulletList[i].projectileDamage;
                    playerThreeBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerThreeBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerThreeBulletList[i].hitBox, playerTwo.bigHitBox))
                {
                    playerTwo.health -= playerThreeBulletList[i].projectileDamage;
                    playerThreeBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerThreeBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerThreeBulletList[i].hitBox, playerFour.bigHitBox))
                {
                    playerFour.health -= playerThreeBulletList[i].projectileDamage;
                    playerThreeBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerFourBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerFourBulletList[i].hitBox, playerOne.bigHitBox))
                {
                    playerOne.health -= playerFourBulletList[i].projectileDamage;
                    playerFourBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerFourBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerFourBulletList[i].hitBox, playerTwo.bigHitBox))
                {
                    playerTwo.health -= playerFourBulletList[i].projectileDamage;
                    playerFourBulletList[i].active = false;

                }
            }
            for (int i = 0; i < playerFourBulletList.Count; i++)
            {
                if (bulletCollision.Intersection(playerFourBulletList[i].hitBox, playerThree.bigHitBox))
                {
                    playerThree.health -= playerFourBulletList[i].projectileDamage;
                    playerFourBulletList[i].active = false;

                }
            }
        }

        
        public void PlayerOneAddBullet()
        {
            bullet = new Projectile();


            bullet.Initialize(particle, 15, 1, playerOne.playerPosition,PlayerOneLastDirection);

                playerOneBulletList.Add(bullet);
            

        }
        public void BulletCollisionWall()
        {
            for (int i = 0; i < playerOneBulletList.Count; i++)
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (walls[j].ProjectileCollision(playerOneBulletList[i].hitBox))
                    {
                        playerOneBulletList[i].active = false;
                    }
                }
            }
            for (int i = 0; i < playerTwoBulletList.Count; i++)
            {
                for (int j = 0; j < walls.Count; j++)
                {

                    if (walls[j].ProjectileCollision(playerTwoBulletList[i].hitBox))
                    {
                        playerTwoBulletList[i].active = false;
                    }
                }
            }
            for (int i = 0; i < playerThreeBulletList.Count; i++)
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (walls[j].ProjectileCollision(playerThreeBulletList[i].hitBox))
                    {
                        playerThreeBulletList[i].active = false;
                    }
                }
            }
            for (int i = 0; i < playerFourBulletList.Count; i++)
            {
                for (int j = 0; j < walls.Count; j++)
                {
                    if (walls[j].ProjectileCollision(playerFourBulletList[i].hitBox))
                    {
                        playerFourBulletList[i].active = false;
                    }
                }
            }
        }

        public void PlayerOneUpdateBullet(GameTime gametime)
        {
            if (playerOne.active)
            {
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X != 0 || GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y != 0)
                {
                    PlayerOneLastDirection = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right;
                }
                if (gametime.TotalGameTime - playerOnePreviousFireTime > shootSpeed)
                {
                    if (GamePad.GetState(PlayerIndex.One).Triggers.Right >= 0.5f)
                    {

                        playerOnePreviousFireTime = gametime.TotalGameTime;
                        PlayerOneAddBullet();
                    }
                }
            }
         }
        public void PlayerTwoAddBullet()
        {
            bullet = new Projectile();


            bullet.Initialize(particle, 15, 1, playerTwo.playerPosition,PlayerTwoLastDirection);

            playerTwoBulletList.Add(bullet);


        }
        public void PlayerTwoUpdateBullet(GameTime gametime)
        {
            if (playerTwo.active)
            {
                if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.X != 0 || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.Y != 0)
                {
                    PlayerTwoLastDirection = GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right;
                }
                if (gametime.TotalGameTime - playerTwoPreviousFireTime > shootSpeed)
                {
                    if (GamePad.GetState(PlayerIndex.Two).Triggers.Right >= 0.5f)
                    {
                       
                        playerTwoPreviousFireTime = gametime.TotalGameTime;
                        PlayerTwoAddBullet();
                    }
                }
            }
        }
        public void PlayerThreeAddBullet()
        {
            bullet = new Projectile();


            bullet.Initialize(particle, 15, 1, playerThree.playerPosition,PlayerThreeLastDirection);

            playerThreeBulletList.Add(bullet);


        }
        public void PlayerThreeUpdateBullet(GameTime gametime)
        {
            if (playerThree.active)
            {
                if (GamePad.GetState(PlayerIndex.Three).ThumbSticks.Right.X != 0 || GamePad.GetState(PlayerIndex.Three).ThumbSticks.Right.Y != 0)
                {
                    PlayerThreeLastDirection = GamePad.GetState(PlayerIndex.Three).ThumbSticks.Right;
                }
                if (gametime.TotalGameTime - playerThreePreviousFireTime > shootSpeed)
                {
                    if (GamePad.GetState(PlayerIndex.Three).Triggers.Right >= 0.5f)
                    {
                        
                        playerThreePreviousFireTime = gametime.TotalGameTime;
                        PlayerThreeAddBullet();
                    }
                }
            }
        }
        public void PlayerFourAddBullet()
        {
            bullet = new Projectile();


            bullet.Initialize(particle, 15, 1, playerFour.playerPosition,PlayerFourLastDirection);

            playerFourBulletList.Add(bullet);


        }
        public void PlayerFourUpdateBullet(GameTime gametime)
        {
            if (playerFour.active)
            {

                if (GamePad.GetState(PlayerIndex.Four).ThumbSticks.Right.X != 0 || GamePad.GetState(PlayerIndex.Four).ThumbSticks.Right.Y != 0)
                {
                    PlayerFourLastDirection = GamePad.GetState(PlayerIndex.Four).ThumbSticks.Right;
                }
                if (gametime.TotalGameTime - playerFourPreviousFireTime > shootSpeed)
                {
                    if (GamePad.GetState(PlayerIndex.Four).Triggers.Right >= 0.5f)
                    {
                        playerFourPreviousFireTime = gametime.TotalGameTime;
                        PlayerFourAddBullet();
                    }
                }
            }
        }
            
        public void PlayerOneAddRainbow()
        {
            if (playerOne.active)
            {
                Random randomizer = new Random();
                for (int i = 0; i < 100; i++)
                {
                    playerOneRainbow = new Rainbow();
                    playerOneRainbow.Initialize(particle, playerOne.playerPosition + new Vector2(0, 0), randomizer, 500, 0.1f, (float)Math.PI, PlayerIndex.One);
                    playerOneRainbowList.Add(playerOneRainbow);
                }
            }
        }
        public void PlayerOneUpdateRainbow(GameTime gameTime)
        {
            if (playerOne.active)
            {
                for (int i = 0; i < playerOneRainbowList.Count - 1; i++)
                {
                    playerOneRainbowList[i].Update(gameTime);
                    if (!playerOneRainbowList[i].active)
                    {
                        playerOneRainbowList.RemoveAt(i);
                    }
                }
            }
        }
        public void PlayerTwoAddRainbow()
        {
            if (playerTwo.active)
            {
                Random randomizer = new Random();
                for (int i = 0; i < 100; i++)
                {
                    playerTwoRainbow = new Rainbow();
                    playerTwoRainbow.Initialize(particle, playerTwo.playerPosition + new Vector2(0, 0), randomizer, 500, 0.1f, (float)Math.PI, PlayerIndex.Two);
                    playerTwoRainbowList.Add(playerTwoRainbow);
                }
            }
        }
        public void PlayerTwoUpdateRainbow(GameTime gameTime)
        {
            if (playerTwo.active)
            {
                for (int i = 0; i < playerTwoRainbowList.Count - 1; i++)
                {
                    playerTwoRainbowList[i].Update(gameTime);
                    if (!playerTwoRainbowList[i].active)
                    {
                        playerTwoRainbowList.RemoveAt(i);
                    }
                }
            }
        }
        public void PlayerThreeAddRainbow()
        {
            if (playerThree.active)
            {
                Random randomizer = new Random();
                for (int i = 0; i < 100; i++)
                {
                    playerThreeRainbow = new Rainbow();
                    playerThreeRainbow.Initialize(particle, playerThree.playerPosition + new Vector2(0, 0), randomizer, 500, 0.1f, (float)Math.PI, PlayerIndex.Three);
                    playerThreeRainbowList.Add(playerThreeRainbow);
                }
            }
        }
        public void PlayerThreeUpdateRainbow(GameTime gameTime)
        {
            if (playerThree.active)
            {
                for (int i = 0; i < playerThreeRainbowList.Count - 1; i++)
                {
                    playerThreeRainbowList[i].Update(gameTime);
                    if (!playerThreeRainbowList[i].active)
                    {
                        playerThreeRainbowList.RemoveAt(i);
                    }
                }
            }
        }
        public void PlayerFourAddRainbow()
        {
            if (playerFour.active)
            {
                Random randomizer = new Random();
                for (int i = 0; i < 100; i++)
                {
                    playerFourRainbow = new Rainbow();
                    playerFourRainbow.Initialize(particle, playerFour.playerPosition + new Vector2(0, 0), randomizer, 500, 0.1f, (float)Math.PI, PlayerIndex.Four);
                    playerFourRainbowList.Add(playerFourRainbow);
                }
            }
        }
        public void PlayerFourUpdateRainbow(GameTime gameTime)
        {
            if(playerFour.active)
            {
            for (int i = 0; i < playerFourRainbowList.Count - 1; i++)
            {
                playerFourRainbowList[i].Update(gameTime);
                if (!playerFourRainbowList[i].active)
                {
                    playerFourRainbowList.RemoveAt(i);
                }
            }
        }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();
            if (running == true)
            {
                frameRateCounter.Update(gameTime);
                PlayerOneUpdateBullet(gameTime);
                for (int i = 0; i < playerOneBulletList.Count; i++)
                {
                    playerOneBulletList[i].Update(gameTime);
                    if (!playerOneBulletList[i].active)
                        playerOneBulletList.RemoveAt(i);
                }
                PlayerTwoUpdateBullet(gameTime);
                for (int i = 0; i < playerTwoBulletList.Count; i++)
                {
                    playerTwoBulletList[i].Update(gameTime);
                    if (!playerTwoBulletList[i].active)
                        playerTwoBulletList.RemoveAt(i);
                }
                PlayerThreeUpdateBullet(gameTime);
                for (int i = 0; i < playerThreeBulletList.Count; i++)
                {
                    playerThreeBulletList[i].Update(gameTime);
                    if (!playerThreeBulletList[i].active)
                        playerThreeBulletList.RemoveAt(i);
                }
                PlayerFourUpdateBullet(gameTime);
                for (int i = 0; i < playerFourBulletList.Count; i++)
                {
                    playerFourBulletList[i].Update(gameTime);
                    if (!playerFourBulletList[i].active)
                        playerFourBulletList.RemoveAt(i);
                }
                switch (numberOfPlayers)
                {
                    case 1:

                        playerOne.Update(gameTime, wallHitBoxes,keyboardState);

                        
                        PlayerOneAddRainbow();
                        PlayerOneUpdateRainbow(gameTime);
                        break;
                    case 2:
                        
                            playerOne.Update(gameTime, wallHitBoxes,keyboardState);


                            playerTwo.Update(gameTime, wallHitBoxes,keyboardState);
                        
                        PlayerOneAddRainbow();
                            PlayerOneUpdateRainbow(gameTime);
                            PlayerTwoAddRainbow();
                            PlayerTwoUpdateRainbow(gameTime);
                        break;
                    case 3:
                        
                            playerOne.Update(gameTime, wallHitBoxes,keyboardState);

                            playerTwo.Update(gameTime, wallHitBoxes,keyboardState);
                            playerThree.Update(gameTime, wallHitBoxes,keyboardState);
                        
                        PlayerOneAddRainbow();
                            PlayerOneUpdateRainbow(gameTime);

                            PlayerTwoAddRainbow();
                            PlayerTwoUpdateRainbow(gameTime);


                            PlayerThreeAddRainbow();
                         
                            PlayerThreeUpdateRainbow(gameTime);
                            
                        break;
                    case 4:
                        
                            playerOne.Update(gameTime, wallHitBoxes,keyboardState);

                            playerTwo.Update(gameTime, wallHitBoxes,keyboardState);
                            playerThree.Update(gameTime, wallHitBoxes,keyboardState);
                            playerFour.Update(gameTime, wallHitBoxes,keyboardState);
                
                PlayerOneAddRainbow();
                        PlayerOneUpdateRainbow(gameTime);

                        PlayerTwoAddRainbow();
                        PlayerTwoUpdateRainbow(gameTime);


                        PlayerThreeAddRainbow();
                        PlayerThreeUpdateRainbow(gameTime);


                        PlayerFourAddRainbow();
                        PlayerFourUpdateRainbow(gameTime);
                        break;
                    
                }
                BulletCollisionWall();
                Collision();
            }
            menu.Update(keyboardState);
            if(menu.active == false&& endScreen.active== false)
            {

                running = true;

            }

            if(CheckWin())
            {
                running = false;
                endScreen.active = true;

                endScreen.Update();

            }
            if(endScreen.restart)
            {
                this.Initialize();
                this.LoadContent();

            }
            
            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            if (running == true)
            {
                switch (numberOfPlayers)
                {
                    case 1:
                        if (playerOne.active)
                        {
                            foreach (Rainbow playerOneRainbow in playerOneRainbowList)
                            {
                                playerOneRainbow.Draw(spriteBatch);
                            }
                        }
                        playerOne.Draw(spriteBatch);
                        break;
                    case 2:
                        if (playerOne.active)
                        {
                            foreach (Rainbow playerOneRainbow in playerOneRainbowList)
                            {
                                playerOneRainbow.Draw(spriteBatch);
                            }
                        }
                        if (playerTwo.active)
                        {
                            foreach (Rainbow playerTwoRainbow in playerTwoRainbowList)
                            {
                                playerTwoRainbow.Draw(spriteBatch);
                            }
                        }
                        playerOne.Draw(spriteBatch);
                        playerTwo.Draw(spriteBatch);
                        break;
                    case 3:
                        if (playerOne.active)
                        {
                            foreach (Rainbow playerOneRainbow in playerOneRainbowList)
                            {
                                playerOneRainbow.Draw(spriteBatch);
                            }
                        }
                        if (playerTwo.active)
                        {

                            foreach (Rainbow playerTwoRainbow in playerTwoRainbowList)
                            {
                                playerTwoRainbow.Draw(spriteBatch);
                            }
                        }
                        if (playerThree.active)
                        {
                            foreach (Rainbow playerThreeRainbow in playerThreeRainbowList)
                            {
                                playerThreeRainbow.Draw(spriteBatch);
                            }
                        }

                        playerOne.Draw(spriteBatch);
                        playerTwo.Draw(spriteBatch);
                        playerThree.Draw(spriteBatch);
                        break;
                    case 4:
                        if (playerOne.active)
                        {
                            foreach (Rainbow playerOneRainbow in playerOneRainbowList)
                            {
                                playerOneRainbow.Draw(spriteBatch);
                            }
                        }
                        if (playerTwo.active)
                        {

                            foreach (Rainbow playerTwoRainbow in playerTwoRainbowList)
                            {
                                playerTwoRainbow.Draw(spriteBatch);
                            }
                        }
                        if (playerThree.active)
                        {
                            foreach (Rainbow playerThreeRainbow in playerThreeRainbowList)
                            {
                                playerThreeRainbow.Draw(spriteBatch);
                            }
                        }
                        if (playerFour.active)
                        {
                            foreach (Rainbow playerFourRainbow in playerFourRainbowList)
                            {
                                playerFourRainbow.Draw(spriteBatch);
                            }
                        }
                        playerOne.Draw(spriteBatch);
                        playerTwo.Draw(spriteBatch);
                        playerThree.Draw(spriteBatch);
                        playerFour.Draw(spriteBatch);
                        break;
                    default:
                        playerOne.Draw(spriteBatch);
                        break;
                }
                foreach (Projectile bullet in playerOneBulletList)
                {
                    bullet.Draw(spriteBatch);
                }
                foreach (Projectile bullet in playerTwoBulletList)
                {
                    bullet.Draw(spriteBatch);
                }
                foreach (Projectile bullet in playerThreeBulletList)
                {
                    bullet.Draw(spriteBatch);
                }
                foreach (Projectile bullet in playerFourBulletList)
                {
                    bullet.Draw(spriteBatch);
                }
                frameRateCounter.Draw();
                string fps = "fps: " + frameRateCounter.frameRate;
                spriteBatch.DrawString(fpsFont, fps, new Vector2(2, 2), Color.White);
                foreach (Wall wall in walls)
                {
                    wall.Draw(spriteBatch);
                }
            }
            if(menu.active)
            { 
            menu.Draw(spriteBatch);
        }
            if(endScreen.active )
            {
                endScreen.Draw(spriteBatch, winner);
            }
                    spriteBatch.End();
                    base.Draw(gameTime);
            
            }
        }
    }

