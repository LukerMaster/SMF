using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    interface MenuComponentWithText : MenuComponent
    {
        Font font { get; set; }
    }
}
