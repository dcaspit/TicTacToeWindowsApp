using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05
{
    class LogicBoard
    {
        private int? m_LengthOfBoard;
        private char[,] m_GameBoard;
        private List<int> m_EmptyCells;

        public LogicBoard(int i_Length)
        {
            m_LengthOfBoard = i_Length;
            m_GameBoard = new char[i_Length, i_Length];
            m_EmptyCells = new List<int>();
            InitEmptyCells();
            InitBoard();
        }

        public int Length
        {
            get
            {
                if (m_LengthOfBoard != null)
                {
                    return m_LengthOfBoard.Value;
                }
                else
                {
                    Console.WriteLine("Length Parmeter isn't initialized yet!");
                    return 0;
                }
            }
            set
            {
                m_LengthOfBoard = value;
            }
        }

        public char this[int row, int col]
        {
            get
            {
                return m_GameBoard[row, col];
            }
            set
            {
                m_GameBoard[row, col] = value;
            }
        }

        public void InitEmptyCells()
        {
            if (m_LengthOfBoard != null)
            {
                for (int i = 0; i < (m_LengthOfBoard * m_LengthOfBoard); i++)
                {
                    m_EmptyCells.Add(i);
                }
            }
        }

        public void RemoveEmptyCell(int i_Value)
        {
            m_EmptyCells.Remove(i_Value);
        }

        public int GetEmptyRandomCell()
        {
            Random random = new Random();
            int index = random.Next(m_EmptyCells.Count());
            return m_EmptyCells[index];
        }

        public void InitBoard()
        {
            for (int i = 0; i < m_LengthOfBoard; i++)
            {
                for (int j = 0; j < m_LengthOfBoard; j++)
                {
                    this[i, j] = ' ';
                }
            }
        }

        public bool FullBoard()
        {
            return m_EmptyCells.Count == 0;
        }

    }
}
