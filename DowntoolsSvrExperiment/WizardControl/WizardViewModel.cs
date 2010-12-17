using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DowntoolsSvrExperiment.WizardControl
{
    public class WizardViewModel
    {
        private readonly WizardView m_View;
        private readonly WizardPage m_FirstPage;
        private readonly Action m_FinishAction;
        private readonly Stack<WizardPage> m_PreviousPages;
        private WizardPage m_CurrentPage;

        public WizardViewModel(WizardView view, WizardPage firstPage, Action finishAction, Action cancelAction)
        {
            m_View = view;
            m_FirstPage = firstPage;
            m_FinishAction = finishAction;
            m_PreviousPages = new Stack<WizardPage>();

            m_View.OnCancelDo(cancelAction);
            m_View.OnNextDo(MoveToNextPage);
            m_View.OnPreviousDo(MoveToPreviousPage);

            m_CurrentPage = m_FirstPage;
            UpdateViewWithNewPage(m_CurrentPage);
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

        private void UpdateViewButtonState()
        {
            m_View.EnableNextButton(m_CurrentPage.ReadyToMove());
        }

        private void UpdateViewWithNewPage(WizardPage newPage)
        {
            m_View.SetPage(newPage.GetControl(), String.Format("Step {0} of {1}: {2}", GetCurrentPageIndexCount(), GetPageCount(), m_CurrentPage.getName()));
            m_View.EnableBackButton(newPage != m_FirstPage);
            m_View.EnableNextButton(newPage.ReadyToMove());
            m_View.SetNextButtonName(newPage.GetNextButtonText());
            m_View.EnableCancelButton(true);

            m_View.SetPageList(ConstructPageList());

            newPage.OnChangeDo(UpdateViewButtonState);
        }

        private int GetPageCount()
        {
            WizardPage cursor = m_FirstPage;
            int index = 0;
            while (cursor != null)
            {
                index++;
                cursor = cursor.GetNextPage();
            }
            return index;
        }

        private int GetCurrentPageIndexCount()
        {
            WizardPage cursor = m_FirstPage;
            int index = 0;
            while (cursor != null)
            {
                index++;
                if(cursor == m_CurrentPage)
                {
                    return index;
                }
                cursor = cursor.GetNextPage();
            }
            throw new Exception("Didn't find current page in list of pages");
        }

        private List<PageNameAndCurrent> ConstructPageList()
        {
            var pages = new List<PageNameAndCurrent>();
            WizardPage cursor = m_FirstPage;
            while (cursor != null)
            {
                pages.Add(new PageNameAndCurrent { CurrentPage = (cursor == m_CurrentPage), Name = cursor.getName() });
                cursor = cursor.GetNextPage();
            }
            return pages;
        }
    }
}
