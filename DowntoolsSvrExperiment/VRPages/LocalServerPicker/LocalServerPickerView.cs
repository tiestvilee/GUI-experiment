using System.Windows.Forms;

namespace DowntoolsSvrExperiment.VRPages.LocalServerPicker
{
    public interface LocalServerPickerView
    {
        UserControl getControl();
        string GetInstance();
        SecurityType GetSecurityType();
        string GetUserName();
    }

    public enum SecurityType
    {
        Integrated, SqlServerAuth
    }
}
