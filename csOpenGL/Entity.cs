using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public abstract class Entity
    {

        public double MaxHealth, Health, MaxMana, Mana, PhysicalAmp, MagicalAmp, speed, accuracy;
        public int w, h;
        protected float rotation = 0, r = 1, g = 1, b = 1, a = 1;
        public float x, y;
        protected Sprite s, HBarBack, HBar, attack, baseAnimation;
        protected Animation ani = null;
        protected float xDir, yDir;
        protected int attackAnimation = 0;
        public string name;
        protected double RegenTick { get; set; }
        protected double TimePassed { get; set; }
        public double StandardBlock { get; set; }
        public double CurrentBlock { get; set; }
        public double BlockRegen { get; set; }

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
            s = new Sprite(w, h, spriteNum, Window.texs[texNum]);
            baseAnimation = s;
            HBar = new Sprite(w, h / 8, 0, Window.texs[2]);
            HBarBack = new Sprite(w, h / 8, 0, Window.texs[2]);
            attack = new Sprite(w, h, 0, Window.texs[attackTexNum]);
        }

        public double GetMagicAmp()
        {
            return MagicalAmp;
        }

        public bool LoseMana(double mana)
        {
            if(mana > Mana)
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
        }

        public virtual void DealPhysicalDamage(double damage, string name, string with)
        {
            if(CurrentBlock > damage)
            {
                CurrentBlock -= damage;
                damage = 0;
            }
            else
            {
                damage -= CurrentBlock;
                CurrentBlock = 0;
            }
            Health -= damage;
            if (Health < 0) Health = 0;
            Globals.rootActionLog.TakeDamage(name, damage, with);
        }

        public virtual void DealMagicDamage(double damage, string name, string with)
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
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public void Move(float xa, float ya)
        {
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
            }
        }

        public virtual int[] CheckCollision()
        {
            for(int i = (int)(x/Globals.TileSize); i < (int)(x / Globals.TileSize) + 2 + w/Globals.TileSize && i < Globals.l.Current.width && i > -1; i++)
            {
                for(int j = (int)(y / Globals.TileSize); j < (int)(y / Globals.TileSize) + 2 + h / Globals.TileSize && j < Globals.l.Current.height && j > -1; j++)
                {
                    Tile t = Globals.l.Current.getTile(i, j);
                    if (t.GetWalkable() == Walkable.SOLID)
                    {
                        if (Globals.checkCol((int)x, (int)y, w, h, i * Globals.TileSize, j * Globals.TileSize, Globals.TileSize, Globals.TileSize))
                        {
                            return new int[]{ i, j };
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
