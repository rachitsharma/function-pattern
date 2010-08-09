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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FunctionPattern.Chain4Action;

namespace Sample
{
    public partial class MultiActorSampleForm : SampleForm
    {
        ChainManager m_chainManager;
        TextEditor m_textEdiorRecorder1;
        TextEditor m_textEdiorRecorder2;

        public MultiActorSampleForm()
        {
            InitializeComponent();

            m_textEdiorRecorder1 = new TextEditor(textBox1);
            m_textEdiorRecorder2 = new TextEditor(textBox2);

            ChainManager.IniParams param = new ChainManager.IniParams()
            {
                DontRecordWhenStatusError = true,
            };

            param.RecordCollection.Add(m_textEdiorRecorder1);
            param.RecordCollection.Add(m_textEdiorRecorder2);

            m_chainManager = new ChainManager(param);
            CheckUndoRedoStatus();
        }

        private void OnUndo(object sender, EventArgs e)
        {
            m_chainManager.Undo();
            CheckUndoRedoStatus();
        }

        private void OnRedo(object sender, EventArgs e)
        {
            m_chainManager.Redo();
            CheckUndoRedoStatus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (m_chainManager.ActStatus == ActStatus.None)
            {
                m_chainManager.StartRecord();
                m_textEdiorRecorder1.SetText(textBox1.Text);
                m_chainManager.EndRecord();

                CheckUndoRedoStatus();
            }

            m_textEdiorRecorder1.CurrentText = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (m_chainManager.ActStatus == ActStatus.None)
            {
                m_chainManager.StartRecord();
                m_textEdiorRecorder2.SetText(textBox2.Text);
                m_chainManager.EndRecord();

                CheckUndoRedoStatus();
            }

            m_textEdiorRecorder2.CurrentText = textBox2.Text;
        }

        void CheckUndoRedoStatus()
        {
            buttonUndo.Enabled = m_chainManager.CanUndo;
            buttonRedo.Enabled = m_chainManager.CanRedo;
        }
    }
}
