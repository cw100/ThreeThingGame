
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
    class Animation
    {
        Texture2D spriteSheet;
        float time;
        float frameTime;
        int frameIndex;
        int totalFrames;
        public int frameHeight;
        public int frameWidth;
        public Vector2 position;
        Vector2 origin;
        Rectangle source;
        float angle;
        Color color;

        public void LoadContent(ContentManager theContentManager, string textureName)
        {

            spriteSheet = theContentManager.Load<Texture2D>(textureName);

            frameHeight = spriteSheet.Height;
            frameWidth = spriteSheet.Width / totalFrames;

        }
        public void Initialize(int totalframes, float animationlength, Vector2 startposition, float startangle, Color startcolor)
        {
            totalFrames = totalframes;
            frameTime = animationlength / totalframes;
            position = startposition;
            angle = startangle;
            color = startcolor;
        }

        public void Update(GameTime gameTime)
        {

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > frameTime)
            {
                frameIndex++;


                if (frameIndex == totalFrames)
                {

                    frameIndex = 0;

                }
                time = 0f;
            }
            if (frameIndex > totalFrames) frameIndex = 1;
             source = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);
             origin = new Vector2(frameWidth / 2.0f, frameHeight/2.0f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, position, source, color, angle,
              origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
