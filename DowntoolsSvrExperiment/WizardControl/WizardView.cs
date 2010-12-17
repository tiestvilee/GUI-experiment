using System.Collections.Generic;
using System.Windows.Forms;

namespace DowntoolsSvrExperiment.WizardControl
{
    public interface WizardView
    {
        void SetPage(UserControl pageControl, string pageName);
        void EnableBackButton(bool b);
        void EnableNextButton(bool b);
        void SetNextButtonName(string name);
        void EnableCancelButton(bool b);
        void SetPageList(List<PageNameAndCurrent> pages);
    }

    public struct PageNameAndCurrent
    {
        public string Name;
        public bool CurrentPage;
    }

}