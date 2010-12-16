using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DowntoolsSvrExperiment.WizardControl
{
    public class WizardViewModel : ChangeListener
    {
        private readonly IWizardView m_View;
        private readonly WizardPage m_FirstPage;

        public WizardViewModel(IWizardView view, WizardPage firstPage)
        {
            m_View = view;
            m_FirstPage = firstPage;

            view.SetPage(firstPage.GetControl());
            m_View.EnableBackButton(false);
            m_View.EnableNextButton(false);
            m_View.EnableFinishButton(false);
            m_View.EnableCancelButton(true);
            firstPage.AddChangeListener(this);
        }

        public void Notify(object origin)
        {
            throw new NotImplementedException();
        }
    }
}
