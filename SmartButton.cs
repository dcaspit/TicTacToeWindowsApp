﻿using System;
using System.Windows.Forms;


namespace Ex05
{
    public class SmartButton : Button
    {
        private int m_Row;
        public int Row
        {
            get { return m_Row; }
        }

        private int m_Col;
        public int Col
        {
            get { return m_Col; }
        }

        public SmartButton(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

    }
}
