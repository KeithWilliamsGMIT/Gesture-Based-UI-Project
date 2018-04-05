using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts
{
    class PlayerEntity
    {
        private string playerName;
        private int playerScore;

        public void setPlayerName(string pName)
        {
            playerName = pName;
        }

        public string getPlayerName()
        {
            return playerName;
        }

        public void setPlayerScore(int s)
        {
            playerScore = s;
        }

        public int getPlayerScore()
        {
            return playerScore;
        }
    }

}
