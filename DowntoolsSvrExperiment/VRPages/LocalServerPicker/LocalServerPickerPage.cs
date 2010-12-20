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

        public LocalServerPickerPage(LocalServerPickerView view, WizardPage nextPage, TestConnection testConnection) : base(nextPage)
        {
            m_View = view;
            m_TestConnection = testConnection;
        }

        public override UserControl GetControl()
        {
            return m_View.GetControl();
        }

        public override void OnChangeDo(Action onChangeAction)
        {
            throw new NotImplementedException();
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
            }
        }
    }
}
