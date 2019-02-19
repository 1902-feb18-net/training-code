using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
    class Circle : IShape
    {
        private double _radius = 0;

        public double Length
        {
            get => _radius * 2;
            set
            {
                _radius = value / 2;
            }
        }

        public double Width
        {
            get => _radius * 2;
            set
            {
                _radius = value / 2;
            }
        }
    }
}
