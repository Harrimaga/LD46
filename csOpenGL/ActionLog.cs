﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD46
{
    public class ActionLog
    {
        private int limit;
        public Queue<string> ActionList { get; set; }
        public Sprite Sprite { get; set; }
        public int Limit { get { return limit; } set { limit = value; CheckLimit(); } }

        public ActionLog(int limit)
        {
            ActionList = new Queue<string>();
            Limit = limit;
            Sprite = new Sprite(350, 255, 0, Window.texs[2]);
        }

        public void Add(string item)
        {
            ActionList.Enqueue(item);
            CheckLimit();
        }

        public void DealDamage(string enemyName, double damage, string source)
        {
            Add("You deal " + damage + " damage to " + enemyName + " using " + source);
        }

        public void TakeDamage(string enemyName, double damage, string source)
        {
            Add(enemyName + " deals " + damage + " damage to you using " + source);
        }

        public void EnemyTakeDamageFromEnemy(string targetName, string sourceName, double damage)
        {
            Add(sourceName + " deals " + damage + " damage to " + targetName);
        }

        public void CastSpell(string spellName, string caster, int targets)
        {
            Add(caster + " casts " + spellName + ", affecting " + targets + " targets");
        }

        public void Death(string entityName)
        {
            Add(entityName + " has died.");
        }

        public void BossDeath()
        {
            Add("Game lost because the boss died.");
        }

        private void CheckLimit()
        {
            while (ActionList.Count > Limit)
            {
                ActionList.Dequeue();
            }
        }

        public void Draw()
        {
            
            Sprite.Draw(0, 1080 - 255, false, 0, 0, 0, 0, 0.85f);
            int y = 1080 - 255;
            foreach (string logItem in ActionList)
            {
                Window.window.DrawText(logItem, 0, y, Globals.logFont);
                y += 17;
            }
            
        }
    }
}
