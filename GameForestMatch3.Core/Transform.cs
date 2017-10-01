using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameForestMatch3.Core
{
    public class Transform/*:GameObject<Transform>*/
    {
        private Vector2 _position;
        private Vector2 _localPosition;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 LocalPosition
        {
            get { return _localPosition; }
            set { _localPosition = value; }
        }

        
    }
}
