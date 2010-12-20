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
        private TestConnection testConnection;

        [SetUp]
        public void SetUp()
        {
            testConnection = m_Mocks.StrictMock<TestConnection>();
        }

        [Test]
        public void ShouldReturnWidgetAndNextPage()
        {
            // Given
            var widget = new UserControl();
            var view = new StubbedView2(widget);
            var nextPage = new StubbedPage2(null);

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
            var view = new StubbedView2(null);
            view.Instance = null;

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
            var view = new StubbedView2(null);
            view.Instance = "something";
            view.SecurityType = SecurityType.Integrated;

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
            var view = new StubbedView2(null);
            view.Instance = "soemthingelse";
            view.SecurityType = SecurityType.SqlServerAuth;
            view.UserName = "tiest";
            view.Password = "rocks";

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
            var view = new StubbedView2(null);
            view.Instance = "soemthingelseelse";
            view.SecurityType = SecurityType.SqlServerAuth;
            view.Password = "rocks";

            var localServerPickerPage = new LocalServerPickerPage(view, null, null);

            // When
            var readyToMove = localServerPickerPage.ReadyToMove();

            // Then
            Assert.That(readyToMove, Is.False);
        }

        [Test]
        public void PassesPostValidationIfConnectionSucceeds()
        {
            // Given
            var postValidateCalled = false;
            var connection = new Connection("instance", SecurityType.Integrated, "tiestv", "rocks!");
            ConnectionStatus connectionStatus = null; //  new ConnectionStatus(true, "", new List<DbObjectName>(), "datapath");
            var localServerPickerPage = new LocalServerPickerPage(null, null, testConnection);
            Expect.Call(testConnection.Test(connection)).Return(connectionStatus);

            // When
            localServerPickerPage.PostValidate(() => { postValidateCalled = true; });

            //Then
            Assert.That(postValidateCalled, Is.True);
        }


    }

    class StubbedView2 : LocalServerPickerView
    {
        private readonly UserControl m_Control;
        public string Instance;
        public SecurityType SecurityType;
        public string Password;
        public string UserName;

        public StubbedView2(UserControl control)
        {
            m_Control = control;
        }

        public UserControl getControl()
        {
            return m_Control;
        }

        public string GetInstance()
        {
            return Instance;
        }

        public SecurityType GetSecurityType()
        {
            return SecurityType;
        }

        public string GetUserName()
        {
            return UserName;
        }
    }

    class StubbedPage2 : WizardPage
    {
        public StubbedPage2(WizardPage nextPage) : base(nextPage)
        {
        }

        public override UserControl GetControl()
        {
            throw new NotImplementedException();
        }

        public override void OnChangeDo(DowntoolsSvrExperiment.Utilities.Action onChangeAction)
        {
            throw new NotImplementedException();
        }

        public override bool ReadyToMove()
        {
            throw new NotImplementedException();
        }

        public override string getName()
        {
            throw new NotImplementedException();
        }

        public override void PostValidate(DowntoolsSvrExperiment.Utilities.Action andThen)
        {
            throw new NotImplementedException();
        }
    }
}
