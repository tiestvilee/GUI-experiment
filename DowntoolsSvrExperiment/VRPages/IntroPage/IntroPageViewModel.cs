using System.Windows.Forms;
using DowntoolsSvrExperiment.Utilities;
using DowntoolsSvrExperiment.WizardControl;

namespace DowntoolsSvrExperiment.VRPages.IntroPage
{
    class IntroPageViewModel : WizardPage
    {
        private readonly UserControl m_Widget;

        public IntroPageViewModel(UserControl widget, WizardPage nextPage) : base(nextPage)
        {
            m_Widget = widget;
        }

        public override UserControl GetControl()
        {
            return m_Widget;
        }

        public override void OnChangeDo(Action onChangeAction)
        {
            // don't care
        }

        public override bool ReadyToMove()
        {
            return true;
        }

        public override string getName()
        {
            return "Introduction";
        }

        public override void PostValidate(Action andThen)
        {
            andThen();
        }
    }
}
