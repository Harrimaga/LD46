using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class Effect
    {
        public EffectType Affects { get; set; }
        public double Modifier { get; set; } //You can cast this to int if you need to
        public double TimeLeft { get; set; }

        public Effect(EffectType affects, double modifier, double timeLeft)
        {
            Affects = affects;
            Modifier = modifier;
            TimeLeft = timeLeft;
        }

        public bool HasExpired (double deltaTime)
        {
            if(TimeLeft == -999) //-999 means doesn't expire
            {
                return false; // Therefore it never expires
            } else if(TimeLeft < 0) // Had already expired before
            {
                return true; //So we don't want to decrease the timeleft even further
            }
            TimeLeft -= deltaTime;
            return TimeLeft < 0;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
