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
    class EndScreen
    {
        Texture2D Logo;
        SpriteFont spriteFont;
        public bool active;
        public bool restart = false;
        public void Initialize(Texture2D logo, SpriteFont spritefont)
        {
            Logo = logo;
            spriteFont = spritefont;
            active = false;
        }
        public void Update()
        {
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                var buttonList = new List<Buttons>()
                {
                    {Buttons.A}
                   
                };

                foreach (var button in buttonList)
                {
                    if (GamePad.GetState(PlayerIndex.One).IsButtonDown(button))
                    {
                        active = false;
                        restart = true ;
                        
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, string winner)
        {

            spriteBatch.Draw(Logo, new Vector2(700, 200), Color.White);

            string PressAnyKey = winner +" Wins!\nPress A To Play Again";
            spriteBatch.DrawString(spriteFont, PressAnyKey, new Vector2(850, 900), Color.White);
        }


    }
}

