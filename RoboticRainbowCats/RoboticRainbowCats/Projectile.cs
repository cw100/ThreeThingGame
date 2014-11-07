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
    class Projectile
    {
        Vector2 direction;
        Vector2 Position;
        Texture2D projectileTexture;
        public int projectileDamage;
        float projectileSpeed;
        public bool active=true;
        public Rectangle hitBox;

        public void Initialize(Texture2D projetiletexture, int damage, float speed, Vector2 position, Vector2 angle)
        {
            Position = position;
            direction = angle;
            if (direction.X == 0 && direction.Y == 0)
            {
                direction = new Vector2(0, 1);
            }
            direction.Normalize();
            
            projectileDamage = damage;
            projectileSpeed = speed;
            projectileTexture = projetiletexture;
            hitBox = new Rectangle((int)position.X, (int)position.Y, (int)projectileTexture.Width, (int)projectileTexture.Height);
        }
        public void Update(GameTime gametime)
        {
            
            Position += new Vector2(direction.X,-direction.Y )* projectileSpeed * projectileSpeed*gametime.ElapsedGameTime.Milliseconds;
            hitBox.X = (int)Position.X;
            hitBox.Y = (int)Position.Y;

            if (Position.X  > 1920)
            {
                active = false;
            }
            if (Position.Y  > 1080)
            {
                active = false;
            }
            if (Position.X + projectileTexture.Width < 0)
            {
                active = false;
            }
            if (Position.Y + projectileTexture.Height< 0)
            {
                active = false;
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if(active)
                spritebatch.Draw(projectileTexture, Position, new Rectangle(0,0,10,10), Color.Red, 0f, new Vector2(5,5), 4f, SpriteEffects.None, 1f);




        }
    }
}
