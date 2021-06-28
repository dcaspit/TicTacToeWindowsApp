using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05_TicTacToe_Logic
{
    public class Player
    {
        private int m_WinCounter = 0;
        private char m_Sign;
        private string m_Name;

        public Player(int i_PlayerIndex, string i_Name)
        {
            m_Name = i_Name;
            m_Sign = (i_PlayerIndex == 0) ? 'X' : 'O';
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public char Sign
        {
            get
            {
                return m_Sign;
            }
            set
            {
                if (value == 'X' || value == 'O')
                {
                    m_Sign = value;
                }
                else
                {
                    Console.WriteLine("This sign is not OK!");
                }
            }
        }

        public int Wins
        {
            get
            {
                return m_WinCounter;
            }
            set
            {
                if (value > m_WinCounter)
                {
                    m_WinCounter = value;
                }
                else
                {
                    Console.WriteLine("Can't update wins count to less the current one");
                }
            }
        }

    }
}
