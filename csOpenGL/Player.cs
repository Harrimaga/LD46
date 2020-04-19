﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Player : Entity
    {

        public double attackTimer = 0, attackSpeed = 0, attackPoint = 1, damage;
        public bool attacked = false, attacking = false, a = false;
        public string name;
        public Sprite UIBack, HBarUI, HBarBackUI, MBarUI, MBarBackUI;
        public List<Item> items = new List<Item>();
        public List<Spell> Spells { get; set; }
        public List<DrawnButton> buttons = new List<DrawnButton>();

        public Player(double Health, double Mana, float x, float y, int texNum, int attackTexNum, int spriteNum, int w, int h, double speed, double attackPoint, double attackSpeed, string name, double damage, double PhysicalAmp, double MagicalAmp)
        {
            Init(Health, Mana, x, y, texNum, attackTexNum, spriteNum, w, h, speed, 1, PhysicalAmp, MagicalAmp);
            Spells = new List<Spell>();
            ani = new Animation(0, 3, 10);
            this.attackSpeed = attackSpeed;
            this.attackPoint = attackPoint;
            this.damage = damage;
            this.name = name;
            UIBack = new Sprite(200, 880, 0, Window.texs[2]);
            HBarUI = new Sprite(w, h / 8, 0, Window.texs[2]);
            HBarBackUI = new Sprite(w, h / 8, 0, Window.texs[2]);
            MBarUI = new Sprite(w, h / 8, 0, Window.texs[2]);
            MBarBackUI = new Sprite(w, h / 8, 0, Window.texs[2]);
            Spells.Add(new Fireball());
        }

        public override void Update(double delta)
        {
            float dis = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (dis != 0)
            {
                xDir /= dis;
                yDir /= dis;
            }
            Move((float)(delta * xDir * speed), (float)(delta * yDir * speed));
            Window.camX = x - 960 + w / 2;
            Window.camY = y - 540 + h / 2;

            for (int i = (int)(x / Globals.TileSize); i < (int)(x / Globals.TileSize) + 2 + w / Globals.TileSize && i < Globals.l.Current.width && i > -1; i++)
            {
                for (int j = (int)(y / Globals.TileSize); j < (int)(y / Globals.TileSize) + 2 + h / Globals.TileSize && j < Globals.l.Current.height && j > -1; j++)
                {
                    Tile t = Globals.l.Current.getTile(i, j);
                    if (t.GetTileType() == TileType.DOOR)
                    {
                        Direction d = Direction.SOUTH;
                        if (i == 0)
                        {
                            d = Direction.WEST;
                        }
                        else if (j == 0)
                        {
                            d = Direction.NORTH;
                        }
                        else if (i == Globals.l.Current.width - 1)
                        {
                            d = Direction.EAST;
                        }

                        foreach (Connection c in Globals.l.Current.Connections)
                        {
                            if (c.Direction == d)
                            {
                                Globals.l.Current = c.Room;
                                Globals.l.Current.visited = true;
                                switch (d)
                                {
                                    case Direction.NORTH:
                                        y = (Globals.l.Current.height - 2.1f) * Globals.TileSize;
                                        x = (Globals.l.Current.Connections.Find((Connection) => { return Connection.Direction == Direction.SOUTH; }).location) * Globals.TileSize;
                                        break;
                                    case Direction.WEST:
                                        x = (Globals.l.Current.width - 2.1f) * Globals.TileSize;
                                        y = (Globals.l.Current.Connections.Find((Connection) => { return Connection.Direction == Direction.EAST; }).location) * Globals.TileSize;
                                        break;
                                    case Direction.SOUTH:
                                        y = Globals.TileSize;
                                        x = (Globals.l.Current.Connections.Find((Connection) => { return Connection.Direction == Direction.NORTH; }).location) * Globals.TileSize;
                                        break;
                                    case Direction.EAST:
                                        x = Globals.TileSize;
                                        y = (Globals.l.Current.Connections.Find((Connection) => { return Connection.Direction == Direction.WEST; }).location) * Globals.TileSize;
                                        break;
                                }
                                Window.camX = x - 960 + w / 2;
                                Window.camY = y - 540 + h / 2;
                            }
                        }
                    }
                }

                HaveItemsExpired(delta);
            }

            base.Update(delta);

            if (a)
            {
                BasicAttack(delta);
            }

            xDir = 0;
            yDir = 0;

        }

        public void SetDir(int x, int y)
        {
            if (x != 0)
            {
                xDir = x;
            }
            if (y != 0)
            {
                yDir = y;
            }
        }

        public void BasicAttack(double delta)
        {
            List<Enemy> enemies = Globals.l.Current.enemies;

            if (attacking)
            {
                attackTimer += delta;
                if (!attacked && attackTimer > attackSpeed * attackPoint)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        float xd = enemy.x + enemy.w / 2 - x - w / 2;
                        float yd = enemy.y + enemy.h / 2 - y - h / 2;
                        float dis = (float)Math.Sqrt(xd * xd + yd * yd);

                        if (dis <= (enemy.w / 2 + w / 2) * 1.2f)
                        {
                            enemy.DealPhysicalDamage(damage * PhysicalAmp, name, "their blade");
                        }
                    }

                    attacked = true;
                    attacking = false;
                    s = baseAnimation;
                    a = false;
                    ani = new Animation(0, 3, 10);
                }
                else if (attackTimer > attackSpeed)
                {
                    attacking = false;
                    s = baseAnimation;
                    a = false;
                    ani = new Animation(0, 3, 10);
                }
            }
            else
            {
                s = attack;
                ani = new Animation(0, 9, attackSpeed / 10);
                attackTimer = 0;
                attacked = false;
                attacking = true;
            }
        }

        public override void DealPhysicalDamage(double damage, string name, string with)
        {
            //Globals.rootActionLog.TakeDamage(name, damage, with);
            base.DealPhysicalDamage(damage, name, with);
        }

        public override void DealMagicDamage(double damage, string name, string with)
        {
            Globals.rootActionLog.TakeDamage(name, damage, with);
            base.DealMagicDamage(damage, name, with);
        }

        public void DrawUI()
        {
            UIBack.w = 200;
            UIBack.h = 880;
            UIBack.Draw(1720, 0, false, 0, 0.5f, 0.5f, 0.5f, 0.85f);
            int y = 5;
            UIBack.w = 190;
            UIBack.h = 45;

            HBarUI.w = (int)(200 * Health / MaxHealth);
            HBarUI.h = 30;
            HBarBackUI.w = 200;
            HBarBackUI.h = 30;
            HBarBackUI.Draw(1720, 755, false, 0, 0, 0, 0);
            HBarUI.Draw(1720, 755, false, 0, (float)(1 - Health / MaxHealth) / 2, (float)(Health / MaxHealth) / 2, 0); 

            MBarUI.w = (int)(200 * Mana / MaxMana);
            MBarUI.h = 30;
            MBarBackUI.w = 200;
            MBarBackUI.h = 30;
            MBarBackUI.Draw(1720, 800, false, 0, 0, 0, 0);
            MBarUI.Draw(1720, 800, false, 0, 0, 1 - (float)(Mana / MaxMana), 1);

            string TextHP = "HP: " + Health + "/" + MaxHealth;
            string TextMP = "MP: " + Mana + "/" + MaxMana;

            Window.window.DrawTextCentered(TextHP, (int)(1720 + (200 / 2)), (int)(750 + (30 / 2) - 12), Globals.buttonFont);
            Window.window.DrawTextCentered(TextMP, (int)(1720 + (200 / 2)), (int)(800 + (30 / 2) - 12), Globals.buttonFont);

            foreach (Item it in items)
            {
                UIBack.Draw(1725, y, false, 0, 0, 0, 0, 0.5f);
                it.Draw(1727, y + 2);
                y += 50;
            }
        }

        public void EquipItem(Item item)
        {
            DrawnButton b = new DrawnButton("", 1725, 5 + 50 * items.Count, 190, 45, () => { DequipItem(item); });
            b.a = 0;
            Game.game.buttons.Add(b);
            buttons.Add(b);

            items.Add(item);
            foreach (Effect e in item.GrantedEffects)
            {
                if (!e.HasExpired(0))
                {
                    switch (e.Affects)
                    {
                        case EffectType.HP:
                            MaxHealth += e.Modifier;
                            break;
                        case EffectType.BLOCK:
                            throw new NotImplementedException("Block is not implemented");
                            break;
                        case EffectType.MAGICAL_DAMAGE:
                            MagicalAmp += e.Modifier;
                            break;
                        case EffectType.PHYSICAL_DAMAGE:
                            PhysicalAmp += e.Modifier;
                            break;
                    }
                }
            }
        }

        public void DequipItem(Item item)
        {
            int i = 0;
            for (i = 0; i < items.Count; i++)
            {
                if(items[i] == item)
                {
                    break;
                }
            }
            DrawnButton b = buttons[i];
            buttons.Remove(b);
            Game.game.buttons.Remove(b);
            Globals.l.Current.DropItem(item, x, y);
            for(int j = i; j < buttons.Count; j++)
            {
                buttons[j].Y -= 50;
            }
            items.Remove(item);
            foreach (Effect e in item.GrantedEffects)
            {
                if (!e.HasExpired(0))
                {
                    switch (e.Affects)
                    {
                        case EffectType.HP:
                            MaxHealth -= e.Modifier;
                            break;
                        case EffectType.BLOCK:
                            throw new NotImplementedException("Block is not implemented");
                            break;
                        case EffectType.MAGICAL_DAMAGE:
                            MagicalAmp -= e.Modifier;
                            break;
                        case EffectType.PHYSICAL_DAMAGE:
                            PhysicalAmp -= e.Modifier;
                            break;
                    }
                }
            }
        }

        public void HaveItemsExpired(double delta)
        {
            foreach(Item i in items)
            {
                foreach(Effect e in i.GrantedEffects)
                {
                    if(e.HasExpired(delta))
                    {
                        switch (e.Affects)
                        {
                            case EffectType.HP:
                                MaxHealth -= e.Modifier;
                                break;
                            case EffectType.BLOCK:
                                throw new NotImplementedException("Block is not implemented");
                                break;
                            case EffectType.MAGICAL_DAMAGE:
                                MagicalAmp -= e.Modifier;
                                break;
                            case EffectType.PHYSICAL_DAMAGE:
                                PhysicalAmp -= e.Modifier;
                                break;
                        }
                    }
                }
            }
        }
    }
}
