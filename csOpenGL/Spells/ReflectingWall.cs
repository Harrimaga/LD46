﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class ReflectingWall: Spell
    {
        Tile TileCopy { get; set; }
        Tile Tile { get; set; }
        double TimeLeft { get; set; }
        bool SetTileBack { get; set; }

        public ReflectingWall(): base(
            Spells.CREATE_WALL_MANA,
            Spells.CREATE_WALL_DAMAGE,
            Spells.CREATE_WALL_COOLDOWN,
            Spells.CREATE_WALL_NAME,
            Spells.CREATE_WALL_DESCRIPTION,
            new List<Effect> { },
            Spells.CREATE_WALL_AOE,
            new Sprite(40, 40, 0, Window.texs[29]),
            new Animation(0, 0, 30),
            SpellType.SINGLE_TARGET,
                20,
                0)
        {
            SetTileBack = true;
            TimeLeft = 0;
        }

        public override void Cast(float x, float y, IEnumerable<Entity> possibleTargets, Entity caster)
        {
            if (CurrentCooldown > 0 || !caster.LoseMana(Mana))
            {
                return;
            }
            CurrentCooldown = Cooldown;
            Tile = Globals.l.Current.getTile((int)(x / Globals.TileSize), (int)(y / Globals.TileSize));
            if(Tile.GetTileType()==TileType.TILE)
            {
                TileCopy = (Tile)Tile.Clone();
                Tile = new Tile(new Sprite(Globals.TileSize, Globals.TileSize, 0, Globals.l.Current.Theme.GetTextureByType(TileType.WALL)), Walkable.SOLID, TileType.WALL, 0);
                TimeLeft = 240;
                SetTileBack = false;
            }
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            TimeLeft -= deltaTime;
            if(TimeLeft<0 && !SetTileBack)
            {
                Tile = TileCopy;
                SetTileBack = true;
            }
        }
    }
}
