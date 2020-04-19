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
        public Sprite UIBack, HBarUI, HBarBackUI, MBarUI, MBarBackUI;
        public List<Item> items = new List<Item>();
        public List<Spell> Spells { get; set; }
        public List<DrawnButton> itemButtons = new List<DrawnButton>(), spellButtons = new List<DrawnButton>();
        public double HealthRegen { get; set; }
        public double ManaRegen { get; set; }

        public Player(double Health, double Mana, float x, float y, int texNum, int attackTexNum, int spriteNum, int w, int h, double speed, double attackPoint, double attackSpeed, string name, double damage, double PhysicalAmp, double MagicalAmp, double standardBlock, double blockRegen)
        {
            HealthRegen = 0.1;
            ManaRegen = 0.1;
            Init(Health, Mana, x, y, texNum, attackTexNum, spriteNum, w, h, speed, 1, standardBlock, PhysicalAmp, MagicalAmp, blockRegen);
            Spells = new List<Spell>();
            ani = new Animation(0, s.texture.totW / s.texture.sW - 1, 10);
            this.attackSpeed = attackSpeed;
            this.attackPoint = attackPoint;
            this.damage = damage;
            this.name = name;
            UIBack = new Sprite(200, 880, 0, Window.texs[2]);
            HBarUI = new Sprite(w, h / 8, 0, Window.texs[2]);
            HBarBackUI = new Sprite(w, h / 8, 0, Window.texs[2]);
            MBarUI = new Sprite(w, h / 8, 0, Window.texs[2]);
            MBarBackUI = new Sprite(w, h / 8, 0, Window.texs[2]);
            AddSpell(new Fireball());
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

                Regen(delta);
                HaveItemsExpired(delta);
                foreach (Spell s in Spells)
                {
                    s.Update(delta);
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
                    ani = new Animation(0, s.texture.totW / s.texture.sW - 1, 10);
                }
                else if (attackTimer > attackSpeed)
                {
                    attacking = false;
                    s = baseAnimation;
                    a = false;
                    ani = new Animation(0, s.texture.totW / s.texture.sW - 1, 10);
                }
            }
            else
            {
                s = attack;
                ani = new Animation(0, attack.texture.totW / s.texture.sW - 1, attackSpeed / 10);
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
            UIBack.h = 915;
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

            string TextHP = "HP: " + (int)Health + "/" + MaxHealth;
            string TextMP = "MP: " + (int)Mana + "/" + MaxMana;

            Window.window.DrawTextCentered(TextHP, (int)(1720 + (200 / 2)), (int)(750 + (30 / 2) - 12), Globals.buttonFont);
            Window.window.DrawTextCentered(TextMP, (int)(1720 + (200 / 2)), (int)(800 + (30 / 2) - 12), Globals.buttonFont);
            Window.window.DrawTextCentered("Block: " + (int)CurrentBlock, (int)(1720 + (200 / 2)), (int)(880 + (30 / 2) - 12), Globals.buttonFont);

            foreach (Spell sp in Spells)
            {
                UIBack.Draw(1725, y, false, 0, 0, 0, 0, 0.5f);
                sp.Draw(1727, y + 2);
                y += 50;
            }
            y = 355;
            foreach (Item it in items)
            {
                UIBack.Draw(1725, y, false, 0, 0, 0, 0, 0.5f);
                it.Draw(1727, y + 2);
                y += 50;
            }
        }

        public void AddSpell(Spell spell)
        {
            DrawnButton b = new DrawnButton("", 1725, 5 + 50 * Spells.Count, 190, 45, () => { RemoveSpell(spell); });
            b.a = 0;
            Game.game.buttons.Add(b);
            spellButtons.Add(b);
            Spells.Add(spell);
        }

        public void RemoveSpell(Spell spell)
        {
            int i = 0;
            for (i = 0; i < Spells.Count; i++)
            {
                if (Spells[i] == spell)
                {
                    break;
                }
            }
            DrawnButton b = spellButtons[i];
            spellButtons.Remove(b);
            Game.game.buttons.Remove(b);
            Globals.l.Current.DropSpell(spell, x, y);
            for (int j = i; j < spellButtons.Count; j++)
            {
                spellButtons[j].Y -= 50;
            }
            Spells.Remove(spell);
        }

        public void EquipItem(Item item)
        {
            DrawnButton b = new DrawnButton("", 1725, 355 + 50 * items.Count, 190, 45, () => { DequipItem(item); });
            b.a = 0;
            Game.game.buttons.Add(b);
            itemButtons.Add(b);

            items.Add(item);
            foreach (Effect e in item.GrantedEffects)
            {
                if (!e.HasExpired(0))
                {
                    switch (e.Affects)
                    {
                        case EffectType.HP:
                            double HPpercent = Health / MaxHealth;
                            MaxHealth += e.Modifier;
                            Health = MaxHealth * HPpercent;
                            break;
                        case EffectType.BLOCK:
                            StandardBlock += e.Modifier;
                            CurrentBlock += e.Modifier;
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
                if (items[i] == item)
                {
                    break;
                }
            }
            DrawnButton b = itemButtons[i];
            itemButtons.Remove(b);
            Game.game.buttons.Remove(b);
            Globals.l.Current.DropItem(item, x, y);
            for(int j = i; j < itemButtons.Count; j++)
            {
                itemButtons[j].Y -= 50;
            }
            items.Remove(item);
            foreach (Effect e in item.GrantedEffects)
            {
                if (!e.HasExpired(0))
                {
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
            foreach (Item i in items)
            {
                foreach (Effect e in i.GrantedEffects)
                {
                    if (e.HasExpired(delta))
                    {
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

        public void Regen(double deltaTime)
        {
            TimePassed += deltaTime;
            if (TimePassed > RegenTick)
            {
                TimePassed -= RegenTick;
                Health = Health + HealthRegen > MaxHealth ? MaxHealth : Health + HealthRegen;
                Mana = Mana + ManaRegen > MaxMana ? MaxMana : Mana + ManaRegen;
            }
        }
    }
}
