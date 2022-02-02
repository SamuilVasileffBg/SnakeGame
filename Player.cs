using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SnakeGame
{
    class Player
    {
        public Player(string name, List<int> score)
        {
            Name = name;
            Score = score;
        }

        public string Name { get; set; }
        public List<int> Score { get; set; }

        private int highscore;
        public int Highscore {
            get
            {
                highscore = Score.Max();
                return highscore;
            }
            set
            {
                highscore = Score.Max();
            }

        }

    }
}
