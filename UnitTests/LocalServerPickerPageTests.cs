using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DowntoolsSvrExperiment.Connection;
using DowntoolsSvrExperiment.VRPages.LocalServerPicker;
using NUnit.Framework;
using Rhino.Mocks;
using UnitTests.Support;

namespace UnitTests
{
    [TestFixture]
    public class LocalServerPickerPageTests : MockingBase
    {
        private TestConnection m_TestConnection;
        private ConnectionStatus m_PassingConnectionStatus;
        private ConnectionStatus m_FailingConnectionStatus;
        private StubbedLocalServerPickerView m_DefaultView;
        private Connection m_DefaultConnection;
        private GetLocalInstances m_GetLocalInstances;

        [SetUp]
        public void SetUp()
        {
            m_TestConnection = m_Mocks.StrictMock<TestConnection>();
            m_PassingConnectionStatus = new ConnectionStatus(true, "", new List<DbObjectName>(), "datapath");
            m_FailingConnectionStatus = new ConnectionStatus(false, "you're just plain wrong", new List<DbObjectName>(), "");
            m_DefaultView = new StubbedLocalServerPickerView(null)
                .Instance("instance")
                .SecurityType(SecurityType.SqlServerAuth)
                .UserName("tiestv")
                .Password("rocks!");

            m_DefaultConnection = new Connection("instance", SecurityType.SqlServerAuth, "tiestv", "rocks!");
            m_GetLocalInstances = m_Mocks.PartialMock<StubbedGetLocalInstances>();
        }

        [Test]
        public void ShouldReturnWidgetAndNextPageAndFormIsEnabled()
        {
            Given();
            var widget = new UserControl();
            var view = new StubbedLocalServerPickerView(widget);
            var nextPage = new StubbedPage(null, null, null, null);

            When();
            var localServerPickerPage = new LocalServerPickerPage(view, nextPage, null, m_GetLocalInstances);

            Then();
            Assert.That(m_DefaultView.FormEnabled, Is.True);
            Assert.That(localServerPickerPage.GetControl(), Is.EqualTo(widget));
            Assert.That(localServerPickerPage.GetNextPage(), Is.EqualTo(nextPage));
        }

        [Test]
        public void ShouldReturnCorrectName()
        {
            Given();

            When();
            var localServerPickerPage = new LocalServerPickerPage(m_DefaultView, null, null, m_GetLocalInstances);

            Then();
            Assert.That(localServerPickerPage.getName(), Is.EqualTo("Choose Local Server"));
        }

        [Test]
        public void NotReadyToMoveIfInstanceNotSelected()
        {
            Given();
            var view = new StubbedLocalServerPickerView(null)
                .Instance(null);


            When();
            var localServerPickerPage = new LocalServerPickerPage(view, null, null, m_GetLocalInstances);
            var readyToMove = localServerPickerPage.ReadyToMove();

            Then();
            Assert.That(readyToMove, Is.False);
        }

        [Test]
        public void ReadyToMoveIfInstanceSelectedAndUsingIntegratedSecurity()
        {
            Given();
            var view = new StubbedLocalServerPickerView(null)
                .Instance("something");
            view.SecurityType(SecurityType.Integrated);

            When();
            var localServerPickerPage = new LocalServerPickerPage(view, null, null, m_GetLocalInstances);
            var readyToMove = localServerPickerPage.ReadyToMove();

            Then();
            Assert.That(readyToMove, Is.True);
        }

        [Test]
        public void ReadyToMoveIfInstanceSelectedUsingSqlAuthAndUserPasswordProvided()
        {
            Given();
            var view = 
                new StubbedLocalServerPickerView(null)
                    .Instance("soemthingelse")
                    .SecurityType(SecurityType.SqlServerAuth)
                    .UserName("tiest")
                    .Password("rocks");

            When();
            var localServerPickerPage = new LocalServerPickerPage(view, null, null, m_GetLocalInstances);
            var readyToMove = localServerPickerPage.ReadyToMove();

            Then();
            Assert.That(readyToMove, Is.True);
        }

        [Test]
        public void NotReadyToMoveIfUsernameNotSupplied()
        {
            Given();
            var view =
                new StubbedLocalServerPickerView(null)
                    .Instance("soemthingelseelse")
                    .SecurityType(SecurityType.SqlServerAuth)
                    .Password("rocks");

            When();
            var localServerPickerPage = new LocalServerPickerPage(view, null, null, m_GetLocalInstances);
            var readyToMove = localServerPickerPage.ReadyToMove();

            Then();
            Assert.That(readyToMove, Is.False);
        }

        [Test]
        public void ShouldConstructConnectionInformationFromView()
        {
            Given();
            var connection = new Connection(
                m_DefaultView.GetInstance(), 
                m_DefaultView.GetSecurityType(), 
                m_DefaultView.GetUserName(), 
                m_DefaultView.GetPassword());
            Expect.Call(m_TestConnection.Test(connection)).Return(m_PassingConnectionStatus);

            When();
            var localServerPickerPage = new LocalServerPickerPage(m_DefaultView, null, m_TestConnection, m_GetLocalInstances);
            localServerPickerPage.PostValidate(() => { });

            Then();
        }

        [Test]
        public void PassesPostValidationIfConnectionSucceeds()
        {
            Given();
            var postValidateCalled = false;

            Expect.Call(m_TestConnection.Test(m_DefaultConnection)).Return(m_PassingConnectionStatus);

            When();
            var localServerPickerPage = new LocalServerPickerPage(m_DefaultView, null, m_TestConnection, m_GetLocalInstances);
            localServerPickerPage.PostValidate(() => { postValidateCalled = true; });

            Then();
            Assert.That(postValidateCalled, Is.True);
        }

        [Test]
        public void FailsPostValidationIfConnectionFailsAndProvidesWarningToUser()
        {
            Given();
            var postValidateCalled = false;

            Expect.Call(m_TestConnection.Test(m_DefaultConnection)).Return(m_FailingConnectionStatus);

            When();
            var localServerPickerPage = new LocalServerPickerPage(m_DefaultView, null, m_TestConnection, m_GetLocalInstances);
            localServerPickerPage.PostValidate(() => { postValidateCalled = true; });

            Then();
            Assert.That(postValidateCalled, Is.False);
            Assert.That(m_DefaultView.FormEnabled, Is.True);
            Assert.That(m_DefaultView.WarningText, Is.EqualTo(m_FailingConnectionStatus.ErrorMessage));
        }

        [Test]
        public void ShouldProvideListOfInstancesToView()
        {
            Given();
            var expectedInstances = new List<string>();
            expectedInstances.Add("(local)");
            expectedInstances.Add("SQL2008");
            Expect.Call(m_GetLocalInstances.LocalInstances()).Return(expectedInstances);

            When();
            new LocalServerPickerPage(m_DefaultView, null, null, m_GetLocalInstances);
            
            Then();
            Assert.That(m_DefaultView.Instances, Is.EqualTo(expectedInstances));
        }

        [Test]
        public void ShouldWarnUserWhenNoLocalInstancesAvailableAndDisableForm()
        {
            Given();
            var emptyInstances = new List<string>();
            Expect.Call(m_GetLocalInstances.LocalInstances()).Return(emptyInstances);

            When();
            new LocalServerPickerPage(m_DefaultView, null, null, m_GetLocalInstances);

            Then();
            Assert.That(m_DefaultView.Instances, Is.Null);
            Assert.That(m_DefaultView.FormEnabled, Is.False);
            Assert.That(m_DefaultView.WarningText, Is.EqualTo("There are no local SQL Server instances on this computer.  " +
                "Try running the SQL Virtual Restore Wizard on a computer that has a SQL Server instance installed."));
        }

    }

    public class StubbedGetLocalInstances : GetLocalInstances
    {
        public override IEnumerable<string> LocalInstances()
        {
            var localInstances = new List<string>();
            localInstances.Add("(local)");
            return localInstances;
        }
    }

    class StubbedLocalServerPickerView : LocalServerPickerView
    {
        private readonly UserControl m_Control;
        private string m_Instance;
        private SecurityType m_SecurityType;
        private string m_Password;
        private string m_UserName;

        public IEnumerable<string> Instances;
        public bool FormEnabled = true;
        public string WarningText;

        public StubbedLocalServerPickerView(UserControl control)
        {
            m_Control = control;
        }

        public UserControl GetControl()
        {
            return m_Control;
        }

        public string GetInstance()
        {
            return m_Instance;
        }

        public SecurityType GetSecurityType()
        {
            return m_SecurityType;
        }

        public string GetUserName()
        {
            return m_UserName;
        }

        public string GetPassword()
        {
            return m_Password;
        }

        public void SetLocalInstances(IEnumerable<string> localInstances)
        {
            Instances = localInstances;
        }

        public void ShowWarning(string errorMessage)
        {
            WarningText = errorMessage;
        }

        public void SetFormEnabled(bool enabled)
        {
            FormEnabled = enabled;
        }

        public StubbedLocalServerPickerView Instance(string instance)
        {
            m_Instance = instance;
            return this;
        }

        public StubbedLocalServerPickerView SecurityType(SecurityType securityType)
        {
            m_SecurityType = securityType;
            return this;
        }

        public StubbedLocalServerPickerView Password(string password)
        {
            m_Password = password;
            return this;
        }

        public StubbedLocalServerPickerView UserName(string userName)
        {
            m_UserName = userName;
            return this;
        }
    }
}
