using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class Player
    {
        private string name;
        private bool? isAI;
        private int[] bestMoveArray;

        public Player(string name, bool? isAi)
        {
            this.isAI = isAi;
            if (isAi == true)
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

            if (playerOneTurn == true)
            {
                for (int i = 0; i < 6; i++)
                {
                    int count = int.Parse(currentBoard[i]);
                    int endPos = i + count;
                    if ((i + int.Parse(currentBoard[i])) == 6)
                    {
                        bestMoveArray[i] = 2;
                    }

                    else if (endPos == 0 && int.Parse(currentBoard[12 - endPos]) != 0)
                    {
                        bestMoveArray[i] = 1;
                    }
                    else if (count > 2 && endPos >= 0)
                    {
                        bestMoveArray[i] = 3;
                    }
                    else
                    {
                        int random;
                        Random rand = new Random();
                        random = rand.Next(6) + 0;
                        bestMoveArray[random] = 4;
                    }
                }

            }
            else
            {
                for (int i = 7; i < 13; i++)
                {
                    int count = int.Parse(currentBoard[i]);
                    int endPos = i + count;
                    if ((i + int.Parse(currentBoard[i])) == 13)
                    {
                        bestMoveArray[i - 7] = 2;
                    }

                    else if (endPos == 7 && int.Parse(currentBoard[12 - endPos]) != 0)
                    {
                        bestMoveArray[i - 7] = 1;
                    }
                    else if (count > 2 && endPos >= 7)
                    {
                        bestMoveArray[i - 7] = 3;
                    }
                    else
                    {
                        int random;
                        Random rand = new Random();
                        random = rand.Next(6) + 0;
                        bestMoveArray[random + 7] = 4;
                    }

                }

            }
            return SelectMove(playerOneTurn);
        }

        public int SelectMove(bool playerOneTurn)
        {

            //check the for the best move 
            int value = 0;
            if (bestMoveArray.Contains(1))
            {
                for (int x = 6; x > 0; x--)
                {
                    if (bestMoveArray[x] == 1)
                    {
                        value = x;
                        break;
                    }
                }

            }
            else if (bestMoveArray.Contains(2))
            {
                for (int x = 6; x > 0; x--)
                {
                    if (bestMoveArray[x] == 2)
                    {
                        value = x;
                        break;
                    }
                }
            }
            else if (bestMoveArray.Contains(3))
            {
                for (int x = 6; x < 0; x--)
                {
                    if (bestMoveArray[x] == 3)
                    {
                        value = x;
                        break;
                    }
                }
            }
            else
            {
                for (int x = 6; x < 0; x--)
                {
                    if (bestMoveArray[x] == 4)
                    {
                        value = x;
                        break;
                    }
                }
            }


            if (playerOneTurn)
            {
                return value;
            }
            else
            {
                return (value + 7);
            }
        }
    }
}
