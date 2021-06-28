using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05
{
    public delegate void Action<T1, T2, T3>(T1 t1, T2 t2, T3 t3);
    class Game
    {
        public event Action<char, int, int> BoardChanged;
        public event Action<string> d;

        private LogicBoard m_Board;
        private Player[] m_ArrayOfPlayers = new Player[2];
        private int m_PlayerIndex = 0; //Starting with player 1.

        /// <summary>
        /// Parmert CTOR
        /// </summary>
        /// <param name="i_BoardSize">Board's Size</param>
        /// <param name="i_FirstPlayerName">First Player name</param>
        /// <param name="i_SecondPlayerName"></param>
        public Game(int i_AmountOfPlayers, int i_BoardSize,
                  string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_Board = new LogicBoard(i_BoardSize);
            m_ArrayOfPlayers[0] = new Player(0, i_FirstPlayerName);
            m_ArrayOfPlayers[1] = new Player(1, i_SecondPlayerName);
        }

        public int PlayerIndex
        {
            get { return m_PlayerIndex; }
        }

        public int Rows
        {
            get { return m_Board.Length; }
        }

        public LogicBoard Board
        {
            get
            {
                return m_Board;
            }
        }

        public Player[] Players
        {
            get
            {
                return m_ArrayOfPlayers;
            }
        }
        public Player this[int Index]
        {
            get
            {
                return m_ArrayOfPlayers[Index];
            }
        }

        public bool PlayRound(int[] i_MatrixIndex)
        {
            char currSign = this[PlayerIndex].Sign;
            bool returnBool;

            int valueToDelete = (i_MatrixIndex[0]) * Board.Length + (i_MatrixIndex[1]);
            Board.RemoveEmptyCell(valueToDelete);

            Board[i_MatrixIndex[0], i_MatrixIndex[1]] = currSign; // enter here the removeal from emptycells
            BoardChanged.Invoke(currSign, i_MatrixIndex[0], i_MatrixIndex[1]);

            if(Board.FullBoard())
            {
                throw new Exception("This is a Tie!");
            }

            if (LoseMove(PlayerIndex, i_MatrixIndex[0], i_MatrixIndex[1]))
            {
                this[(PlayerIndex + 1) % 2].Wins++;
                throw new Exception(this[(PlayerIndex + 1) % 2].Name + " Wins!");
            }
            else
            {
                returnBool = false;
            }

            if(m_PlayerIndex == 0)
            {
                m_PlayerIndex = 1;
            }
            else
            {
                m_PlayerIndex = 0;
            }
            return returnBool;
        }

        public bool LoseMove(int i_PlayerIndex, int i_Row, int i_Col)
        {
            bool returner;
            int sequence = Board.Length;
            char sign = this[i_PlayerIndex].Sign;

            returner = (CheckDiagBySequence(sign, sequence, i_Row, i_Col)
                        || CheckRowBySequence(sign, sequence, i_Row, i_Col));

            return returner;
        }

        public bool CheckDiagBySequence(char i_Sign, int i_Sequence, int i_Row, int i_Col)
        {
            int sequenceCounterStraight = 0;
            int sequenceCounterBack = 0;

            for (int i = 0; i < i_Sequence; i++)
            {
                if (Board[i, i] == i_Sign)
                {
                    sequenceCounterStraight++;
                }
                else
                {
                    sequenceCounterStraight = 0;
                }

                if (Board[i_Sequence - i - 1, i] == i_Sign)
                {
                    sequenceCounterBack++;
                }
                else
                {
                    sequenceCounterBack = 0;
                }
            }

            return (sequenceCounterStraight == i_Sequence) || (sequenceCounterBack == i_Sequence);
        }

        public bool CheckRowBySequence(char i_Sign, int i_Sequence, int i_Row, int i_Col)
        {
            int sequenceCounterRows = 0;
            int sequenceCounterCols = 0;

            for (int i = 0; i < i_Sequence; i++)
            {
                if (Board[i_Row, i] == i_Sign)
                {
                    sequenceCounterRows++;
                }
                else
                {
                    sequenceCounterRows = 0;
                }

                if (Board[i, i_Col] == i_Sign)
                {
                    sequenceCounterCols++;
                }
                else
                {
                    sequenceCounterCols = 0;
                }
            }

            return (sequenceCounterRows == i_Sequence || sequenceCounterCols == i_Sequence);
        }
    
    }
}
