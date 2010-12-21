using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DowntoolsSvrExperiment.Utilities;

namespace DowntoolsSvrExperiment.VRPages.LocalServerPicker
{
    public partial class LocalServerPickerWidget : UserControl, LocalServerPickerView
    {
        private Action m_SecurityTypeChangeAction;

        public LocalServerPickerWidget()
        {
            InitializeComponent();
        }

        public UserControl GetControl()
        {
            return this;
        }

        public string GetInstance()
        {
            return m_Instances.SelectedText;
        }

        public SecurityType GetSecurityType()
        {
            if(m_WindowsAuth.Checked)
            {
                return SecurityType.Integrated;
            }
            return SecurityType.SqlServerAuth;
        }

        public string GetUserName()
        {
            return m_UserName.Text;
        }

        public string GetPassword()
        {
            return m_Password.Text;
        }

        public void SetLocalInstances(IEnumerable<string> localInstances)
        {
            m_Instances.Items.Clear();
            foreach(var instance in localInstances)
            {
                m_Instances.Items.Add(instance);
            }
        }

        public void ShowWarning(string warningMessage)
        {
            m_WarningText.Text = warningMessage;
        }

        public void SetFormEnabledState(EnabledState enabled)
        {
            switch (enabled)
            {
                case EnabledState.Integrated:
                    m_Instances.Enabled = true;
                    break;
                case EnabledState.SqlServerAuth:
                    m_Instances.Enabled = true;
                    break;
                case EnabledState.Disabled:
                    m_Instances.Enabled = false;
                    break;
            }
        }

        public void OnSecurityTypeChange(Action doThis)
        {
            m_SecurityTypeChangeAction = doThis;
        }
    }
}
