﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Hirrathak : Enemy
    {
        private double CastingSpeed { get; set; }
        private bool Casting { get; set; }
        private List<Spell> Spells { get; set; }
        private Spell CurrentSpell { get; set; }
        private float TargetX { get; set; }
        private float TargetY { get; set; }
        private double SwingBack { get; set; }

        public Hirrathak() : base(Enemies.HIRRATHAK_HEALTH, Enemies.HIRRATHAK_MANA, 12 * Globals.TileSize, 12 * Globals.TileSize, 0, 3, 3, Globals.TileSize, Globals.TileSize, Enemies.HIRRATHAK_SPEED, Enemies.HIRRATHAK_ATTACKPOINT, Enemies.HIRRATHAK_ATTACKSPEED, Enemies.HIRRATHAK_DAMAGE, "Hirrathak, the Purple", Enemies.HIRRATHAK_BLOCK, Enemies.HIRRATHAK_PHYSICAL_AMP, Enemies.HIRRATHAK_MAGICAL_AMP)
        {
            Spells = new List<Spell>
            {
                new PillarOfLight(),
                new Fireball(),
            };
            CastingSpeed = 60;
            SwingBack = 500;
        }

        public override void Update(double delta)
        {
            foreach (Spell s in Spells)
            {
                s.Update(delta);
            }
            base.Update(delta);
        }

        public override void AIMove(double delta)
        {
            if (Math.Abs(x - Globals.l.p.x) < 175 && Math.Abs(y - Globals.l.p.y) < 175 && !Casting)
            {
                StupidMovement(delta);
                BasicMeleeAttack(delta);
            }
            else
            {
                CastSpell(delta);
            }
        }

        private void CastSpell(double delta)
        {

            if (Casting)
            {
                attackTimer += delta;
                if (!attacked && attackTimer > CastingSpeed)
                {
                    CurrentSpell.Cast(TargetX, TargetY, new List<Player>() { Globals.l.p }, this);
                    attacked = true;
                    s = baseAnimation;
                    ani = new Animation(0, 3, 10);
                }
                else if (attackTimer > CastingSpeed + SwingBack)
                {
                    Casting = false;
                    s = baseAnimation;
                    ani = new Animation(0, 3, 10);
                }
            }
            else
            {
                List<Spell> possibleSpells = Spells.FindAll((spell) => { return spell.CurrentCooldown < CastingSpeed; });
                CurrentSpell = possibleSpells[Globals.Rng.Next(possibleSpells.Count)];
                s = attack;
                ani = new Animation(0, 9, CastingSpeed / 10);
                TargetX = Globals.l.p.x;
                TargetY = Globals.l.p.y;
                attackTimer = 0;
                attacking = false;
                Casting = true;
                attacked = false;
            }
        }
    }
}