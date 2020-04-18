﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public abstract class Entity
    {

        protected double MaxHealth, Health, PhysicalAmp, MagicalAmp, speed;
        public int w, h;
        protected float rotation = 0, r = 1, g = 1, b = 1, a = 1;
        public float x, y;
        protected Sprite s, HBarBack, HBar, attack, baseAnimation;
        protected Animation ani = null;
        protected float xDir, yDir;
        protected int attackAnimation = 0;
        public string name;

        public void Init(double Health, float x, float y, int texNum, int attackTexNum, int spriteNum, int w, int h, double speed, double PhysicalAmp = 1, double MagicalAmp = 1)
        {
            this.MaxHealth = Health;
            this.Health = Health;
            this.PhysicalAmp = PhysicalAmp;
            this.MagicalAmp = MagicalAmp;
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.w = w;
            this.h = h;
            s = new Sprite(w, h, spriteNum, Window.texs[texNum]);
            baseAnimation = s;
            HBar = new Sprite(w, h / 8, 0, Window.texs[2]);
            HBarBack = new Sprite(w, h / 8, 0, Window.texs[2]);
            attack = new Sprite(w, h, 0, Window.texs[attackTexNum]);
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

        }

        public virtual void DealPhysicalDamage(double damage, string name, string with)
        {
            Health -= damage;
            Globals.rootActionLog.TakeDamage(name, damage, with);
        }

        public virtual void DealMagicDamage(double damage, string name, string with)
        {
            Health -= damage;
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

    }
}
