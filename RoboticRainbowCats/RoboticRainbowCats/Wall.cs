using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboticRainbowCats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    class Wall
    {
        public Rectangle hitBox;
        Texture2D dummyTexture;

        Vector2 wallPosition;
        public void Initialize( GraphicsDevice graphics, Vector2 position,int width, int height)
        {
            hitBox = new Rectangle((int)position.X, (int)position.Y, width, height);
            wallPosition = position;
            dummyTexture = new Texture2D(graphics, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.Black;
            }
            dummyTexture.SetData(data);
    }
        public bool ProjectileCollision(Rectangle rectangle)
        {
            if(hitBox.Intersects(rectangle))
            {
                return true;
            }
            return false;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dummyTexture, wallPosition, Color.White);
        }

    }
}
