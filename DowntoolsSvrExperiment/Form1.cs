using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DowntoolsSvrExperiment.WizardControl;

namespace DowntoolsSvrExperiment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var wizardView = new WizardViewWidget();
            var secondPage = new PageExample(null, "second page");
            var firstPage = new PageExample(secondPage, "first page");
            new WizardViewModel(wizardView, firstPage, cancel, cancel);

            Controls.Add(wizardView);
        }

        private void cancel()
        {
            this.Close();
        }
    }

    public class PageExample : WizardPage
    {
        private readonly WizardPage m_NextPage;
        private readonly string m_PageName;

        public PageExample(WizardPage nextPage, string pageName)
        {
            m_NextPage = nextPage;
            m_PageName = pageName;
        }

        public UserControl GetControl()
        {
            return new ExamplePageUserWidget();
        }

        public void OnChangeDo(Action onChangeAction)
        {
        }

        public bool ReadyToMove()
        {
            return true;
        }

        public WizardPage GetNextPage()
        {
            return m_NextPage;
        }

        public string GetNextButtonText()
        {
            return "Next";
        }

        public string getName()
        {
            return m_PageName;
        }
    }


}
