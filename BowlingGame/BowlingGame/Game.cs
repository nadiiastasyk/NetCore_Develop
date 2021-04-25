using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    public class Game
    {
        private int[] _rolls = new int[22];
        private int currentRoll = 0;

        public void Roll(int pins)
        {
            _rolls[currentRoll++] = pins;
        }

        public int Score()
        {
            int score = 0;
            int roll = 0;
            for (int frame = 0; frame < 10; frame++) 
            {
                if (IsSpare(roll))
                {
                    score += _rolls[roll] + _rolls[roll + 1] + _rolls[roll + 2];
                    roll += 2;
                }
                else if (_rolls[roll] == 10)
                {
                    score += _rolls[roll] + _rolls[roll + 1] + _rolls[roll + 2];
                    roll += 1;
                }
                else
                {
                    score += _rolls[roll] + _rolls[roll + 1];
                    roll += 2;
                }
            }

            return score;
        }

        private bool IsSpare(int roll)
        {
            return _rolls[roll] + _rolls[roll + 1] == 10;
        }
    }
}