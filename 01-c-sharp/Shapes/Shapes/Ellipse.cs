using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
    class Ellipse : IShape
    {
        public virtual double Length { get; set; }
        public virtual double Width { get; set; }
    }
}
