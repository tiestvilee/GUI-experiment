using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DowntoolsSvrExperiment.WizardControl
{
    public interface ChangeListener
    {
        void Notify(Object origin);
    }
}
