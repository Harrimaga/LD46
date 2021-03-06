﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class PrayerCircle : Structure
    {
        public bool Prayed { get; set; }
        public double TimePrayed { get; set; }
        public double TimeToPray { get; set; }
        public Sprite sprite { get; set; }

        public PrayerCircle(int x, int y, Tile[,] tileGrid, Theme theme):base(1,1,x,y,tileGrid,theme)
        {
            sprite = new Sprite(Globals.TileSize, Globals.TileSize, 0, Window.texs[37]);
            Prayed = false;
            TimePrayed = 0;
            TimeToPray = 240;
        }
        public override void Place(Tile[,] tileGrid)
        {
        }

        public override void Draw()
        {
            sprite.Draw(X * Globals.TileSize, Y * Globals.TileSize);   
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            if (Globals.checkCol(X* Globals.TileSize, Y*Globals.TileSize, Globals.TileSize, Globals.TileSize, (int)Globals.l.p.x, (int)Globals.l.p.y, Globals.l.p.w, Globals.l.p.h) && !Prayed)
            {
                TimePrayed += deltaTime;
                if(TimePrayed>TimeToPray)
                {
                    Prayed = true;
                    PrayerCircleBossRoom room = (PrayerCircleBossRoom)Globals.l.Current;
                    room.HasPrayed();
                    sprite.texture = Window.texs[36];
                }
            }
        }
    }
}
