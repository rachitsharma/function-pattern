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
    class TextEditor : IActionRecordable
    {
        TextBox m_textbox;
        TextEditRecorder m_recorder;

        public string CurrentText { set; get; }

        public TextEditor(TextBox textBox)
        {
            m_textbox = textBox;
            m_recorder = new TextEditRecorder(this);
        }

        public void SetText(string strText)
        {
            m_recorder.SetText(strText);
        }

        #region IActionRecordable 成员

        public ActionRecorder GetActonRecorder()
        {
            return m_recorder;
        }

        #endregion

        #region nested class
        class TextEditRecorder : ActionRecorder
        {
            TextEditRecord m_currentRecord;
            TextEditor m_host;

            public TextEditRecorder(TextEditor host)
            {
                m_host = host;
            }

            protected override object RecorderIdCore
            {
                get
                {
                    return typeof(TextEditor);
                }
            }

            protected override ActionRecord StartRecordCore()
            {
                m_currentRecord = new TextEditRecord(m_host.m_textbox);
                m_currentRecord.SetOldText(m_host.CurrentText);
                return m_currentRecord;
            }

            protected override void EndRecordCore()
            {
                m_currentRecord = null;
            }

            protected override void CutRecordCore()
            {
                m_currentRecord = null;
            }

            protected override ActionRecord GenerateEmptyRecordCore()
            {
                var empty = new TextEditRecord(m_host.m_textbox);
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
        #endregion
    }
}
