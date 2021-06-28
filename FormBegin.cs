using System;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05
{
    public class FormBegin : Form
    {
        private const int k_Min = 3;
        private const int k_Max = 9;

        Label m_LabelPlayers;
        Label m_LabelPlayer1;
        Label m_LabelPlayer2;
        Label m_LabelBoardSize;
        Label m_LabelRows;
        Label m_LabelCols;

        TextBox m_TextBoxPlayer1;
        TextBox m_TextBoxPlayer2;

        Button m_ButtonStart;

        CheckBox m_CheckBoxPlayer2;

        NumericUpDown m_NumericUpDownRows;
        NumericUpDown m_NumericUpDownCols;


        public FormBegin()
        {
            initializeComponents();
        }

        private void initializeComponents()
        {
            m_LabelPlayers = new Label();
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Top = 16;
            m_LabelPlayers.Left = 16;
            m_LabelPlayers.AutoSize = true;
            this.Controls.Add(m_LabelPlayers);

            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = "Player 1:";
            m_LabelPlayer1.Top = m_LabelPlayers.Bottom + 8;
            m_LabelPlayer1.Left = m_LabelPlayers.Left + 8;
            m_LabelPlayer1.AutoSize = true;
            this.Controls.Add(m_LabelPlayer1);

            m_TextBoxPlayer1 = new TextBox();
            m_TextBoxPlayer1.Left = m_LabelPlayer1.Right + 36;
            m_TextBoxPlayer1.Top = m_LabelPlayer1.Top + m_LabelPlayer1.Height / 2 - m_TextBoxPlayer1.Height / 2;
            this.Controls.Add(m_TextBoxPlayer1);

            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = "Player 2:";
            m_LabelPlayer2.Top = m_LabelPlayer1.Bottom + 16;
            m_LabelPlayer2.Left = m_LabelPlayer1.Left + 16;
            m_LabelPlayer2.AutoSize = true;
            this.Controls.Add(m_LabelPlayer2);

            m_TextBoxPlayer2 = new TextBox();
            m_TextBoxPlayer2.Left = m_TextBoxPlayer1.Left;
            m_TextBoxPlayer2.Top = m_LabelPlayer2.Top + m_LabelPlayer2.Height / 2 - m_TextBoxPlayer2.Height / 2;
            m_TextBoxPlayer2.Text = "[Computer]";
            m_TextBoxPlayer2.Enabled = false;
            this.Controls.Add(m_TextBoxPlayer2);

            m_CheckBoxPlayer2 = new CheckBox();
            m_CheckBoxPlayer2.Checked = false;
            m_CheckBoxPlayer2.Left = m_LabelPlayer1.Left;
            m_CheckBoxPlayer2.Top = m_LabelPlayer2.Top;
            m_CheckBoxPlayer2.AutoSize = true;
            m_CheckBoxPlayer2.Click += new EventHandler(m_CheckBox_Click);
            this.Controls.Add(m_CheckBoxPlayer2);

            m_LabelBoardSize = new Label();
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Top = m_LabelPlayer2.Bottom + 20;
            m_LabelBoardSize.Left = m_LabelPlayers.Left;
            this.Controls.Add(m_LabelBoardSize);

            m_LabelRows = new Label();
            m_LabelRows.Text = "Rows:";
            m_LabelRows.Top = m_LabelBoardSize.Bottom + 8;
            m_LabelRows.Left = m_LabelBoardSize.Left + 8;
            m_LabelRows.AutoSize = true;
            this.Controls.Add(m_LabelRows);

            m_NumericUpDownRows = new NumericUpDown();
            m_NumericUpDownRows.Minimum = k_Min;
            m_NumericUpDownRows.Maximum = k_Max;
            m_NumericUpDownRows.Top = m_LabelRows.Top + m_LabelRows.Height / 2 - m_NumericUpDownRows.Height / 2;
            m_NumericUpDownRows.Left = m_LabelRows.Right + 4;
            m_NumericUpDownRows.Width = 40;
            m_NumericUpDownRows.Click += new EventHandler(m_NumericUpDown_Click);
            this.Controls.Add(m_NumericUpDownRows);

            m_LabelCols = new Label();
            m_LabelCols.Text = "Cols:";
            m_LabelCols.Top = m_LabelRows.Top;
            m_LabelCols.AutoSize = true;
            this.Controls.Add(m_LabelCols);

            m_NumericUpDownCols = new NumericUpDown();
            m_NumericUpDownCols.Minimum = k_Min;
            m_NumericUpDownCols.Maximum = k_Max;
            m_NumericUpDownCols.Width = 40;
            m_NumericUpDownCols.Top = m_LabelCols.Top + m_LabelCols.Height / 2 - m_NumericUpDownCols.Height / 2;
            m_NumericUpDownCols.Left = m_TextBoxPlayer1.Right - m_NumericUpDownCols.Width;
            m_NumericUpDownCols.Click += new EventHandler(m_NumericUpDown_Click);
            this.Controls.Add(m_NumericUpDownCols);

            m_LabelCols.Left = m_NumericUpDownCols.Left - 32;


            m_ButtonStart = new Button();
            m_ButtonStart.Text = "Start!";
            m_ButtonStart.Left = m_TextBoxPlayer1.Right - m_ButtonStart.Width;
            m_ButtonStart.Top = m_NumericUpDownRows.Bottom + 32;
            m_ButtonStart.Click += new EventHandler(m_ButtonStart_Click);
            this.Controls.Add(m_ButtonStart);

            this.Text = "Game Settings:";
            this.ClientSize = new Size(m_ButtonStart.Right + 16, m_ButtonStart.Bottom + 16);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        }

        private void m_ButtonStart_Click(object sender, EventArgs e)
        {
            if (Authenticator.CheckUserCredentials((int)m_NumericUpDownRows.Value, (int)m_NumericUpDownCols.Value))
            {
                this.Close();
                int AmountOfPlayers = m_CheckBoxPlayer2.Checked ? 2 : 1;
                GameForm gameForm =
                    new GameForm(AmountOfPlayers,
                                (int)m_NumericUpDownRows.Value, (int)m_NumericUpDownCols.Value,
                                m_TextBoxPlayer1.Text, m_TextBoxPlayer2.Text);
                gameForm.ShowDialog();

            }
            else
            {
                MessageBox.Show("Dude.... Rows & Cols must be the same", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void m_CheckBox_Click(object sender, EventArgs e)
        {
            m_TextBoxPlayer2.Enabled = !m_TextBoxPlayer2.Enabled;
            m_TextBoxPlayer2.Text = m_TextBoxPlayer2.Enabled ? "" : "[Computer]";
        }

        private void m_NumericUpDown_Click(object sender, EventArgs e)
        {
            m_NumericUpDownCols.Value = m_NumericUpDownRows.Value = (sender as NumericUpDown).Value;
        }

    }

}
