#region License
/*
Copyright (c) 2010 ShanGuanDa etc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunctionPattern.Chain4Action;
using System.Windows.Forms;

namespace Sample
{
    class TextEditRecord : ActionRecord
    {
        bool m_isEmpty;
        string m_newText;
        string m_oldText;
        TextBox m_textBox;

        public TextEditRecord(TextEditRecorder myRecoder, TextBox textBox)
            : base(myRecoder)
        {
            m_textBox = textBox;
        }

        public override bool IsEmpty
        {
            get
            {
                return m_isEmpty;
            }
        }

        public override void ToEmpty()
        {
            m_isEmpty = true;
        }

        public override void Redo()
        {
            m_textBox.Text = m_newText;
        }

        public override void Undo()
        {
            m_textBox.Text = m_oldText;
        }

        public void SetOldText(string strOldText)
        {
            m_oldText = strOldText;
        }

        public void SetNewText(string strNewText)
        {
            m_newText = strNewText;
        }
    }
}
