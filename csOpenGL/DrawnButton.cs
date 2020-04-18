using QuickFont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class DrawnButton
    {
        public delegate void EventAction();

        private Sprite Sprite { get; set; }
        public string Text { get; set; }
        private float X { get; set; }
        private float Y { get; set; }
        private float Width { get; set; }
        private float Height { get; set; }
        private EventAction OnClickAction { get; set; }

        public DrawnButton(string text, float x, float y, float width, float height, EventAction onClickAction)
        {
            if(height<25)
            {
                height = 25;
            }
            Text = text;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            OnClickAction = onClickAction;
            Sprite = new Sprite((int)width, (int)height, 0, Window.texs[1]);
        }

        public bool IsInButton(float x, float y)
        {
            return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        }

        public void OnClick()
        {
            OnClickAction();
        }

        public void Draw()
        {
            Sprite.Draw(X, Y);
            Window.window.DrawTextCentered(Text, (int)(X + (Width / 2)), (int)(Y + (Height / 2) - 12), Globals.buttonFont);
            
        }
    }
}
