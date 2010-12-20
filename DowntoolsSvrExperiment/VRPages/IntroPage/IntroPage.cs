using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DowntoolsSvrExperiment.WizardControl;

namespace DowntoolsSvrExperiment.VRPages.IntroPage
{
    class IntroPage : WizardPage
    {
        private readonly UserControl m_Widget;
        private readonly WizardPage m_NextPage;

        public IntroPage(UserControl widget, WizardPage nextPage)
        {
            m_Widget = widget;
            m_NextPage = nextPage;
        }

        public UserControl GetControl()
        {
            return m_Widget;
        }

        public void OnChangeDo(Action onChangeAction)
        {
            // don't care
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
            return "Introduction";
        }
    }
}
