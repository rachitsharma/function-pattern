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
    public partial class TextSampleForm : Form
    {
        ChainManager m_chainManager;
        TextEditRecorder m_recorder;

        public TextSampleForm()
        {
            InitializeComponent();

            m_recorder = new TextEditRecorder(textBox1);

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
