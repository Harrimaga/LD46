using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace LD46
{
    /*struct Sprite {
	layout(rgba8, bindless_image) image2D img;
	int w;
	int h;
	float x;
	float y;
	float scalex;
	float scaley;
	int startx;
	int starty;
	float r, g, b, a, rot;
};*/
    struct SData
    {
        long img;
        int w, h;
        float x, y, scalex, scaley;
        int startx, starty;
        float r, g, b, a, rot;

        public SData(long img, int w, int h, float x, float y, float scalex, float scaley, int startx, int starty, float r, float g, float b, float a, float rot)
        {
            this.img = img;
            this.w = w;
            this.h = h;
            this.x = x;
            this.y = y;
            this.scalex = scalex;
            this.scaley = scaley;
            this.startx = startx;
            this.starty = starty;
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
            this.rot = rot;
        }
    }

    class Sprite
    {

        public int w, h, num;
        public Texture texture;

        public Sprite(int w, int h, int num, Texture texture)
        {
            this.w = w;
            this.h = h;
            this.num = num;
            this.texture = texture;
        }

        public void Draw(float x, float y, float rot = 0, float r = 1, float g = 1, float b = 1, float a = 1)
        {
            texture.AddToList(x, y, r, g, b, a, rot, num, w, h);
        }

    }
}
