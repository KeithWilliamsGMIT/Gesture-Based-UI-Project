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
            this.playerName = pName;
        }

        public string getPlayerName()
        {
            return this.playerName;
        }
    }

}
