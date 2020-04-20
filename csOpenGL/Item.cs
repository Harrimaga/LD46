using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public abstract class Item
    {
        public string Name { get; set; }
        public Rarity Rarity { get; set; }
        public string Description { get; set; }
        public Sprite Sprite { get; set; }
        public Effect[] GrantedEffects { get; set; }

        protected Item(string name, Rarity rarity, string description, Sprite sprite, Effect[] grantedEffects)
        {
            Name = name;
            Rarity = rarity;
            Description = description;
            Sprite = sprite;
            GrantedEffects = grantedEffects;
        }

        public virtual bool UseItem(float x, float y, IEnumerable<Entity> possibleTargets, Entity caster)
        {
            return false;
        }

        public virtual void Draw(float x, float y)
        {
            Sprite.Draw(x, y, false);
            Window.window.DrawText(Name, (int)x + 45, (int)y - 2, Globals.buttonFont);
            Window.window.DrawText(Description, (int)x + 45, (int)y+20, Globals.logFont);
        }

        public virtual void DrawOnGround(int x, int y, float rot)
        {
            Sprite.Draw(x, y, true, rot);
        }

    }
}
