using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    public static class Resolutions
    {
        public static List<Vector2u> ResolutionsPossible = new List<Vector2u>()
        {
            new Vector2u(7680, 4320),
            new Vector2u(3840, 2160),
            new Vector2u(2160, 1440),
            new Vector2u(1920, 1080),
            new Vector2u(1600, 900),
            new Vector2u(1440, 900),
            new Vector2u(1366, 768),
            new Vector2u(1360, 768),
            new Vector2u(1280, 720),
            new Vector2u(1024, 768),
            new Vector2u(800, 600),
            new Vector2u(640, 480)
        };
        public static Vector2u GetBigger(Vector2u current)
        {
            int index = ResolutionsPossible.FindIndex(v => v == current);
            if (index - 1 >= 0)
                return ResolutionsPossible[index - 1];
            else
                return ResolutionsPossible[index];
        }
        public static Vector2u GetSmaller(Vector2u current)
        {
            int index = ResolutionsPossible.FindIndex(v => v == current);
            if (index + 1 < ResolutionsPossible.Count)
                return ResolutionsPossible[index + 1];
            else
                return ResolutionsPossible[index];
        }
    }
}
