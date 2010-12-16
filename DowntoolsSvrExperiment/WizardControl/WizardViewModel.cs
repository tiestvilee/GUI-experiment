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
        private WizardPage m_CurrentPage;
        private Stack<WizardPage> m_PreviousPages;

        public WizardViewModel(IWizardView view, WizardPage firstPage)
        {
            m_View = view;
            m_FirstPage = firstPage;
            m_PreviousPages = new Stack<WizardPage>();
        }

        public void Notify(object origin)
        {
            throw new NotImplementedException();
        }

        public void MoveToNextPage()
        {
            IncrementCurrentPage();

            UpdateViewWithNewPage(m_CurrentPage);
        }

        private void IncrementCurrentPage()
        {
            if (m_CurrentPage == null)
            {
                m_CurrentPage = m_FirstPage;
            }
            else
            {
                m_PreviousPages.Push(m_CurrentPage);
                m_CurrentPage = m_CurrentPage.GetNextPage();
            }
        }

        public void MoveToPreviousPage()
        {
            m_CurrentPage = m_PreviousPages.Pop();

            UpdateViewWithNewPage(m_CurrentPage);
        }

        private void UpdateViewWithNewPage(WizardPage newPage)
        {
            m_View.SetPage(newPage.GetControl());
            m_View.EnableBackButton(newPage != m_FirstPage);
            m_View.EnableNextButton(newPage.ReadyToMove());
            m_View.SetNextButtonName(newPage.GetNextButtonText());
            m_View.EnableCancelButton(true);
            m_CurrentPage.AddChangeListener(this);
        }
    }
}
