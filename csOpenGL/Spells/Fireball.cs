using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Fireball : Spell
    {
        public Fireball() : 
            base(
                Spells.FIREBALL_MANA, 
                Spells.FIREBALL_DAMAGE, 
                Spells.FIREBALL_COOLDOWN, 
                Spells.FIREBALL_NAME, 
                Spells.FIREBALL_DESCRIPTION, 
                new List<SpellEffect> {  }, 
                Spells.FIREBALL_AOE, 
                new Sprite(40,40,0, Window.texs[0]), 
                new Animation(0,0,0))
        {
        }
    }
}
