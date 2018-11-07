using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    class Player
    {
        private string name;
        private int playerNum;
        private bool AI;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int PlayerNum
        {
            get { return playerNum; }
            set { playerNum = value; }
        }

        public bool Ai
        {
            get { return AI; }
            set { AI = value; }
        }
    }
}
