using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Particle
    {

        public double maxTime, timer = 0;
        public float x, y, xs, ys, r, g, b;
        public int w, h;
        public Sprite s;
        public Animation ani;
        public bool fade;

        public Particle(float x, float y, float xs, float ys, int w, int h, int tNum, int sNum, double duration, float r = 1, float g = 1, float b = 1, bool fade = true, Animation ani = null)
        {
            this.x = x;
            this.y = y;
            this.xs = xs;
            this.ys = ys;
            this.w = w;
            this.h = h;
            this.r = r;
            this.g = g;
            this.b = b;
            this.ani = ani;
            this.maxTime = duration;
            this.fade = fade;
            s = new Sprite(w, h, sNum, Window.texs[tNum]);
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <returns>True if dead</returns>
        public bool Update(double delta)
        {
            if(ani != null)
            {
                ani.Update(s, delta);
            }
            timer += delta;
            x += xs * (float)delta;
            y += ys * (float)delta;
            return timer >= maxTime;
        }

        public void Draw()
        {
            float rotation = 0;
            if (xs != 0 || ys != 0)
            {
                rotation = (float)Math.Atan2(xs, -ys);
            }
            if(fade)
            {
                s.Draw(x, y, true, rotation, r, g, b, (float)((maxTime*1.25 - timer)/maxTime));
            }
            else
            {
                s.Draw(x, y, true, rotation, r, g, b);
            }
            
        }

    }
}
