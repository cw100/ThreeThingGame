using System;
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
        Vector2 position;
        Vector2 velocity;
        Vector2 gravity;
        Vector2 force;
        Vector2 lastPosition;
        Rectangle bigHitBox;
        PlayerIndex playerNumber;
        float playerSpeed;
        State currentState;
        enum State
        {
            Standing,
            Walking,
            Running,
            Jumping,
            Flying
        }
        public void Initialize(Animation playeranimation, float playerspeed, float starthealth, 
            Vector2 startvelocity, Vector2 startgravity, 
            Vector2 startposition, PlayerIndex playernumber)
        {

            playerAnimation = playeranimation;
            playerAnimation.Initialize(1, 1, startposition, 0f, Color.White);
            health = starthealth;
            gravity = startgravity;
            velocity = startvelocity;
            position = startposition;
            currentState = State.Walking;
            playerNumber = playernumber;
            playerSpeed = playerspeed;
            bigHitBox = new Rectangle((int)position.X, (int)position.Y, playerAnimation.frameWidth, playerAnimation.frameHeight);

        }
        public void ScreenCollision()
        {
           position.X = MathHelper.Clamp(position.X, 0 + (playerAnimation.frameWidth / 2), 1920 - (playerAnimation.frameWidth / 2));

           position.Y = MathHelper.Clamp(position.Y, 0 + (playerAnimation.frameHeight / 2), 1080 - (playerAnimation.frameHeight / 2));
            
        }
        public void ApplyGravity(GameTime gameTime)
        {
            velocity += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public void ControllerMove(GameTime gameTime)
        {
            
            velocity.X = GamePad.GetState(playerNumber).ThumbSticks.Left.X  *playerSpeed*(float)gameTime.ElapsedGameTime.TotalSeconds;
            
        }


        public void Update(GameTime gameTime)
        {
            lastPosition = position;
            ControllerMove(gameTime);
                       
            ApplyGravity(gameTime);
            
            position += velocity;

            ScreenCollision();
            bigHitBox.X = (int)position.X;
            bigHitBox.Y =   (int)position.Y;
             
            playerAnimation.position = new Vector2((int)position.X,(int)position.Y);
            playerAnimation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.Draw(spriteBatch);
        }



    }
}
