using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DowntoolsSvrExperiment.WizardControl
{
    public interface WizardPage
    {
        UserControl GetControl();
        void AddChangeListener(ChangeListener listener);
        void FireChangeEvent();
        bool ReadyToMove();
        WizardPage GetNextPage();
        string GetNextButtonText();
    }
}
