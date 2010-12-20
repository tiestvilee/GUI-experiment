using System.Windows.Forms;
using DowntoolsSvrExperiment.Utilities;

namespace DowntoolsSvrExperiment.WizardControl
{
    public abstract class WizardPage
    {
        private readonly WizardPage m_NextPage;

        public WizardPage(WizardPage nextPage)
        {
            m_NextPage = nextPage;
        }


        public virtual WizardPage GetNextPage()
        {
            return m_NextPage;
        }

        public virtual string GetNextButtonText()
        {
            return "Next";
        }

        public abstract UserControl GetControl();
        public abstract void OnChangeDo(Action onChangeAction);
        public abstract bool ReadyToMove();
        public abstract string getName();

        public abstract void PostValidate(Action andThen);
    }
}
