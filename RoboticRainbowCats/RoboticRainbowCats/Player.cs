using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoboticRainbowCats
{
    class Player
    {
        public Color playerColour;
        public bool active = true;
        public int health = 500;
        Animation playerAnimation;
        float lastAngle;
        Vector2 spawnLocation;
        public Vector2 playerPosition;
        Vector2 startPosition;
        PlayerIndex playerNumber;
        SpriteEffects spriteEffect;
        int jumpHeight;
        float playerSpeed;
        float Gravity;
        float angle;
         int width;
         public Rectangle bigHitBox;

         Vector2 previousPosition;

         int height;


         public void Initialize(Animation playeranimation, Vector2 spawnlocation, PlayerIndex playernumber,
             float playerspeed,Color playercolour)
         {
             playerColour = playercolour;
             playerAnimation = playeranimation;
             spawnLocation = spawnlocation;
             playerPosition = spawnlocation;
             playerNumber = playernumber;
             playerSpeed = playerspeed;
             angle = 0;
             playerAnimation.Position = spawnlocation;
         }
         
       

        public void ScreenCheck()
        {
            if (playerPosition.X + width/2 > 1920)
            {
                playerPosition.X = 1920 - width/2;
            }
            if (playerPosition.Y + height/2 > 1080)
            {
                playerPosition.Y = 1080 - height/2;
            }
            if (playerPosition.X < 0 + width / 2)
            {
                playerPosition.X = 0+width/2;
            }
            if (playerPosition.Y < 0 + height / 2)
            {
                playerPosition.Y = height / 2;
            }
        }
        public void WallCollision(Rectangle rectangle)
        {
            if (bigHitBox.Intersects(rectangle))
            {
                playerPosition = previousPosition;
            }
        }
       public float GetAngle()
        {if(GamePad.GetState(playerNumber).ThumbSticks.Right.Y!=0||GamePad.GetState(playerNumber).ThumbSticks.Right.X!=0)
           {
               return (float)Math.PI / 2 - (float)Math.Atan2(GamePad.GetState(playerNumber).ThumbSticks.Right.Y, GamePad.GetState(playerNumber).ThumbSticks.Right.X);
       }
           else
           {
               return lastAngle;
               }
        }
       public void ControllerMove(GameTime gametime)
       {
            playerPosition += GamePad.GetState(playerNumber).ThumbSticks.Left  * playerSpeed *new Vector2(1,-1) * gametime.ElapsedGameTime.Milliseconds;

        }
       public void KeyboardMove(GameTime gametime, KeyboardState keyboard )
       {
           if(keyboard[Keys.W] == KeyState.Down)
           {
           playerPosition.Y -= playerSpeed *(float)gametime.ElapsedGameTime.TotalMilliseconds;
           }
           if (keyboard[Keys.S] == KeyState.Down)
           {
               playerPosition.Y += playerSpeed * (float)gametime.ElapsedGameTime.TotalMilliseconds;
           }
           if (keyboard[Keys.D] == KeyState.Down)
           {
               playerPosition.X += playerSpeed * (float)gametime.ElapsedGameTime.TotalMilliseconds;
           }
           if (keyboard[Keys.A] == KeyState.Down)
           {
               playerPosition.X -= playerSpeed * (float)gametime.ElapsedGameTime.TotalMilliseconds;
           }
       }
        public void Update(GameTime gametime,List<Rectangle> rectangle,KeyboardState keyboard)
        {

            width = playerAnimation.FrameWidth;
            height = playerAnimation.FrameHeight;
            bigHitBox = new Rectangle((int)playerPosition.X - playerAnimation.FrameWidth / 2, (int)playerPosition.Y - playerAnimation.FrameHeight/2, playerAnimation.FrameWidth, playerAnimation.FrameHeight);
            
            previousPosition = playerPosition;
            ControllerMove(gametime);
            KeyboardMove(gametime, keyboard);
            
            

            bigHitBox.X = (int)playerPosition.X - playerAnimation.FrameWidth / 2;
            bigHitBox.Y = (int)playerPosition.Y - playerAnimation.FrameHeight / 2;
            lastAngle = playerAnimation.Angle;
            playerAnimation.Angle = GetAngle();
            for (int i = 0; i < rectangle.Count;i++ )
            {
                WallCollision(rectangle[i]);
        }
            ScreenCheck();
            playerAnimation.color = playerColour;
            playerAnimation.Position = playerPosition;

            playerAnimation.Update(gametime);
           
            if (health <= 0)
            {
                bigHitBox = new Rectangle();
                active = false;
            }

        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (active)
                playerAnimation.Draw(spritebatch);
            
        }




    }
}
