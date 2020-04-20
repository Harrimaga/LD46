using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public abstract class Entity
    {

        public double MaxHealth, Health, MaxMana, Mana, PhysicalAmp, MagicalAmp, speed, accuracy, knockBackX = 0, knockBackY = 0;
        public int w, h;
        protected float rotation = 0, r = 1, g = 1, b = 1, a = 1;
        public float x, y;
        protected Sprite s, HBarBack, HBar, attack, baseAnimation;
        protected Animation ani = null;
        protected float xDir, yDir;
        protected int attackAnimation = 0;
        public List<Effect> effects;
        public string name;
        public bool stunned;
        protected double RegenTick { get; set; }
        protected double TimePassed { get; set; }
        public double StandardBlock { get; set; }
        public double CurrentBlock { get; set; }
        public double BlockRegen { get; set; }
        public double HealthRegen { get; set; }
        public double ManaRegen { get; set; }

        public void Init(double Health, double Mana, float x, float y, int texNum, int attackTexNum, int spriteNum, int w, int h, double speed, double accuracy, double standardBlock, double PhysicalAmp = 1, double MagicalAmp = 1, double blockRegen = 0.1, double regenTick = 60)
        {
            TimePassed = 0;
            this.MaxHealth = Health;
            StandardBlock = standardBlock;
            CurrentBlock = standardBlock;
            BlockRegen = blockRegen;
            RegenTick = regenTick;
            this.Health = Health;
            this.MaxMana = Mana;
            this.Mana = Mana;
            this.PhysicalAmp = PhysicalAmp;
            this.MagicalAmp = MagicalAmp;
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.w = w;
            this.h = h;
            this.accuracy = accuracy;
            stunned = false;
            s = new Sprite(w, h, spriteNum, Window.texs[texNum]);
            baseAnimation = s;
            HBar = new Sprite(w, h / 8, 0, Window.texs[2]);
            HBarBack = new Sprite(w, h / 8, 0, Window.texs[2]);
            attack = new Sprite(w, h, 0, Window.texs[attackTexNum]);
            effects = new List<Effect>();
        }

        public double GetMagicAmp()
        {
            return MagicalAmp;
        }

        public bool LoseMana(double mana)
        {
            if (mana > Mana)
            {
                return false;
            }
            Mana -= mana;
            return true;
        }

        public virtual void Draw()
        {
            s.Draw(x, y, true, rotation, r, g, b, a);
            HBar.w = (int)(w * Health / MaxHealth);
            HBarBack.Draw(x, y, true, 0, 0, 0, 0);
            HBar.Draw(x, y, true, 0, (float)(1 - Health / MaxHealth), (float)(Health / MaxHealth), 0);
        }

        public virtual void Update(double delta)
        {

            if(ani != null)
            {
                ani.Update(s, delta);
            }
            if (xDir != 0 || yDir != 0)
            {
                rotation = (float)Math.Atan2(xDir, -yDir);
            }
            RegenBlock(delta);
            if (knockBackX > 0)
            {
                knockBackX -= 0.2 * delta;
                if (knockBackX <= 0)
                {
                    knockBackX = 0;
                }
                else
                {
                    if (Move((float)knockBackX, 0))
                    {
                        knockBackX = 0;
                    }
                }
            }
            else if(knockBackX < 0)
            {
                knockBackX += 0.2 * delta;
                if (knockBackX >= 0)
                {
                    knockBackX = 0;
                }
                else
                {
                    if(Move((float)knockBackX, 0))
                    {
                        knockBackX = 0;
                    }
                }
            }
            if (knockBackY > 0)
            {
                knockBackY -= 0.2 * delta;
                if (knockBackY <= 0)
                {
                    knockBackY = 0;
                }
                else
                {
                    if(Move(0, (float)knockBackY))
                    {
                        knockBackY = 0;
                    }
                }
            }
            else if (knockBackY < 0)
            {
                knockBackY += 0.2 * delta;
                if (knockBackY >= 0)
                {
                    knockBackY = 0;
                }
                else
                {
                    if (Move(0, (float)knockBackY))
                    {
                        knockBackY = 0;
                    }
                }
            }
            EffextUpdate(delta);
        }

        public virtual void DealPhysicalDamage(double damage, string name, string with, Entity Attacker = null, double knockBackMod = 1)
        {
            if (CurrentBlock > damage)
            {
                CurrentBlock -= damage;
                damage = 0;
            }
            else
            {
                damage -= CurrentBlock;
                CurrentBlock = 0;
            }
            if (Attacker != null && damage != 0)
            {
                KnockBack(Attacker, knockBackMod, damage);
            }
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public virtual void KnockBack(Entity e, double mod, double damage)
        {
            double xd = e.x + e.w / 2 - x - w / 2;
            double yd = e.y + e.h / 2 - y - h / 2;
            xd *= -1;
            yd *= -1;
            double distance = Math.Sqrt(xd * xd + yd * yd);
            xd /= distance;
            yd /= distance;
            xd *= mod * damage * 25 / MaxHealth;
            yd *= mod * damage * 25 / MaxHealth;
            knockBackX += xd;
            knockBackY += yd;
        }

        public virtual void DealMagicDamage(double damage, string name, string with, Entity Attacker = null, double knockBackMod = 1)
        {
            if (CurrentBlock * 0.8 > damage)
            {
                CurrentBlock -= damage;
                damage = 0;
            }
            else
            {
                damage -= CurrentBlock * 0.8;
                CurrentBlock = 0;
            }
            if (Attacker != null && damage != 0)
            {
                KnockBack(Attacker, knockBackMod, damage);
            }
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public void TakeEffect(Effect effect)
        {
            effects.Add(effect);
            if (!effect.HasExpired(0))
            {
                switch (effect.Affects)
                {
                    case EffectType.HP:
                        double HPpercent = Health / MaxHealth;
                        MaxHealth += effect.Modifier;
                        Health = MaxHealth * HPpercent;
                        break;
                    case EffectType.BLOCK:
                        StandardBlock += effect.Modifier;
                        CurrentBlock += effect.Modifier;
                        break;
                    case EffectType.MAGICAL_DAMAGE:
                        MagicalAmp += effect.Modifier;
                        break;
                    case EffectType.PHYSICAL_DAMAGE:
                        PhysicalAmp += effect.Modifier;
                        break;
                    case EffectType.SPEED:
                        speed *= effect.Modifier;
                        break;
                    case EffectType.MANA:
                        double Manapercent = Mana / MaxMana;
                        MaxMana += effect.Modifier;
                        Mana = MaxMana * Manapercent;
                        break;
                    case EffectType.MPREGEN:
                        ManaRegen += effect.Modifier;
                        break;
                    case EffectType.HPREGEN:
                        HealthRegen += effect.Modifier;
                        break;
                    case EffectType.STUN:
                        stunned = true;
                        break;
                }
            }
        }

        public void EffextUpdate(double delta)
        {
            List<Effect> removables = new List<Effect>();
            foreach (Effect e in effects)
            {
                if (e.HasExpired(delta))
                {
                    removables.Add(e);
                    switch (e.Affects)
                    {
                        case EffectType.HP:
                            double HPpercent = Health / MaxHealth;
                            MaxHealth -= e.Modifier;
                            Health = MaxHealth * HPpercent;
                            break;
                        case EffectType.BLOCK:
                            StandardBlock -= e.Modifier;
                            CurrentBlock -= e.Modifier;
                            if(CurrentBlock < 0)
                            {
                                CurrentBlock = 0;
                            }
                            break;
                        case EffectType.MAGICAL_DAMAGE:
                            MagicalAmp -= e.Modifier;
                            break;
                        case EffectType.PHYSICAL_DAMAGE:
                            PhysicalAmp -= e.Modifier;
                            break;
                        case EffectType.SPEED:
                            speed /= e.Modifier;
                            break;
                        case EffectType.MANA:
                            double Manapercent = Mana / MaxMana;
                            MaxMana -= e.Modifier;
                            Mana = MaxMana * Manapercent;
                            break;
                        case EffectType.MPREGEN:
                            ManaRegen -= e.Modifier;
                            break;
                        case EffectType.HPREGEN:
                            HealthRegen -= e.Modifier;
                            break;
                        case EffectType.STUN:
                            stunned = false;
                            foreach (Effect effect in effects)
                            {
                                if (effect.Affects == EffectType.STUN && effect != e && !removables.Contains(effect))
                                {
                                    stunned = true;
                                }
                            } 
                            break;
                    }
                }
            }

            foreach (Effect effect in removables)
            {
                effects.Remove(effect);
            }
        }

        public bool Move(float xa, float ya)
        {
            bool ret = false;
            x += xa;
            int[] coll = CheckCollision();
            if (coll != null)
            {
                if (xa > 0)
                {
                    x = coll[0] * Globals.TileSize - w;
                }
                else if (xa < 0)
                {
                    x = (1 + coll[0]) * Globals.TileSize;
                }
                ret = true;
            }
            y += ya;
            coll = CheckCollision();
            if (coll != null)
            {
                if (ya > 0)
                {
                    y = coll[1] * Globals.TileSize - h;
                }
                else if (ya < 0)
                {
                    y = (1 + coll[1]) * Globals.TileSize;
                }
                ret = true;
            }
            return ret;
        }

        public virtual int[] CheckCollision()
        {
            for (int i = (int)(x / Globals.TileSize); i < (int)(x / Globals.TileSize) + 2 + w / Globals.TileSize && i < Globals.l.Current.width && i > -1; i++)
            {
                for (int j = (int)(y / Globals.TileSize); j < (int)(y / Globals.TileSize) + 2 + h / Globals.TileSize && j < Globals.l.Current.height && j > -1; j++)
                {
                    Tile t = Globals.l.Current.getTile(i, j);
                    if (t.GetWalkable() == Walkable.SOLID)
                    {
                        if (Globals.checkCol((int)x, (int)y, w, h, i * Globals.TileSize, j * Globals.TileSize, Globals.TileSize, Globals.TileSize))
                        {
                            return new int[] { i, j };
                        }
                    }
                }
            }
            return null;
        }

        protected void RegenBlock(double deltaTime)
        {
            TimePassed += deltaTime;
            if (TimePassed > RegenTick)
            {
                TimePassed -= RegenTick;
                CurrentBlock = CurrentBlock + BlockRegen > StandardBlock ? CurrentBlock : CurrentBlock + BlockRegen;
            }
        }
    }
}
