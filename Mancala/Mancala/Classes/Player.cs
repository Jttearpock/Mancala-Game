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
        private int[] bestMoveArray;

        public Player(string name, bool? isAi)
        {
            this.isAI = isAi;
            if (isAi ==  true)
            {
                string aiName = "[AI]";
                this.name = aiName += name;
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
            
            if (playerOneTurn)
            {
                for (int i = 0; i < 6; i++)
                {
                    int count = int.Parse(currentBoard[i]);
                    int endPos = i + count;
                    if ((i + int.Parse(currentBoard[i])) == 6)
                    {
                        bestMoveArray[i] = 4;
                    }

                    else if (endPos == 0 && int.Parse(currentBoard[12-endPos]) != 0)
                    {
                        bestMoveArray[i] = 5;
                    }
                }

            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    int count = int.Parse(currentBoard[i]);
                    int endPos = i + count;
                    if ((i + int.Parse(currentBoard[i])) == 6)
                    {
                        bestMoveArray[i] = 4;
                    }

                    else if (endPos == 0 && int.Parse(currentBoard[12 - endPos]) != 0)
                    {
                        bestMoveArray[i] = 5;
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
