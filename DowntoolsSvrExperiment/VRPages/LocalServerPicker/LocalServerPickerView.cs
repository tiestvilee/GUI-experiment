using System.Windows.Forms;

namespace DowntoolsSvrExperiment.VRPages.LocalServerPicker
{
    public interface LocalServerPickerView
    {
        UserControl GetControl();
        string GetInstance();
        SecurityType GetSecurityType();
        string GetUserName();
        string GetPassword();
    }

    public enum SecurityType
    {
        Integrated, SqlServerAuth
    }
}
