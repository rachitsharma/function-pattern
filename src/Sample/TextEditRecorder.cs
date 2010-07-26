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
    class TextEditRecorder : ActionRecorder
    {
        TextEditRecord m_currentRecord;
        TextBox m_textbox;

        public string CurrentText { set; get; }

        public TextEditRecorder(TextBox textBox)
        {
            m_textbox = textBox;
        }

        protected override ActorId ActorIdCore
        {
            get
            {
                return ActorId.Generate<TextEditRecorder>();
            }
        }

        protected override ActionRecord StartRecordCore()
        {
            m_currentRecord = new TextEditRecord(this, m_textbox);
            m_currentRecord.SetOldText(CurrentText);
            return m_currentRecord;
        }

        protected override void EndRecordCore()
        {
            m_currentRecord = null;
        }

        protected override ActionRecord GenerateEmptyRecordCore()
        {
            var empty = new TextEditRecord(this, m_textbox);
            empty.ToEmpty();
            return empty;
        }

        public void SetText(string strText)
        {
            if (m_currentRecord != null)
            {
                m_currentRecord.SetNewText(strText);
            }
        }
    }
}
