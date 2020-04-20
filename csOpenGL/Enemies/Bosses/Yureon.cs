using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Yureon : Enemy
    {
        public List<Spell> Spells { get; set; }
        public double CastingSpeed { get; set; }

        public Yureon(): base(Enemies.YUREON_HEALTH, Enemies.YUREON_MANA, 12 * Globals.TileSize, 12 * Globals.TileSize, 6, 7, 0, Globals.TileSize * 3, Globals.TileSize * 3, Enemies.YUREON_SPEED, Enemies.YUREON_ATTACKPOINT, Enemies.YUREON_ATTACKSPEED, Enemies.YUREON_DAMAGE, "Yureon, Cannon of glass", Enemies.YUREON_BLOCK, Enemies.YUREON_PHYSICAL_AMP, Enemies.YUREON_MAGICAL_AMP)
        {
            Spells = new List<Spell>
            {
                new Fireball(),
                new Fireball(),
                new Fireball(),
                new Fireball(),
                new PillarOfLight(),
                new PillarOfLight(),
                new PillarOfLight(),
                new PillarOfLight(),
                new Slowness(),
                new Slowness(),
                new Slowness(),
                new Slowness()
            };
            CastingSpeed = 90;
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
            RainHellAndFire(delta);
        }

        private void RainHellAndFire(double delta)
        {
            attackTimer += delta;
            if(attackTimer > CastingSpeed)
            {
                attackTimer = 0;
                IEnumerable<Spell> SpellsToCast = Spells.FindAll((spell) => { return spell.CurrentCooldown < CastingSpeed; }).Take(4);
                foreach (Spell s in SpellsToCast)
                {
                    s.Cast(Globals.Rng.Next(Globals.l.Current.width * Globals.TileSize), Globals.Rng.Next(Globals.l.Current.height * Globals.TileSize), new List<Entity> { Globals.l.p }, this);
                }
                ani = idleAni;
            }else if(attackTimer > (CastingSpeed/4)*3)
            {
                ani = attackAni;
            }

        }
    }
}
