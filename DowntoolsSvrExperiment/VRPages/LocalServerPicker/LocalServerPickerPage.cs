using System;
using System.Windows.Forms;
using DowntoolsSvrExperiment.Connection;
using DowntoolsSvrExperiment.Utilities;
using DowntoolsSvrExperiment.WizardControl;

namespace DowntoolsSvrExperiment.VRPages.LocalServerPicker
{
    public class LocalServerPickerPage : WizardPage
    {
        private readonly LocalServerPickerView m_View;
        private readonly TestConnection m_TestConnection;
        private Action m_OnChangeAction;

        public LocalServerPickerPage(LocalServerPickerView view, WizardPage nextPage, TestConnection testConnection, GetLocalInstances getLocalInstances) : base(nextPage)
        {
            m_View = view;
            m_TestConnection = testConnection;
            m_View.SetFormEnabledState(EnabledState.Integrated);
            UpdateViewWithLocalInstances(getLocalInstances);
            view.OnChange(OnChangeAction);
        }

        private void OnChangeAction()
        {
            if (m_View.GetSecurityType() == SecurityType.Integrated)
            {
                m_View.SetFormEnabledState(EnabledState.Integrated);
            }
            else if (m_View.GetSecurityType() == SecurityType.SqlServerAuth)
            {
                m_View.SetFormEnabledState(EnabledState.SqlServerAuth);
            }

            m_OnChangeAction();
        }

        private void UpdateViewWithLocalInstances(GetLocalInstances getLocalInstances)
        {
            var localInstances = getLocalInstances.LocalInstances();
            if(localInstances.GetEnumerator().MoveNext())
            {
                m_View.SetLocalInstances(localInstances);
            } else
            {
                m_View.SetFormEnabledState(EnabledState.Disabled);
                m_View.ShowWarning("There are no local SQL Server instances on this computer.  Try running the SQL Virtual Restore Wizard on a computer that has a SQL Server instance installed.");
            }
        }

        public override UserControl GetControl()
        {
            return m_View.GetControl();
        }

        public override void OnChangeDo(Action onChangeAction)
        {
            m_OnChangeAction = onChangeAction;
        }

        public override bool ReadyToMove()
        {
            if (String.IsNullOrEmpty(m_View.GetInstance()))
            {
                return false;
            }

            if (m_View.GetSecurityType() != SecurityType.Integrated)
            {
                if(String.IsNullOrEmpty(m_View.GetUserName()))
                {
                    return false;
                }
            }

            return true;
        }

        public override string getName()
        {
            return "Choose Local Server";
        }

        public override void PostValidate(Action andThen)
        {
            var connection = new Connection.Connection(
                m_View.GetInstance(), 
                m_View.GetSecurityType(), 
                m_View.GetUserName(), 
                m_View.GetPassword());
            var status = m_TestConnection.Test(connection);
            if(status.Connected)
            {
                andThen();
            } else
            {
                m_View.ShowWarning(status.ErrorMessage);
            }
        }
    }
}
