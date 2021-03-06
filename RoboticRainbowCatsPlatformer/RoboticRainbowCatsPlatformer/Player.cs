﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace RoboticRainbowCatsPlatformer
{
    class Player
    {
        Animation playerAnimation;
        bool active;
        float health;
        Vector2 playerPosition;
        Vector2 preJumpPosition;
        Vector2 jumpHeight;
        Vector2 velocity;
        Vector2 lastVelocity;
        Vector2 gravity;
        Vector2 lastPosition;

        public Rectangle testRectangle;
        public List<Rectangle> testRectangles;
        public Rectangle bigHitBox;
        PlayerIndex playerNumber;
        float playerSpeed;
        
        State currentState;
        enum State
        {
            Standing,
            Walking,
            Running,
            Jumping,
            Flying,
            Falling
        }
        public void LoadContent(ContentManager theContentManager, string textureName)
        {
            playerAnimation.LoadContent(theContentManager, textureName);
            bigHitBox = new Rectangle((int)(playerPosition.X - playerAnimation.frameWidth / 2), (int)(playerPosition.Y - playerAnimation.frameHeight/2), playerAnimation.frameWidth, playerAnimation.frameHeight);
           
        }
        public void Initialize(float playerspeed, float starthealth,
            Vector2 startvelocity, Vector2 startgravity,
            Vector2 startposition, PlayerIndex playernumber,
            Vector2 jumpheight)
        {
            testRectangles = new List<Rectangle>();
            testRectangle = new Rectangle(400, 700, 100, 100);
            
            testRectangles.Add(testRectangle);
            testRectangle = new Rectangle(200, 900, 100, 100);

            testRectangles.Add(testRectangle);
            testRectangle = new Rectangle(500, 700, 1000, 100);

            testRectangles.Add(testRectangle);

            playerAnimation = new Animation();
            playerAnimation.Initialize(1, 1, startposition, 0f, Color.White);
            health = starthealth;
            gravity = startgravity;
            velocity = startvelocity;
            playerPosition = startposition;
            currentState = State.Falling;
            playerNumber = playernumber;
            playerSpeed = playerspeed;
            jumpHeight = jumpheight;

        }
        public void ScreenCollision()
        {
            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0 + (playerAnimation.frameWidth / 2), 1920 - (playerAnimation.frameWidth / 2));

            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0 + (playerAnimation.frameHeight / 2), 1080 - (playerAnimation.frameHeight / 2));

        }
        

        public void ControllerMove(GameTime gameTime)
        {
           

                velocity.X += GamePad.GetState(playerNumber).ThumbSticks.Left.X * playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                velocity.X = MathHelper.Clamp(velocity.X, -playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                
            
        }
        public void ApplyFriction(GameTime gameTime)
        {         
            velocity.X *=0.8f;
        }
        public void ApplyGravity(GameTime gameTime)
        {

            velocity.Y += gravity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds ;
                
            

        }
        public void Jump(GameTime gameTime)
        {
            if (currentState != State.Jumping && currentState != State.Falling)
            {
                if (GamePad.GetState(playerNumber).IsButtonDown(Buttons.A))
                {

                    preJumpPosition = playerPosition;
                    currentState = State.Jumping;
                    velocity.Y = -(float)Math.Sqrt((2 * jumpHeight.Y) * (gravity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds));
                }
                
            }
            if (currentState == State.Jumping)
            {

                if (playerPosition.Y < preJumpPosition.Y - jumpHeight.Y)
                {

                    currentState = State.Falling;
                }
                
            }
        }
        public void StateManager()
        {
            if (playerPosition.Y+(playerAnimation.frameHeight/2) >= 1080 )
            {
                currentState = State.Standing;
            }
        }
       
        public bool IsAboveAC(Rectangle collsionHitBox, Vector2 playervector)
        {
            return IsOnUpperSideOfLine(GetBottomRightCorner(collsionHitBox), GetTopLeftCorner(collsionHitBox), playervector);
        }
        public bool IsAboveDB(Rectangle collsionHitBox, Vector2 playervector)
        {
            return IsOnUpperSideOfLine(GetTopRightCorner(collsionHitBox), GetBottomLeftCorner(collsionHitBox), playervector);
        }

        public bool RectangleCollisionTop(Rectangle playerHitBox, Rectangle collsionHitBox)
        {


            if (playerHitBox.Left < collsionHitBox.Right && playerHitBox.Right > collsionHitBox.Left &&
                playerHitBox.Bottom+ velocity.Y >= collsionHitBox.Top && playerHitBox.Top < collsionHitBox.Top &&
                        IsAboveAC(collsionHitBox,GetBottomRightCorner(playerHitBox)) &&
                        IsAboveDB(collsionHitBox, GetBottomLeftCorner(playerHitBox)))
            {
                return true;
            }
            return false;
        }

        public bool RectangleCollisionBottom(Rectangle playerHitBox, Rectangle collsionHitBox)
        {

            if (playerHitBox.Left < collsionHitBox.Right && playerHitBox.Right > collsionHitBox.Left &&
                playerHitBox.Top + velocity.Y <= collsionHitBox.Bottom && playerHitBox.Bottom > collsionHitBox.Bottom &&
             !IsAboveAC(collsionHitBox, GetTopLeftCorner(playerHitBox)) &&
                        !IsAboveDB(collsionHitBox, GetTopRightCorner(playerHitBox)))
            {
                return true;
            }
            return false;
        }
        public bool RectangleCollisionLeft(Rectangle playerHitBox, Rectangle collsionHitBox)
        {


            if (playerHitBox.Right +velocity.X> collsionHitBox.Left && playerHitBox.Left < collsionHitBox.Left  &&
                   IsAboveDB(collsionHitBox, GetTopRightCorner(playerHitBox)) &&
                        !IsAboveAC(collsionHitBox, GetBottomRightCorner(playerHitBox)))
            {
                return true;
            }
            return false;
        }
        public bool RectangleCollisionRight(Rectangle playerHitBox, Rectangle collsionHitBox)
        {
            if (playerHitBox.Left +velocity.X< collsionHitBox.Right && playerHitBox.Right > collsionHitBox.Right &&
                IsAboveAC(collsionHitBox, GetTopLeftCorner(playerHitBox)) &&
                        !IsAboveDB(collsionHitBox, GetBottomLeftCorner(playerHitBox)))
            {
                return true;
            }
            return false;
        }
        public Vector2 GetCenter(Rectangle rect)
        {
            return new Vector2(rect.Center.X, rect.Center.Y);
        }


        public Vector2 GetTopLeftCorner(Rectangle rect)
        {
            return  new Vector2(rect.X, rect.Y);
        }
        public Vector2 GetTopRightCorner(Rectangle rect)
        {
            return  new Vector2(rect.X + rect.Width, rect.Y);
        }
        public Vector2 GetBottomRightCorner(Rectangle rect)
        {
            return new Vector2(rect.X + rect.Width, rect.Y + rect.Height);
        }
        public Vector2 GetBottomLeftCorner(Rectangle rect)
        {
            return new Vector2(rect.X, rect.Y + rect.Height);
        }
        public bool IsOnUpperSideOfLine(Vector2 corner1, Vector2 oppositeCorner, Vector2 playerHitBoxCenter)
        {
            return ((oppositeCorner.X - corner1.X) * ((int)playerHitBoxCenter.Y - corner1.Y) - (oppositeCorner.Y - corner1.Y) * ((int)playerHitBoxCenter.X - corner1.X)) > 0;
        }
        

        public void Update(GameTime gameTime)
        {


           lastVelocity = velocity;
           lastPosition = playerPosition;

            ApplyFriction(gameTime);
            ControllerMove(gameTime);

            
            StateManager();

            ApplyGravity(gameTime);
            Jump(gameTime);
            playerPosition += velocity;
            ScreenCollision();

            bigHitBox.X = (int)(playerPosition.X - playerAnimation.frameWidth / 2);
            bigHitBox.Y = (int)(playerPosition.Y - playerAnimation.frameHeight / 2);


            foreach (Rectangle testRectangle in testRectangles)
            {
                if (RectangleCollisionTop(bigHitBox, testRectangle))
                {
                    playerPosition.Y = testRectangle.Top - playerAnimation.frameHeight / 2;
                    velocity.Y = 0;
                    bigHitBox.X = (int)(playerPosition.X - playerAnimation.frameWidth / 2);
                    bigHitBox.Y = (int)(playerPosition.Y - playerAnimation.frameHeight / 2);
                    if (currentState != State.Jumping || currentState != State.Falling)
                    {
                        currentState = State.Standing;
                    }
                }

                if (RectangleCollisionBottom(bigHitBox, testRectangle))
                {
                    playerPosition.Y = testRectangle.Bottom + playerAnimation.frameHeight / 2;
                    velocity.Y = 0;
                    bigHitBox.X = (int)(playerPosition.X - playerAnimation.frameWidth / 2);
                    bigHitBox.Y = (int)(playerPosition.Y - playerAnimation.frameHeight / 2);

                }

                if (RectangleCollisionLeft(bigHitBox, testRectangle))
                {
                    playerPosition.X = testRectangle.Left - playerAnimation.frameWidth / 2;
                    velocity.X = 0;
                    bigHitBox.X = (int)(playerPosition.X - playerAnimation.frameWidth / 2);
                    bigHitBox.Y = (int)(playerPosition.Y - playerAnimation.frameHeight / 2);
                    playerAnimation.color = Color.Red;

                }
                else
                {
                    playerAnimation.color = Color.White;

                }

                if (RectangleCollisionRight(bigHitBox, testRectangle))
                {
                    playerPosition.X = testRectangle.Right + playerAnimation.frameWidth / 2;
                    velocity.X = 0;
                    bigHitBox.X = (int)(playerPosition.X - playerAnimation.frameWidth / 2);
                    bigHitBox.Y = (int)(playerPosition.Y - playerAnimation.frameHeight / 2);

                }
            }

          
            playerAnimation.position = new Vector2((int)playerPosition.X, (int)playerPosition.Y);
            playerAnimation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            playerAnimation.Draw(spriteBatch);
        }


    }
}
