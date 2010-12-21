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
        private Action m_OnChangeAction;

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
            var selectedIndex = m_Instances.SelectedIndex;
            if(selectedIndex > -1)
            {
                return (string)m_Instances.Items[selectedIndex];
            }
            return "";
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
                    EnableInstances(true);
                    EnableUserPassword(false);
                    break;
                case EnabledState.SqlServerAuth:
                    EnableInstances(true);
                    EnableUserPassword(true);
                    break;
                case EnabledState.Disabled:
                    EnableInstances(false);
                    EnableUserPassword(false);
                    break;
            }
        }

        private void EnableInstances(bool enabled)
        {
            m_InstanceLabel.Enabled = enabled;
            m_Instances.Enabled = enabled;
            m_WindowsAuth.Enabled = enabled;
            m_SqlServerAuth.Enabled = enabled;
        }

        private void EnableUserPassword(bool enabled)
        {
            m_PasswordLabel.Enabled = enabled;
            m_Password.Enabled = enabled;
            m_UserNameLabel.Enabled = enabled;
            m_UserName.Enabled = enabled;
        }

        public void OnChange(Action onChangeAction)
        {
            m_OnChangeAction = onChangeAction;
        }

        private void m_WindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            m_OnChangeAction();
        }

        private void m_SqlServerAuth_CheckedChanged(object sender, EventArgs e)
        {
            m_OnChangeAction();
        }

        private void m_Instances_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_OnChangeAction();
        }

        private void m_UserName_TextChanged(object sender, EventArgs e)
        {
            m_OnChangeAction();
        }

        private void m_Password_TextChanged(object sender, EventArgs e)
        {
            m_OnChangeAction();
        }
    }
}
