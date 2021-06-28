using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05_TicTacToe_UI
{

    public class Authenticator
    {
        public static bool CheckUserCredentials(int i_Rows, int i_Cols)
        {
            return i_Rows == i_Cols;
        }
    }

}
