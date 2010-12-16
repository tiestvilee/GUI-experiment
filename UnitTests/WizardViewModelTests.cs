using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DowntoolsSvrExperiment.WizardControl;
using NUnit.Framework;
using Rhino.Mocks;

namespace UnitTests
{
    [TestFixture]
    public class WizardViewModelTests : MockingBase
    {
        private StubbedView m_View;
        private StubbedPage m_FirstPage;
        private UserControl m_FirstPageControl;
        private StubbedPage m_LastPage;
        private UserControl m_LastPageControl;

        [SetUp]
        public void SetUp()
        {
            m_View = m_Mocks.PartialMock<StubbedView>();
            
            m_LastPageControl = m_Mocks.Stub<UserControl>();
            m_LastPage = m_Mocks.PartialMock<StubbedPage>(m_LastPageControl, null, "Finish");

            m_FirstPageControl = m_Mocks.Stub<UserControl>();
            m_FirstPage = m_Mocks.PartialMock<StubbedPage>(m_FirstPageControl, m_LastPage, "Next");
        }

        [Test]
        public void ShouldMoveToFirstPageAndSetControl()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage);

            When();
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(m_View.PageControl, Is.EqualTo(m_FirstPageControl));
        }

        [Test]
        public void ShouldMoveToFirstPageAndSetButtonState()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage);
            m_FirstPage.ReadyToMove(true);

            When();
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(m_View.BackButton, Is.False);
            Assert.That(m_View.NextButton, Is.True);
            Assert.That(m_View.NextButtonName, Is.EqualTo("Next"));
            Assert.That(m_View.CancelButton, Is.True);
        }

        [Test]
        public void ShouldMoveToFirstPageAndSetNextButtonToDisabledWhenNotReadyToProceed()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage);
            m_FirstPage.ReadyToMove(false);

            When();
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(m_View.NextButton, Is.False);
        }

        [Test]
        public void ShouldMoveToSecondPageAndSetBackToEnabledAndNextButtonToFinishedWhenReadyToProceed()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage);
            m_LastPage.ReadyToMove(true);

            When();
            wizardViewModel.MoveToNextPage();
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(m_View.PageControl, Is.EqualTo(m_LastPageControl));

            Assert.That(m_View.BackButton, Is.True);
            Assert.That(m_View.NextButton, Is.True);
            Assert.That(m_View.NextButtonName, Is.EqualTo("Finish"));
            Assert.That(m_View.CancelButton, Is.True);
        }

        [Test]
        public void ShouldMoveToSecondPageAndSetFinishButtonToDisabledWhenNotReadyToProceed()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage);
            m_LastPage.ReadyToMove(false);

            When();
            wizardViewModel.MoveToNextPage();
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(m_View.NextButton, Is.False);
            Assert.That(m_View.NextButtonName, Is.EqualTo("Finish"));
        }

        [Test]
        public void ShouldMoveBackFromSecondToFirstPage()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage);

            When();
            wizardViewModel.MoveToNextPage();
            wizardViewModel.MoveToNextPage();
            wizardViewModel.MoveToPreviousPage();

            Then();
            Assert.That(m_View.PageControl, Is.EqualTo(m_FirstPageControl));

            Assert.That(m_View.BackButton, Is.False);
            Assert.That(m_View.NextButton, Is.True);
            Assert.That(m_View.NextButtonName, Is.EqualTo("Next"));
            Assert.That(m_View.CancelButton, Is.True);
        }

        [Test]
        public void ShouldSetupListeningForChangesOnPage()
        {
            Given();
            //Expect.Call()

            When();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage);

            Then();
            m_FirstPage.AssertWasCalled(x => x.AddChangeListener(wizardViewModel));
        }

        [Test]
        public void ShouldUpdateNextButtonStateWhenPageChangesState()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage);

            When();
            wizardViewModel.Notify(m_FirstPage);

            Then();
        }
    }

    public class StubbedView : IWizardView
    {
        public UserControl PageControl;
        public bool BackButton;
        public bool NextButton;
        public string NextButtonName;
        public bool CancelButton;

        public virtual void SetPage(UserControl pageControl)
        {
            PageControl = pageControl;
        }

        public virtual void EnableBackButton(bool b)
        {
            BackButton = b;
        }

        public virtual void EnableNextButton(bool b)
        {
            NextButton = b;
        }

        public void SetNextButtonName(string name)
        {
            NextButtonName = name;
        }

        public virtual void EnableCancelButton(bool b)
        {
            CancelButton = b;
        }
    }

    public class StubbedPage : WizardPage
    {
        private readonly UserControl m_Control;
        private readonly WizardPage m_Next;
        private readonly string m_NextButtonText;
        List<ChangeListener> listeners = new List<ChangeListener>();
        private bool m_ReadyToMove = true;

        public StubbedPage(UserControl control, WizardPage next, string nextButtonText)
        {
            m_Control = control;
            m_Next = next;
            m_NextButtonText = nextButtonText;
        }

        public virtual UserControl GetControl()
        {
            return m_Control;
        }

        public virtual void AddChangeListener(ChangeListener changeListener)
        {
            listeners.Add(changeListener);
        }

        public virtual void FireChangeEvent()
        {
            throw new NotImplementedException();
        }

        public virtual bool ReadyToMove()
        {
            return m_ReadyToMove;
        }

        public void ReadyToMove(bool value)
        {
            m_ReadyToMove = value;
        }

        public WizardPage GetNextPage()
        {
            return m_Next;
        }

        public string GetNextButtonText()
        {
            return m_NextButtonText;
        }
    }
}
