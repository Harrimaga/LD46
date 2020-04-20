using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class RayOfFrost : Spell
    {

        public RayOfFrost() : base(
            new Animation(0, 2, 5),
            Spells.RAY_OF_FROST_PROJECTILE_SPEED,
            25,
            0,
            Spells.RAY_OF_FROST_PIERCE,
            Spells.RAY_OF_FROST_MANA,
            Spells.RAY_OF_FROST_DAMAGE,
            Spells.RAY_OF_FROST_COOLDOWN,
            Spells.RAY_OF_FROST_NAME,
            Spells.RAY_OF_FROST_DESCRIPTION,
            new List<Effect> { new Effect(EffectType.SPEED, Spells.RAY_OF_FROST_SLOW, Spells.RAY_OF_FROST_DURATION) },
            new Sprite(40, 40, 0, Window.texs[15]),
            new Animation(0, 0, 30),
                21,
                5,
                0,
                0,
                100,
                15,
                0.0f,
                0.0f,
                0.8f)
        {
        }

    }
}
