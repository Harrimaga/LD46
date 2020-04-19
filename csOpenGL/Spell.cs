using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Spell : ICloneable
    {
        public double Mana { get; set; }
        public double Damage { get; set; }
        public double Cooldown { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Duration { get; set; }
        public List<Effect> Effects { get; set; }
        public double AOE { get; set; }
        public Sprite Icon { get; set; }
        public Sprite CooldownSprite { get; set; }
        public Animation SpellAnimation { get; set; }
        public double CurrentCooldown { get; set; }

        protected Spell(double mana, double damage, double cooldown, string name, string description, List<Effect> effects, double aOE, Sprite icon, Animation spellAnimation)
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
            CooldownSprite = new Sprite(0, 45, 0, Window.texs[2]);
        }

        public void Cast(float x, float y, IEnumerable<Entity> possibleTargets, Entity caster)
        {
            double damage = Damage * caster.GetMagicAmp();
            if(CurrentCooldown > 0 || !caster.LoseMana(Mana))
            {
                return;
            }
            CurrentCooldown = Cooldown;
            int targets = 0;
            foreach (Entity target in possibleTargets)
            {
                if(Math.Abs(target.x-x + target.w) < AOE && Math.Abs(target.y-y + target.h) < AOE)
                {
                    //Deal damage and add the spell effects to the enemies withing AOE
                    target.DealMagicDamage(damage, Globals.l.p.name, caster.name);
                    targets++;
                    foreach (Effect effect in Effects)
                    {
                        target.TakeEffect((Effect)effect.Clone());
                    }
                    if (caster == Globals.l.p)
                    {
                        Globals.rootActionLog.DealDamage(target.name, damage, Name);
                    }
                    else
                    {
                        Globals.rootActionLog.TakeDamage(target.name, damage, Name);
                    }
                }
            }
            Globals.rootActionLog.CastSpell(Name, caster.name, targets);
        }

        public void DrawOnGround(float x, float y)
        {
            Icon.Draw(x, y, true);
        }

        public void Draw(float x, float y)
        {
            Icon.Draw(x, y, false);
            CooldownSprite.w = (int)(190*CurrentCooldown / Cooldown);
            CooldownSprite.Draw(x - 2, y - 2, false, 0, 0, 0, 0, 0.8f);
            Window.window.DrawText(Name, (int)x + 45, (int)y + 7, Globals.buttonFont);
        }


        public void Update(double deltaTime)
        {
            if(CurrentCooldown - deltaTime < 0)
            {
                CurrentCooldown = 0;
            }
            else
            {
                CurrentCooldown -= deltaTime;
            }

            SpellAnimation.Update(Icon, deltaTime);
        }

        public object Clone()
        {
            List<Effect> effects = new List<Effect>();
            foreach(Effect e in Effects)
            {
                effects.Add(new Effect(e.Affects, e.Modifier, e.TimeLeft));
            }

            Spell s = new Spell(Mana, Damage, Cooldown, Name, Description, effects, AOE, new Sprite(Icon.w, Icon.h, Icon.num, Icon.texture), new Animation(SpellAnimation.start, SpellAnimation.last, SpellAnimation.time));

            return s;
        }
    }
}
