using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public abstract class Spell
    {
        public double Mana { get; set; }
        public double Damage { get; set; }
        public double Cooldown { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SpellEffect> Effects { get; set; }
        public double AOE { get; set; }
        public Sprite Icon { get; set; }
        public Animation SpellAnimation { get; set; }

        protected Spell(double mana, double damage, double cooldown, string name, string description, List<SpellEffect> effects, double aOE, Sprite icon, Animation spellAnimation)
        {
            Mana = mana;
            Damage = damage;
            Cooldown = cooldown;
            Name = name;
            Description = description;
            Effects = effects;
            AOE = aOE;
            Icon = icon;
            SpellAnimation = spellAnimation;
        }

        public void Cast(float x, float y)
        {
            foreach(Enemy enemy in Globals.l.Current.enemies)
            {
                if(Math.Abs(enemy.x-x) < AOE && Math.Abs(enemy.y-y) < AOE)
                {
                    //Deal damage and add the spell effects to the enemies withing AOE
                    enemy.DealMagicDamage(Damage, Globals.l.p.name, Name);

                }
            }
        }
    }
}
