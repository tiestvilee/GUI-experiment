using System.Collections.Generic;
using System.Windows.Forms;
using DowntoolsSvrExperiment.Utilities;

namespace DowntoolsSvrExperiment.VRPages.LocalServerPicker
{
    public interface LocalServerPickerView
    {
        UserControl GetControl();
        string GetInstance();
        SecurityType GetSecurityType();
        string GetUserName();
        string GetPassword();
        void SetLocalInstances(IEnumerable<string> localInstances);
        void ShowWarning(string warningMessage);
        void SetFormEnabledState(EnabledState enabled);
        void OnSecurityTypeChange(Action doThis);
    }

    public enum SecurityType
    {
        Integrated, SqlServerAuth
    }

    public enum EnabledState
    {
        Integrated, SqlServerAuth, Disabled
    }
}
