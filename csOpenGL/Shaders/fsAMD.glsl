#version 430
#extension GL_ARB_bindless_texture : require

//out vec4 outputColor;
uniform ivec2 screenSize;
in vec3 pos;
flat in int id;

struct Sprite {
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
};

layout(std430, binding=0 ) readonly buffer sprites{
    Sprite Sprites[];
};

void main() 
{
    Sprite s = Sprites[id];
    vec2 poss = vec2((pos.x+1)*screenSize.x/2, (1+pos.y)*screenSize.y/2) + vec2(0.49, 0.49);
    ivec2 posi = ivec2(int((poss.x-s.x)/s.scalex+s.startx), int((s.h-(poss.y-s.y))/s.scaley+s.starty));
    vec4 sd = imageLoad(s.img, posi) * vec4(s.r, s.g, s.b, s.a);
    gl_FragColor = sd;
	return;
}