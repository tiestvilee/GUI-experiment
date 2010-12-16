using System.Windows.Forms;

namespace DowntoolsSvrExperiment.WizardControl
{
    public interface IWizardView
    {
        void SetPage(UserControl pageControl);
        void EnableBackButton(bool b);
        void EnableNextButton(bool b);
        void SetNextButtonName(string name);
        void EnableCancelButton(bool b);
    }
}