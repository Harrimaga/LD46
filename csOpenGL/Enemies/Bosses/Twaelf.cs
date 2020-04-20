using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Twaelf : Enemy
    {

        public int maxTurrets, currentTurrets;
        public double spawnTimer, spawnTimerBase;

        public Twaelf() : base(Enemies.TWAELF_HEALTH, Enemies.TWAELF_MANA, 12 * Globals.TileSize, 12 * Globals.TileSize, 41, 41, 0, Globals.TileSize * 3, Globals.TileSize * 3, Enemies.TWAELF_SPEED, Enemies.TWAELF_ATTACKPOINT, Enemies.TWAELF_ATTACKSPEED, Enemies.TWAELF_DAMAGE, "Twaelf, the Spawner", Enemies.TWAELF_BLOCK, Enemies.TWAELF_PHYSICAL_AMP, Enemies.TWAELF_MAGICAL_AMP)
        {
            maxTurrets = 12;
            currentTurrets = 0;
            spawnTimer = 0;
            spawnTimerBase = 5 * 60;
            minDistance = 200;
            attackAni = new Animation(0, 9, 10);
            idleAni = new Animation(0, 3, 10);
        }

        public override void Update(double delta)
        {
            spawnTimer += delta;

            if (spawnTimer > spawnTimerBase)
            {
                if (currentTurrets < maxTurrets)
                {
                    SpawnTurret();
                }
            }

            base.Update(delta);
        }

        public void SpawnTurret()
        {
            spawnTimer = 0;
            Globals.l.Current.addables.Add(new Turret(Globals.Rng.Next(1, Globals.l.Current.width - 1) * Globals.TileSize, Globals.Rng.Next(1, Globals.l.Current.height - 1) * Globals.TileSize));
        }

        public override void AIMove(double delta)
        {
            float distance = float.MaxValue;
            Enemy closest = null;
            foreach (Enemy enemy in Globals.l.Current.enemies)
            {
                if (enemy != this)
                {
                    float xd = enemy.x + enemy.w / 2 - x - w / 2;
                    float yd = enemy.y + enemy.h / 2 - y - h / 2;
                    float dis = (float)Math.Sqrt(xd * xd + yd * yd);

                    if (dis < distance)
                    {
                        closest = enemy;
                        distance = dis;
                    }
                }
            }

            if (closest != null)
            {
                float xd = closest.x + closest.w / 2 - x - w / 2;
                float yd = closest.y + closest.h / 2 - y - h / 2;
                xd /= distance;
                yd /= distance;
                xDir = xd;
                yDir = yd;
                if (distance > minDistance)
                {
                    if (!stunned) Move((float)(xd * delta * speed), (float)(yd * delta * speed));
                }
            }

            base.AIMove(delta);
        }
    }
}
