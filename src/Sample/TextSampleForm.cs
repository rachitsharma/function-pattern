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
    public partial class TextSampleForm : SampleForm
    {
        ChainManager m_chainManager;
        TextEditor m_recorder;

        public TextSampleForm()
        {
            InitializeComponent();

            m_recorder = new TextEditor(textBox1);

            ChainManager.IniParams iniParams = new ChainManager.IniParams();
            iniParams.RecordCollection.Add(m_recorder);
            iniParams.DontRecordWhenStatusError = true;
            m_chainManager = new ChainManager(iniParams);
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            m_chainManager.Undo();
            CheckUndoRedoStatus();
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            m_chainManager.Redo();
            CheckUndoRedoStatus();
        }

        private void TextSampleForm_Load(object sender, EventArgs e)
        {
            CheckUndoRedoStatus();
        }

        void CheckUndoRedoStatus()
        {
            buttonUndo.Enabled = m_chainManager.CanUndo;
            buttonRedo.Enabled = m_chainManager.CanRedo;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (m_chainManager.ActStatus == ActStatus.None)
            {
                m_chainManager.StartRecord();
                m_recorder.SetText(textBox1.Text);
                m_chainManager.EndRecord();

                CheckUndoRedoStatus();
            }

            m_recorder.CurrentText = textBox1.Text;
        }
    }
}
