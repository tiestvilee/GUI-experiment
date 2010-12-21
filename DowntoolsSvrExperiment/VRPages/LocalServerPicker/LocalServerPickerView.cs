﻿using System.Collections.Generic;
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
        void SetLocalInstances(IEnumerable<string> localInstances);
        void ShowWarning(string warningMessage);
        void SetFormEnabled(bool enabled);
    }

    public enum SecurityType
    {
        Integrated, SqlServerAuth
    }
}
