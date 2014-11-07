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
    class Menu
        
    {
        Texture2D Logo;
        SpriteFont spriteFont;
        public bool active;
        public void Initialize(Texture2D logo,SpriteFont spritefont)
        {
            Logo = logo;
            spriteFont = spritefont;
            active= true;
        }
        public void Update()
        {
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                var buttonList = new List<Buttons>()
                {
                    {Buttons.A},
                    {Buttons.B},
                    {Buttons.Y},
                    {Buttons.X},
                    {Buttons.Start},
                    {Buttons.Back},
                    {Buttons.RightShoulder},
                    {Buttons.LeftShoulder},
                    {Buttons.RightTrigger},
                    {Buttons.LeftTrigger},
                };

                foreach (var button in buttonList)
                {
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(button))
                        active = false;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Logo, new Vector2(700, 200), Color.White);

            string PressAnyKey = "Press Any Button To Start" ;
            spriteBatch.DrawString(spriteFont, PressAnyKey, new Vector2(850, 900), Color.White);
        }


    }
}
