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

namespace RoboticRainbowCatsPlatformer
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D t;
        Vector2 globalGravity;
        Vector2 playerJumpHeight;
        float playerSpeed;
        float playerStartHealth;
        Player playerOne;
        Player playerTwo;
        Player playerThree;
        Player playerFour;
        int VirtualScreenWidth = 1920;
        int VirtualScreenHeight = 1080;
        Vector3 screenScale;


        public Game1()
            : base()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1080;

            graphics.PreferredBackBufferWidth = 1920;

           
            Window.IsBorderless = true;
        }
        public void InitializePlayerOne()
        {

            float playerOneSpeed = playerSpeed;
            float playerOneStartHealth = playerStartHealth;
            Vector2 playerOneStartVelocity = new Vector2(0, 0);
            Vector2 playerOneGravity = globalGravity;
            Vector2 playerOneSpawn= new Vector2(0,0);
            Vector2 playerOneJumpHeight = playerJumpHeight;
            playerOne = new Player();
            playerOne.Initialize(playerOneSpeed, playerOneStartHealth, playerOneStartVelocity, playerOneGravity,
               playerOneSpawn, PlayerIndex.One, playerOneJumpHeight);
           
        }
        public void InitializePlayerTwo()
        {

            float playerTwoSpeed = playerSpeed;
            float playerTwoStartHealth = playerStartHealth;
            Vector2 playerTwoStartVelocity = new Vector2(0, 0);
            Vector2 playerTwoGravity = globalGravity;
            Vector2 playerTwoSpawn = new Vector2(200, 0);
            Vector2 playerTwoJumpHeight = playerJumpHeight;
            playerTwo = new Player();
            playerTwo.Initialize(playerTwoSpeed, playerTwoStartHealth, playerTwoStartVelocity, playerTwoGravity,
               playerTwoSpawn, PlayerIndex.Two, playerTwoJumpHeight);

        }
        public void InitializePlayerThree()
        {

            float playerThreeSpeed = 1000f;
            float playerThreeStartHealth = playerStartHealth;
            Vector2 playerThreeStartVelocity = new Vector2(0, 0);
            Vector2 playerThreeGravity = globalGravity;
            Vector2 playerThreeSpawn = new Vector2(400, 0);
            Vector2 playerThreeJumpHeight = playerJumpHeight;
            playerThree = new Player();
            playerThree.Initialize(playerThreeSpeed, playerThreeStartHealth, playerThreeStartVelocity, playerThreeGravity,
               playerThreeSpawn, PlayerIndex.Three, playerThreeJumpHeight);

        }
        public void InitializePlayerFour()
        {

            float playerFourSpeed = playerSpeed;
            float playerFourStartHealth = playerStartHealth;
            Vector2 playerFourStartVelocity = new Vector2(0, 0);
            Vector2 playerFourGravity = globalGravity;
            Vector2 playerFourSpawn = new Vector2(600, 0);
            Vector2 playerFourJumpHeight = playerJumpHeight;
            playerFour = new Player();
            playerFour.Initialize(playerFourSpeed, playerFourStartHealth, playerFourStartVelocity, playerFourGravity,
               playerFourSpawn, PlayerIndex.Four, playerFourJumpHeight);

        }

        
        protected override void Initialize()
        {
            float scaleX = (float)GraphicsDevice.Viewport.Width / (float)VirtualScreenWidth;
            float scaleY = (float)GraphicsDevice.Viewport.Height / (float)VirtualScreenHeight;
               
            screenScale = new Vector3(scaleX, scaleY, 1.0f);

            globalGravity = new Vector2(0, 100f);
            playerSpeed = 1000;
            playerJumpHeight = new Vector2(0, 400f);
            playerStartHealth = 100;


            InitializePlayerOne();
            
            base.Initialize();
            
        }

        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerOne.LoadContent(Content, "RobotCatConverted");
            t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData<Color>(
                new Color[] { Color.White });


        }

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            
            sb.Draw(t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            playerOne.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            Texture2D rectangleTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            
            Color[] color = new Color[1 * 1];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = Color.White;
            }
            rectangleTexture.SetData(color);
            foreach(Rectangle testRectangle in playerOne.testRectangles)
            {
            spriteBatch.Draw(rectangleTexture, testRectangle, Color.Black);
            }
            spriteBatch.Draw(rectangleTexture, playerOne.bigHitBox, Color.Green);

            
            playerOne.Draw(spriteBatch);



            spriteBatch.End();

            base.Draw(gameTime);
        }
        

    }
}
