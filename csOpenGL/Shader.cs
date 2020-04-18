using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace LD46
{
    public class Shader
    {

        public int Handle;

        public Shader(string ver, string frag)
        {
            string vss;
            string fss;
            using (StreamReader reader = new StreamReader(ver, Encoding.UTF8))
            {
                vss = reader.ReadToEnd();
            }
            using (StreamReader reader = new StreamReader(frag, Encoding.UTF8))
            {
                fss = reader.ReadToEnd();
            }
            int vs = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vs, vss);
            int fs = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fs, fss);

            GL.CompileShader(vs);
            string log = GL.GetShaderInfoLog(vs);
            if (log != System.String.Empty)
            {
                System.Console.WriteLine("You fucked up mate: \n" + log);
            }

            GL.CompileShader(fs);
            log = GL.GetShaderInfoLog(vs);
            if (log != System.String.Empty)
            {
                System.Console.WriteLine("You fucked up mate: \n" + log);
            }
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vs);
            GL.AttachShader(Handle, fs);

            GL.LinkProgram(Handle);

            GL.DetachShader(Handle, vs);
            GL.DetachShader(Handle, fs);
            GL.DeleteShader(fs);
            GL.DeleteShader(vs);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

    }
}
