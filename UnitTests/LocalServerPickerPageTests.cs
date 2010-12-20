using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DowntoolsSvrExperiment.Connection;
using DowntoolsSvrExperiment.Utilities;
using DowntoolsSvrExperiment.VRPages.LocalServerPicker;
using DowntoolsSvrExperiment.WizardControl;
using NUnit.Framework;
using Rhino.Mocks;

namespace UnitTests
{
    [TestFixture]
    public class LocalServerPickerPageTests : MockingBase
    {
        private TestConnection m_TestConnection;
        private ConnectionStatus m_PassingConnectionStatus;
        private ConnectionStatus m_FailingConnectionStatus;
        private LocalServerPickerView m_DefaultView;
        private Connection m_DefaultConnection;

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
        }

        [Test]
        public void ShouldReturnWidgetAndNextPage()
        {
            // Given
            var widget = new UserControl();
            var view = new StubbedLocalServerPickerView(widget);
            var nextPage = new StubbedPage(null, null, null, null);

            // When
            var localServerPickerPage = new LocalServerPickerPage(view, nextPage, null);

            // Then
            Assert.That(localServerPickerPage.GetControl(), Is.EqualTo(widget));
            Assert.That(localServerPickerPage.GetNextPage(), Is.EqualTo(nextPage));
        }

        [Test]
        public void ShouldReturnCorrectName()
        {
            var localServerPickerPage = new LocalServerPickerPage(null, null, null);
            Assert.That(localServerPickerPage.getName(), Is.EqualTo("Choose Local Server"));
        }

        [Test]
        public void NotReadyToMoveIfInstanceNotSelected()
        {
            // Given
            var view = new StubbedLocalServerPickerView(null)
                .Instance(null);


            var localServerPickerPage = new LocalServerPickerPage(view, null, null);

            // When
            var readyToMove = localServerPickerPage.ReadyToMove();

            // Then
            Assert.That(readyToMove, Is.False);
        }

        [Test]
        public void ReadyToMoveIfInstanceSelectedAndUsingIntegratedSecurity()
        {
            // Given
            var view = new StubbedLocalServerPickerView(null)
                .Instance("something");
            view.SecurityType(SecurityType.Integrated);

            var localServerPickerPage = new LocalServerPickerPage(view, null, null);

            // When
            var readyToMove = localServerPickerPage.ReadyToMove();

            // Then
            Assert.That(readyToMove, Is.True);
        }

        [Test]
        public void ReadyToMoveIfInstanceSelectedUsingSqlAuthAndUserPasswordProvided()
        {
            // Given
            var view = 
                new StubbedLocalServerPickerView(null)
                    .Instance("soemthingelse")
                    .SecurityType(SecurityType.SqlServerAuth)
                    .UserName("tiest")
                    .Password("rocks");

            var localServerPickerPage = new LocalServerPickerPage(view, null, null);

            // When
            var readyToMove = localServerPickerPage.ReadyToMove();

            // Then
            Assert.That(readyToMove, Is.True);
        }

        [Test]
        public void NotReadyToMoveIfUsernameNotSupplied()
        {
            // Given
            var view =
                new StubbedLocalServerPickerView(null)
                    .Instance("soemthingelseelse")
                    .SecurityType(SecurityType.SqlServerAuth)
                    .Password("rocks");

            var localServerPickerPage = new LocalServerPickerPage(view, null, null);

            // When
            var readyToMove = localServerPickerPage.ReadyToMove();

            // Then
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
            var localServerPickerPage = new LocalServerPickerPage(m_DefaultView, null, m_TestConnection);
            Expect.Call(m_TestConnection.Test(connection)).Return(m_PassingConnectionStatus);

            When();
            localServerPickerPage.PostValidate(() => {});

            Then();
        }

        [Test]
        public void PassesPostValidationIfConnectionSucceeds()
        {
            Given();
            var postValidateCalled = false;
            var localServerPickerPage = new LocalServerPickerPage(m_DefaultView, null, m_TestConnection);

            Expect.Call(m_TestConnection.Test(m_DefaultConnection)).Return(m_PassingConnectionStatus);

            When();
            localServerPickerPage.PostValidate(() => { postValidateCalled = true; });

            Then();
            Assert.That(postValidateCalled, Is.True);
        }

        [Test]
        public void FailsPostValidationIfConnectionFails()
        {
            Given();
            var postValidateCalled = false;
            var localServerPickerPage = new LocalServerPickerPage(m_DefaultView, null, m_TestConnection);

            Expect.Call(m_TestConnection.Test(m_DefaultConnection)).Return(m_FailingConnectionStatus);

            When();
            localServerPickerPage.PostValidate(() => { postValidateCalled = true; });

            Then();
            Assert.That(postValidateCalled, Is.False);
        }


    }

    class StubbedLocalServerPickerView : LocalServerPickerView
    {
        private readonly UserControl m_Control;
        private string m_Instance;
        private SecurityType m_SecurityType;
        private string m_Password;
        private string m_UserName;

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
