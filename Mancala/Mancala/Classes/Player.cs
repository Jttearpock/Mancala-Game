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
        private bool? isAI;
        private string[] gameBoard;
        private int[] bestMoveArray;

        public Player(string name, bool? isAi)
        {
            this.isAI = isAi;
            if (isAi ==  true)
            {
                string aiName = "[AI]";
                this.name = aiName += name;
                gameBoard = new string[14];
                bestMoveArray = new int[6];
            }
            else
            {
                this.name = name;
            }
        }

        public string Name
        {
            get { return name; }
        }

        public bool? IsAi
        {
            get { return isAI; }
        }

        public int FindBestMove(string[] currentBoard, bool playerOneTurn)
        {
            gameBoard = currentBoard;
            if (playerOneTurn)
            {
                for (int i = 0; i < 6; i++)
                {
                    if ((i + int.Parse(gameBoard[i])) == 6)
                    {
                        bestMoveArray[i] = 3;
                    }
                }

            }
            return 0;
        }

        public int SelectMove(bool playerOneTurn)
        {
            return 0;
        }
    }
}
