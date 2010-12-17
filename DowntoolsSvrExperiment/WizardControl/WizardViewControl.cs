﻿using System;
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
            m_CancelButton.Enabled = b;
        }

        public void SetPageList(List<PageNameAndCurrent> pages)
        {
            try
            {
                m_PageList.SuspendLayout();

                m_PageList.Controls.Clear();

                foreach (var pageNameAndCurrent in pages)
                {
                    var label = new Label();
                    label.Text = pageNameAndCurrent.Name;
                    label.ForeColor = pageNameAndCurrent.CurrentPage ? Color.Black : SystemColors.GrayText;
                    label.Margin = new Padding(3,3,3,3);
                    label.Tag = pageNameAndCurrent; // ?? 
                    m_PageList.Controls.Add(label);
                    label.Dock = DockStyle.Top;
                    label.BringToFront(); /* fix up z-order */
                }
            }
            finally
            {
                ResumeLayout();
            }
        }

        private void m_PreviousButton_Click(object sender, EventArgs e)
        {

        }

        private void m_NextButton_Click(object sender, EventArgs e)
        {

        }

        private void m_CancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}
