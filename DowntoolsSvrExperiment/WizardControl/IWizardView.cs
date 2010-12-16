using System.Windows.Forms;

namespace DowntoolsSvrExperiment.WizardControl
{
    public interface IWizardView
    {
        void SetPage(UserControl pageControl);
        void EnableBackButton(bool b);
        void EnableNextButton(bool b);
        void EnableFinishButton(bool b);
        void EnableCancelButton(bool b);
    }
}