using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace RoboticRainbowCats
{
    class Animation
    {

        public Texture2D spriteStrip;
        public float scale;
        int elapsedTime;
        public int frameTime;
        public int frameCount;
        public int currentFrame;
        public Color color;
        Rectangle sourceRect = new Rectangle();
        public Rectangle destinationRect = new Rectangle();
        public int FrameWidth;
        public int FrameHeight;
        public bool Active;
        public bool Animated;
        public Vector2 Position;
        public Texture2D spriteTexture;
        public float Angle;
        public int Row;
        public int rowCount;
        public Vector2 origin;



        public List<Texture2D> LoadContent(ContentManager theContentManager, List<string> textureName, int listNum)
        {
            List<Texture2D> startTextureList = new List<Texture2D>();
            for (int i = 0; i < listNum; i++)
            {
                spriteTexture = theContentManager.Load<Texture2D>(textureName[i]);
                startTextureList.Add(spriteTexture);
            }
            List<Texture2D> textureList = startTextureList;
            return textureList;
        }
        public Texture2D LoadContent(ContentManager theContentManager, string textureName)
        {

            spriteTexture = theContentManager.Load<Texture2D>(textureName);

            return spriteTexture;
        }
      
        public void Initialize(int textureNum, List<Texture2D> endTextureList, Vector2 position, int row, int rowcount,
            int frameCount, int frametime, Color color, float scale, bool animated, float angle)
        {
            this.color = color;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;
            Animated = animated;
            Position = position;
            spriteStrip = endTextureList[textureNum];
            this.FrameWidth = spriteStrip.Width / frameCount;
            this.FrameHeight = spriteStrip.Height / rowcount;
            elapsedTime = 0;
            currentFrame = 0;
            Active = true;
            Angle = angle;
            origin = new Vector2(FrameWidth / 2, FrameHeight / 2);
            rowCount = rowcount;
            Row = row - 1;
        }
        public void Initialize(Texture2D texture, Vector2 position, int row, int rowcount,
            int frameCount, int frametime, Color color, float scale, bool animated, float angle)
        {
            this.color = color;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;
            Animated = animated;
            Position = position;
            spriteStrip = texture;
            this.FrameWidth = spriteStrip.Width / frameCount;
            this.FrameHeight = spriteStrip.Height / rowcount;
            elapsedTime = 0;
            currentFrame = 0;
            Active = true;
            Angle = angle;
            Row = row - 1;
            rowCount = rowcount;
            origin = new Vector2(FrameWidth / 2, FrameHeight / 2);

        }

        public void Update(GameTime gameTime)
        {

            if (Active == false)
            {
                currentFrame = 0;
                return;
            }

            if (Animated == false)
            {
                currentFrame = 0;
            }
            if (Animated == true)
            {
                elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTime > frameTime)
                {

                    currentFrame++;

                    if (currentFrame == frameCount)
                    {

                        currentFrame = 0;

                    }

                    elapsedTime = 0;
                }
            }

            sourceRect = new Rectangle(currentFrame * FrameWidth, (int)FrameHeight * Row, FrameWidth, FrameHeight);


            destinationRect = new Rectangle((int)Position.X,
            (int)Position.Y,
            (int)(FrameWidth * scale),
            (int)(FrameHeight * scale));



        }
      
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {

                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color, Angle, origin, SpriteEffects.None, 0);


            }
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRect, Rectangle sourceRect)
        {
            if (Active)
            {

                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color, Angle, origin, SpriteEffects.None, 0);

            }
        }
    }
}
