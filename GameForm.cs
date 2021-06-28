using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Ex05_TicTacToe_Logic;

namespace Ex05_TicTacToe_UI
{
    public class GameForm : Form
    {
        List<SmartButton> m_ListButtons = new List<SmartButton>();
        Label m_LabelPlayer1;
        Label m_LabelPlayer2;
        Game  m_Game;

        public GameForm(int i_AmountOfPlayers, int i_Rows, int i_Cols, string i_Player1, string i_Player2)
        {
            m_Game = new Game(i_AmountOfPlayers, i_Rows, i_Player1, i_Player2);
            m_Game.BoardChanged += M_Game_BoardChanged;
            initializeComponents(i_Rows, i_Cols, i_Player1, i_Player2);
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
                    button.BackColor = Color.LightBlue;
                    button.Padding = new Padding(6);
                    button.Font = new Font("French Script MT", 18);
                    button.Click += new EventHandler(m_ButtonSmart_Click);
                    this.Controls.Add(button);
                    m_ListButtons.Add(button);
                }
                overAllHeigth += 88;
            }

            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = $@"{i_Player1} : {m_Game.Players[0].Wins}";
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer1.Top = overAllHeigth + 8;
            m_LabelPlayer1.Left = ((overAllHeigth + 8) / 2) - (m_LabelPlayer1.Width / 2);

            this.Controls.Add(m_LabelPlayer1);

            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = $@"{i_Player2} : {m_Game.Players[1].Wins}";
            m_LabelPlayer2.AutoSize = true;
            m_LabelPlayer2.Top = overAllHeigth + 8;
            m_LabelPlayer2.Left = m_LabelPlayer1.Right + 16;

            this.Controls.Add(m_LabelPlayer2);

            this.Text = "TicTacToe";
            this.ClientSize = new Size(overAllHeigth + 8, m_LabelPlayer1.Bottom + 16);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        }

        private void m_ButtonSmart_Click(object sender, EventArgs e)
        {
            int row = (sender as SmartButton).Row;
            int col = (sender as SmartButton).Col;

            int[] d = { row, col };
            try
            {
                m_Game.PlayRound(d);
                if(m_Game.Players[1].Name == "[Computer]")
                {
                    int randomValue = m_Game.Board.GetEmptyRandomCell();
                    d[0] = (randomValue / m_Game.Board.Length);
                    d[1] = (randomValue % m_Game.Board.Length);
                    m_Game.PlayRound(d);
                }
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show($"{ex.Message} {Environment.NewLine} Would you like to play another round ?", "Game Message:", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(result == DialogResult.Yes)
                {
                    EndGameSequence();
                }
                else
                {
                    DialogResult res = MessageBox.Show("Thank you for playing :)", "GoodBye Message:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(res == DialogResult.OK)
                    {
                        this.Close();
                    }
                }

            }
        }

        private void EndGameSequence()
        {
            m_LabelPlayer2.Text = $@"{m_Game.Players[1].Name} : {m_Game.Players[1].Wins}";
            m_LabelPlayer1.Text = $@"{m_Game.Players[0].Name} : {m_Game.Players[0].Wins}";
            m_Game.Board.InitBoard();
            m_Game.Board.InitEmptyCells();
            foreach(SmartButton button in m_ListButtons)
            {
                button.Text = "";
                button.Enabled = true;
            }
        }

        private void M_Game_BoardChanged(char i_Char, int i_Row, int i_Col)
        {
            int index = ((i_Row) * m_Game.Board.Length) + (i_Col);

            m_ListButtons[index].Text = "" + i_Char;
            m_ListButtons[index].Enabled = false;
        }

    }
}
