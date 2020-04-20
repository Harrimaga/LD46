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
        public int particleSprite { get; set; }
        public int particleAmount { get; set; }
        public int pAniStart { get; set; }
        public int pAniStop { get; set; }
        public double pAniDuration { get; set; }
        public float particleSpeed { get; set; }
        public float pr, pb, pg;
        public SpellType spellType { get; set; }

        protected Spell(double mana, double damage, double cooldown, string name, string description, List<Effect> effects, double aOE, Sprite icon, Animation spellAnimation, SpellType spellType, int particleSprite = 17, int particleAmount = 1000, int pAniStart = 0, int pAniStop = 11, double pAniDuration = 2.5, float particleSpeed = 0, float pr = 1, float pg = 1, float pb = 1)
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
            this.particleAmount = particleAmount;
            this.particleSprite = particleSprite;
            this.pAniStart = pAniStart;
            this.pAniStop = pAniStop;
            this.pAniDuration = pAniDuration;
            this.particleSpeed = particleSpeed;
            this.pr = pr;
            this.pg = pg;
            this.pb = pb;
            this.spellType = spellType;
            CooldownSprite = new Sprite(0, 45, 0, Window.texs[2]);
        }

        public void Cast(float x, float y, IEnumerable<Entity> possibleTargets, Entity caster)
        {
            if (CurrentCooldown > 0 || !caster.LoseMana(Mana))
            {
                return;
            }
            CurrentCooldown = Cooldown;
            switch (spellType)
            {
                case SpellType.AOE:
                    AOESpell(x, y, possibleTargets, caster);
                    break;
                case SpellType.SELF_TARGET:
                    SelfTargetSpell(x, y, caster);
                    break;
            }
        }

        private void AOESpell(float x, float y, IEnumerable<Entity> possibleTargets, Entity caster)
        {
            double damage = Damage * caster.GetMagicAmp();
            int targets = 0;
            for (int i = 0; i < particleAmount; i++)
            {
                float px = (float)(x + (Globals.Rng.NextDouble() * 2 - 1) * (AOE - Globals.TileSize / 2));
                float py = (float)(y + (Globals.Rng.NextDouble() * 2 - 1) * (AOE - Globals.TileSize / 2));
                float xd = px - x;
                float yd = py - y;
                double distance = Math.Sqrt(xd * xd + yd * yd);
                if (distance <= ((20 - particleSpeed) / 20) * (AOE - Globals.TileSize / 2))
                {

                    Globals.l.Current.particles.Add(new Particle(px, py, particleSpeed * (float)(((distance) / (AOE - Globals.TileSize / 2)) * xd / distance), particleSpeed * (float)(((distance) / (AOE - Globals.TileSize / 2)) * yd / distance), Globals.TileSize / 2, Globals.TileSize / 2, particleSprite, 0, 30, pr, pg, pb, true, new Animation(pAniStart, pAniStop, pAniDuration)));
                }
            }
            foreach (Entity target in possibleTargets)
            {
                double xd = target.x - x + target.w;
                double yd = target.y - y + target.h;
                if (Math.Sqrt(xd * xd + yd * yd) <= AOE)
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

        private void SelfTargetSpell(float x, float y, Entity caster)
        {
            double damage = Damage * caster.GetMagicAmp();
            for (int i = 0; i < particleAmount; i++)
            {
                float xs = (float)(2 * Globals.Rng.NextDouble() - 1) * particleSpeed;
                float ys = (float)(2 * Globals.Rng.NextDouble() - 1) * particleSpeed;
                float distance = (float)Math.Sqrt(xs * xs + ys * ys);
                xs /= distance;
                ys /= distance;
                Globals.l.Current.particles.Add(new Particle(caster.x + caster.w/2 - Globals.TileSize/4, caster.y + caster.h / 2 - Globals.TileSize / 4, xs, ys, Globals.TileSize / 2, Globals.TileSize / 2, particleSprite, 0, 30, pr, pg, pb, true, new Animation(pAniStart, pAniStop, pAniDuration)));
            }

            caster.DealMagicDamage(damage, Globals.l.p.name, caster.name);
            foreach (Effect effect in Effects)
            {
                caster.TakeEffect((Effect)effect.Clone());
            }
            if (damage != 0)
            {
                if (caster == Globals.l.p)
                {
                    Globals.rootActionLog.DealDamage(caster.name, damage, Name);
                }
                else
                {
                    Globals.rootActionLog.TakeDamage(caster.name, damage, Name);
                }
            }
        }

        public void DrawOnGround(float x, float y)
        {
            Icon.Draw(x, y, true);
        }

        public void Draw(float x, float y)
        {
            Icon.Draw(x, y, false);
            CooldownSprite.w = (int)(190 * CurrentCooldown / Cooldown);
            CooldownSprite.Draw(x - 2, y - 2, false, 0, 0, 0, 0, 0.8f);
            Window.window.DrawText(Name, (int)x + 45, (int)y + 7, Globals.buttonFont);
        }


        public void Update(double deltaTime)
        {
            if (CurrentCooldown - deltaTime < 0)
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
            foreach (Effect e in Effects)
            {
                effects.Add(new Effect(e.Affects, e.Modifier, e.TimeLeft));
            }

            Spell s = new Spell(Mana, Damage, Cooldown, Name, Description, effects, AOE, new Sprite(Icon.w, Icon.h, Icon.num, Icon.texture), new Animation(SpellAnimation.start, SpellAnimation.last, SpellAnimation.time), spellType, particleSprite, particleAmount, pAniStart, pAniStop, pAniDuration, particleSpeed, pr, pg, pb);

            return s;
        }
    }
}
