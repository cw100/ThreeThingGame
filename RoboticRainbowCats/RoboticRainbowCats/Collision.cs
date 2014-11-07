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
    class Collision
    {

        public bool Intersection(Rectangle rectangleOne,Rectangle rectangleTwo)
        {
            if(rectangleOne.Intersects(rectangleTwo))
            {
                return true;
            }
            return false;
        }
    }
}
