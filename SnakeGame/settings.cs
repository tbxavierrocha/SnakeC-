﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static string directions;

        public settings()
        {
            Width = 16; 
            Height = 16;
            directions = "left";
        }
    }
}
