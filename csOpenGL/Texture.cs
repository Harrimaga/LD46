﻿using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace LD46
{
    public class Texture
    {

        public long Handle;
        public int totW, totH, sW, sH, wNum, hNum;
        public string file;

        public Texture(string file, int totW, int totH, int sW, int sH)
        {
            this.file = file;
            this.totW = totW;
            this.totH = totH;
            this.sW = sW;
            this.sH = sH;
            wNum = totW / sW;
            hNum = totH / sH;

            Image<Rgba32> image = (Image<Rgba32>)Image.Load(file);
            image.Mutate(x => x.Flip(FlipMode.Vertical));
            Rgba32[] tempPixels = image.GetPixelSpan().ToArray();
            List<byte> pixels = new List<byte>();

            foreach (Rgba32 p in tempPixels)
            {
                pixels.Add(p.R);
                pixels.Add(p.G);
                pixels.Add(p.B);
                pixels.Add(p.A);
            }

            int h = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, h);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 0x2601);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, 0x2601);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
            Handle = GL.Arb.GetImageHandle(h, 0, false, 0, (PixelFormat)0x8058);
        }

        public void AddToList(float x, float y, float r, float g, float b, float a, float rot, int num, int w, int h, bool cam)
        {
            int sX = num*sW % totW;
            int sY = (hNum - 1) - num*sW / totW;
            sY *= sH;
            float scaleX = (float)(w) / sW;
            float scaleY = (float)(h) / sH;

            if(cam)
            {
                Window.sd.Add(new SData(Handle, (int)(w * Window.screenScaleX), (int)(h * Window.screenScaleY), (x - Window.camX) * Window.screenScaleX, (y - Window.camY) * Window.screenScaleY, scaleX * Window.screenScaleX, scaleY * Window.screenScaleY, sX, sY, r, g, b, a, rot));
            }
            else
            {
                Window.sd.Add(new SData(Handle, (int)(w * Window.screenScaleX), (int)(h * Window.screenScaleY), x * Window.screenScaleX, y * Window.screenScaleY, scaleX * Window.screenScaleX, scaleY * Window.screenScaleY, sX, sY, r, g, b, a, rot));
            }
            
        } 

    }
}
