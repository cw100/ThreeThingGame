#region Using Statements
using System;
using System.Media;
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
    class Rainbow
    {
        Texture2D rainbowTexture;
        Vector2 position = new Vector2();
        float TTL;
        float colourConstant;
        float timeActive;
        public bool active;
        float Angle;
        float colourNum;
        Color particleColor;
        float Speed;
        public static Color HSL2RGB(double h, double sl, double l)
        {
            double v;
            double r, g, b;

            r = l;
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            Color rgb = new Color();
            rgb.R = Convert.ToByte(r * 255.0f);
            rgb.G = Convert.ToByte(g * 255.0f);
            rgb.B = Convert.ToByte(b * 255.0f);
            return rgb;
        }

        public void Initialize(Texture2D texture, Vector2 mousepos, Random randomizer, int tti, float speed, float angle,PlayerIndex playerNumber)
        {
            Speed = 0;
            timeActive = 0;
            active = true;
            rainbowTexture = texture;
            position = mousepos - new Vector2(texture.Width / 2, texture.Height / 2);
            position += new Vector2(GamePad.GetState(playerNumber).ThumbSticks.Left.Y, GamePad.GetState(playerNumber).ThumbSticks.Left.X) * (randomizer.Next(100) - 50);
            colourConstant = tti;
            Angle = angle;
            TTL = randomizer.Next(tti);
        }
        public void Update(GameTime gametime)
        {
            position += new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * Speed * gametime.ElapsedGameTime.Milliseconds;
            timeActive += gametime.ElapsedGameTime.Milliseconds;
            colourNum = ((timeActive / colourConstant)) - (gametime.ElapsedGameTime.Milliseconds / colourConstant);


            particleColor = HSL2RGB(colourNum, 0.5, 0.5);
            particleColor.A = 255;
            if (TTL < timeActive)
            {
                active = false;
            }

        }
        public void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(rainbowTexture, position, particleColor);

        }

    }
}
