using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05
{
    public class GameForm : Form
    {
        List<SmartButton> m_ListButtons = new List<SmartButton>();
        Label m_LabelPlayer1;
        Label m_LabelPlayer2;
        Game  m_Game;


        public GameForm(int i_AmountOfPlayers, int i_Rows, int i_Cols, string i_Player1, string i_Player2)
        {
            initializeComponents(i_Rows, i_Cols, i_Player1, i_Player2);
            m_Game = new Game(i_AmountOfPlayers, i_Rows, i_Player1, i_Player2);
            m_Game.BoardChanged += M_Game_BoardChanged;
        }

        private void M_Game_BoardChanged(char t, int row, int col)
        {
            foreach(SmartButton button in m_ListButtons)
            {
               if(button.Row == row && button.Col == col)
                {
                    button.Text = "" + t;
                    //button.Enabled = false;
                }
            }
        }

        private void initializeComponents(int i_Rows, int i_Cols, string i_Player1, string i_Player2)
        {
            int overAllHeigth = 16;
            for (int i = 0; i < i_Rows; i++)
            {
                for (int j = 0; j < i_Cols; j++)
                {
                    SmartButton button = new SmartButton(i, j);
                    button.Width = 80;
                    button.Height = 80;
                    if (i == 0)
                    {
                        button.Top = 16;
                    }
                    else
                    {
                        button.Top = 16 + (i * 88);
                    }
                    if (j == 0)
                    {
                        button.Left = 16;
                    }
                    else
                    {
                        button.Left = 16 + (j * 88);
                    }
                    button.Click += new EventHandler(m_ButtonSmart_Click);
                    this.Controls.Add(button);
                    m_ListButtons.Add(button);
                }
                overAllHeigth += 88;
            }



            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = i_Player1 + ":";
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer1.BackColor = Color.Yellow;
            m_LabelPlayer1.Top = overAllHeigth + 8;
            m_LabelPlayer1.Left = ((overAllHeigth + 8) / 2) - (m_LabelPlayer1.Width / 2);

            this.Controls.Add(m_LabelPlayer1);

            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = i_Player2 + ":";
            m_LabelPlayer2.AutoSize = true;
            m_LabelPlayer2.BackColor = Color.Yellow;
            m_LabelPlayer2.Top = overAllHeigth + 8;
            m_LabelPlayer2.Left = m_LabelPlayer1.Right + 16;

            this.Controls.Add(m_LabelPlayer2);

            this.Text = "TicTacToeMisere";
            this.ClientSize = new Size(overAllHeigth + 8, m_LabelPlayer1.Bottom + 16);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        }

        private void m_ButtonSmart_Click(object sender, EventArgs e)
        {
            int row = (sender as SmartButton).Row;
            int col = (sender as SmartButton).Col;
            //(sender as SmartButton).Text = "(" + row + " , " + col + ")";
            //MessageBox.Show("Row: " + row + " Col: " + col, "Button's Credentials:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int[] d = { row, col };
            try
            {
                m_Game.PlayRound(d);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Game Message:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
