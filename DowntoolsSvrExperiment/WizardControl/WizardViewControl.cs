using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DowntoolsSvrExperiment.WizardControl
{
    public partial class WizardViewControl : UserControl, WizardView
    {
        public WizardViewControl()
        {
            InitializeComponent();
        }

        public void SetPage(UserControl pageControl, string pageName)
        {
            try
            {
                m_Page.SuspendLayout();
                m_Page.Controls.Clear();
                m_Page.Controls.Add(pageControl);

                m_PageName.Text = pageName;
            }
            finally
            {
                m_Page.ResumeLayout();
            }
        }

        public void EnableBackButton(bool b)
        {
            m_PreviousButton.Enabled = b;
        }

        public void EnableNextButton(bool b)
        {
            m_NextButton.Enabled = b;
        }

        public void SetNextButtonName(string name)
        {
            m_NextButton.Text = name;
        }

        public void EnableCancelButton(bool b)
        {
            throw new NotImplementedException();
        }

        public void SetPageList(List<PageNameAndCurrent> pages)
        {
            throw new NotImplementedException();
        }
    }
}
