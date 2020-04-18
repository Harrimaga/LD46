using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public abstract class Item
    {
        public string Name { get; set; }
        public Rarity Rarity { get; set; }
        public string Description { get; set; }
        public Sprite Sprite { get; set; }
        public Effect[] GrantedEffects { get; set; }

        protected Item(string name, Rarity rarity, string description, Sprite sprite, Effect[] grantedEffects)
        {
            Name = name;
            Rarity = rarity;
            Description = description;
            Sprite = sprite;
            GrantedEffects = grantedEffects;
        }

        public virtual void UseItem()
        {

        }
    }
}
