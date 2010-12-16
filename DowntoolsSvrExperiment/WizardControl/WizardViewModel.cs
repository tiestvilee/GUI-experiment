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
        private readonly Action m_FinishAction;
        private readonly Stack<WizardPage> m_PreviousPages;
        private WizardPage m_CurrentPage;

        public WizardViewModel(IWizardView view, WizardPage firstPage, Action finishAction)
        {
            m_View = view;
            m_FirstPage = firstPage;
            m_FinishAction = finishAction;
            m_PreviousPages = new Stack<WizardPage>();

            m_CurrentPage = m_FirstPage;
            UpdateViewWithNewPage(m_CurrentPage);
        }

        public void Notify(object origin)
        {
            throw new NotImplementedException();
        }

        public void MoveToNextPage()
        {
            if(!m_CurrentPage.ReadyToMove())
            {
                throw new NotReadyToProceedException();
            }

            if (m_CurrentPage.GetNextPage() == null)
            {
                m_FinishAction.Invoke();
            }
            else
            {
                m_PreviousPages.Push(m_CurrentPage);
                m_CurrentPage = m_CurrentPage.GetNextPage();
                UpdateViewWithNewPage(m_CurrentPage);
            }
        }

        public void MoveToPreviousPage()
        {
            if(m_PreviousPages.Count == 0)
            {
                throw new MoveBeforeFirstPageException();
            }

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
            newPage.AddChangeListener(this);
        }
    }
}
