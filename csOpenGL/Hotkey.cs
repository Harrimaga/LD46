using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    class Hotkey
    {

        private bool repeat;
        private List<Key> keys;

        public Hotkey(bool repeat)
        {
            this.repeat = repeat;
            keys = new List<Key>();
        }

        public Hotkey AddKey(Key k)
        {
            keys.Add(k);
            return this;
        }

        public bool IsDown()
        {
            foreach(Key k in keys)
            {
                if(repeat)
                {
                    if(now.IsKeyDown(k))
                    {
                        return true;
                    }
                }
                else
                {
                    if(now.IsKeyDown(k) && !prev.IsKeyDown(k))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Static update shit
        private static KeyboardState prev, now;

        public static void Update(KeyboardState kstate)
        {
            prev = now;
            now = kstate;
        } 

    }
}
