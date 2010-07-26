using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sample
{
    public partial class SampleViewForm : Form
    {
        public SampleViewForm()
        {
            InitializeComponent();
        }

        private void OnRunSingleText(object sender, EventArgs e)
        {
            new TextSampleForm().ShowDialog();
        }

        private void OnRunMultiActor(object sender, EventArgs e)
        {
            new MultiActorSampleForm().ShowDialog();
        }
    }
}
