using System;
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
        public Sprite UIBack;
        public List<Item> items = new List<Item>();
        public List<DrawnButton> buttons = new List<DrawnButton>();

        public Player(double Health, float x, float y, int texNum, int attackTexNum, int spriteNum, int w, int h, double speed, double attackPoint, double attackSpeed, string name, double damage, double PhysicalAmp, double MagicalAmp)
        {
            Init(Health, x, y, texNum, attackTexNum, spriteNum, w, h, speed);
            ani = new Animation(0, 3, 10);
            this.attackSpeed = attackSpeed;
            this.attackPoint = attackPoint;
            this.damage = damage;
            this.name = name;
            UIBack = new Sprite(200, 880, 0, Window.texs[2]);
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
            items.Add(new Sword());
        }

        public void AddItem(Item it)
        {
            items.Add(it);
            DrawnButton b = new DrawnButton("", 1725, 5 + 55 * items.Count, 190, 45, () => { });
            Window.window.game.buttons.Add(b);
            buttons.Add(b);
        }

        public void RemoveItem(Item it)
        {
            for(int i = 0; i < items.Count; i++)
            {

            }
            items.Remove(it);
        }

        public override void Update(double delta)
        {
            float dis = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if(dis != 0)
            {
                xDir /= dis;
                yDir /= dis;
            }
            Move((float)(delta * xDir * speed), (float)(delta * yDir * speed));

            for (int i = (int)(x / Globals.TileSize); i < (int)(x / Globals.TileSize) + 2 + w / Globals.TileSize && i < Globals.l.Current.width && i > -1; i++)
            {
                for (int j = (int)(y / Globals.TileSize); j < (int)(y / Globals.TileSize) + 2 + h / Globals.TileSize && j < Globals.l.Current.height && j > -1; j++)
                {
                    Tile t = Globals.l.Current.getTile(i, j);
                    if (t.GetTileType() == TileType.DOOR)
                    {
                        Direction d = Direction.SOUTH;
                        if(i == 0)
                        {
                            d = Direction.WEST;
                        } else if(j == 0)
                        {
                            d = Direction.NORTH;
                        } else if(i == Globals.l.Current.width-1)
                        {
                            d = Direction.EAST;
                        }

                        foreach(Connection c in Globals.l.Current.Connections)
                        {
                            if(c.Direction == d)
                            {
                                Globals.l.Current = c.Room;
                                switch(d)
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
                            }
                        }
                    }
                }
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
            if(x != 0)
            {
                xDir = x;
            }
            if(y != 0)
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
                if(!attacked && attackTimer > attackSpeed * attackPoint)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        float xd = enemy.x + enemy.w / 2 - x - w / 2;
                        float yd = enemy.y + enemy.h / 2 - y - h / 2;
                        float dis = (float)Math.Sqrt(xd * xd + yd * yd);

                        if (dis <= (enemy.w / 2 + w / 2) * 1.2f)
                        {
                            enemy.DealPhysicalDamage(damage, name, "their blade");
                        }

                        attacked = true;
                        attacking = false;
                        s = baseAnimation;
                        a = false;
                        ani = new Animation(0, 3, 10);
                    }
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
            UIBack.Draw(1720, 0, 0, 0.5f, 0.5f, 0.5f, 0.85f);
            int y = 5;
            UIBack.w = 190;
            UIBack.h = 45;
            foreach (Item it in items)
            {
                UIBack.Draw(1725, y, 0, 0, 0, 0, 0.5f);
                it.Draw(1727, y + 2);
                y += 55;
            }
        }

    }
}
