using QuickFont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public static class Globals
    {

        public static List<Enemy> PossibleBosses = null;
        public static Level l = null;
        public const int TileSize = 64;
        public static QFont buttonFont = new QFont("Fonts/arial.ttf", 16, new QuickFont.Configuration.QFontBuilderConfiguration(true));
        public static QFont logFont = new QFont("Fonts/arial.ttf", 10, new QuickFont.Configuration.QFontBuilderConfiguration(true));
        
        public static ActionLog rootActionLog = new ActionLog(15);
        public static List<Theme> Themes = new List<Theme> { new Theme("Space"), new Theme("SpaceDark") };
        public static List<Spell> Spells = new List<Spell> { new Fireball(), new PillarOfLight(), new Slowness(), new Shield(), new RayOfFrost(), new Disable(), new Banishment() };
        public static Random Rng = new Random();
        public static Enemy Boss = null;

        public static bool checkCol(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
        {
            return x1 - w2 < x2 && x1 + w1 > x2 && y1 - h2 < y2 && y1 + h1 > y2;
        }

    }
}
