﻿using System;
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
            m_View = new StubbedView(); // m_Mocks.PartialMock<StubbedView>();
            
            m_LastPageControl = m_Mocks.Stub<UserControl>();
            m_LastPage = new StubbedPage(m_LastPageControl, null, "Finish", "Last Page"); // m_Mocks.PartialMock<StubbedPage>(m_LastPageControl, null, "Finish");

            m_FirstPageControl = m_Mocks.Stub<UserControl>();
            m_FirstPage = new StubbedPage(m_FirstPageControl, m_LastPage, "Next", "First Page"); // m_Mocks.PartialMock<StubbedPage>(m_FirstPageControl, m_LastPage, "Next");
        }

        [Test]
        public void ShouldRegisterListenersOnWizardControl()
        {
            Given();
            Action cancelAction = () => { };

            When();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, cancelAction);

            Then();
            Assert.That(m_View.CancelButtonAction, Is.EqualTo(cancelAction));
            Assert.That(m_View.NextButtonAction, Is.EqualTo(new Action(wizardViewModel.MoveToNextPage)));
            Assert.That(m_View.PreviousButtonAction, Is.EqualTo(new Action(wizardViewModel.MoveToPreviousPage)));
        }

        [Test]
        public void ShouldMoveToFirstPageAndSetControl()
        {
            Given();

            When();
            new WizardViewModel(m_View, m_FirstPage, null, null);

            Then();
            Assert.That(m_View.PageControl, Is.EqualTo(m_FirstPageControl));
            Assert.That(m_View.PageName, Is.EqualTo("Step 1 of 2: First Page"));
        }

        [Test]
        public void ShouldMoveToFirstPageAndSetButtonState()
        {
            Given();
            m_FirstPage.ReadyToMove(true);

            When();
            new WizardViewModel(m_View, m_FirstPage, null, null);

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
            m_FirstPage.ReadyToMove(false);

            When();
            new WizardViewModel(m_View, m_FirstPage, null, null);

            Then();
            Assert.That(m_View.NextButton, Is.False);
        }

        [Test]
        public void ShouldMoveToSecondPageAndSetControl()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);

            When();
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(m_View.PageControl, Is.EqualTo(m_LastPageControl));
            Assert.That(m_View.PageName, Is.EqualTo("Step 2 of 2: Last Page"));
        }

        [Test]
        public void ShouldMoveToSecondPageAndSetBackToEnabledAndNextButtonToFinishedWhenReadyToProceed()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);
            m_LastPage.ReadyToMove(true);

            When();
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
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);
            m_LastPage.ReadyToMove(false);

            When();
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(m_View.NextButton, Is.False);
            Assert.That(m_View.NextButtonName, Is.EqualTo("Finish"));
        }

        [Test]
        public void ShouldMoveBackFromSecondToFirstPage()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);

            When();
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
        public void ShouldInformClientWhenFinished()
        {
            Given();
            Boolean clientWasCalled = false;
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, () => { clientWasCalled = true; }, null);

            When();
            wizardViewModel.MoveToNextPage();
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(clientWasCalled, Is.True);
        }

        [Test]
        public void ShouldNotAllowMovingBeforeFirstPage()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);

            When();
            try
            {
                wizardViewModel.MoveToPreviousPage();
                Assert.Fail("Should not get here");
            } catch(MoveBeforeFirstPageException e)
            {
                Then();
            }
        }

        [Test]
        public void ShouldNotAllowMovingIfNotReadyToProceed()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);
            m_FirstPage.ReadyToMove(false);

            When();
            try
            {
                wizardViewModel.MoveToNextPage();
                Assert.Fail("Should not get here");
            }
            catch (NotReadyToProceedException e)
            {
                Then();
            }
        }

        [Test]
        public void ShouldRegisterForPageChangesAndUpdateStateOfNextButtonWhenPageChanges()
        {
            Given();
            m_FirstPage.ReadyToMove(false);
            new WizardViewModel(m_View, m_FirstPage, null, null);
            Assert.That(m_View.NextButton, Is.False);

            When();
            m_FirstPage.ReadyToMove(true);
            m_FirstPage.RaiseOnChangeDoAction();

            Then();
            Assert.That(m_View.NextButton, Is.True);
        }

        [Test]
        public void ShouldUpdateStateOfFinishButtonWhenFinalPageChanges()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);

            m_LastPage.ReadyToMove(false);
            wizardViewModel.MoveToNextPage();

            Assert.That(m_View.NextButton, Is.False);

            When();
            m_LastPage.ReadyToMove(true);
            m_LastPage.RaiseOnChangeDoAction();

            Then();
            Assert.That(m_View.NextButton, Is.True);
        }

        [Test]
        public void WhenOnFinalPageAndFirstPageChangesThenFinishButtonShouldNotChange()
        {
            Given();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);

            m_LastPage.ReadyToMove(false);
            wizardViewModel.MoveToNextPage();

            Assert.That(m_View.NextButton, Is.False);

            When();
            m_FirstPage.ReadyToMove(true);
            m_FirstPage.RaiseOnChangeDoAction();

            Then();
            Assert.That(m_View.NextButton, Is.False);
        }

        [Test]
        public void ShouldUpdateViewWithPageList()
        {
            Given();

            When();
            new WizardViewModel(m_View, m_FirstPage, null, null);

            Then();
            Assert.That(m_View.PageList[0].Name, Is.EqualTo("First Page"));
            Assert.That(m_View.PageList[0].CurrentPage, Is.True);
            Assert.That(m_View.PageList[1].Name, Is.EqualTo("Last Page"));
            Assert.That(m_View.PageList[1].CurrentPage, Is.False);
        }

        [Test]
        public void ShouldHighlightSecondPageInListAfterMoving()
        {
            Given();

            When();
            var wizardViewModel = new WizardViewModel(m_View, m_FirstPage, null, null);
            wizardViewModel.MoveToNextPage();

            Then();
            Assert.That(m_View.PageList[0].CurrentPage, Is.False);
            Assert.That(m_View.PageList[1].CurrentPage, Is.True);
        }
    }

    public class StubbedView : WizardView
    {
        public UserControl PageControl;
        public string PageName;
        public bool BackButton;
        public bool NextButton;
        public string NextButtonName;
        public bool CancelButton;
        public List<PageNameAndCurrent> PageList;
        public Action CancelButtonAction;
        public Action NextButtonAction;
        public Action PreviousButtonAction;

        public virtual void SetPage(UserControl pageControl, string pageName)
        {
            PageControl = pageControl;
            PageName = pageName;
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

        public void SetPageList(List<PageNameAndCurrent> pages)
        {
            PageList = pages;
        }

        public void OnCancelDo(Action cancelAction)
        {
            CancelButtonAction = cancelAction;
        }

        public void OnNextDo(Action nextButtonAction)
        {
            NextButtonAction = nextButtonAction;
        }

        public void OnPreviousDo(Action previousButtonAction)
        {
            PreviousButtonAction = previousButtonAction;
        }
    }

    public class StubbedPage : WizardPage
    {
        private readonly UserControl m_Control;
        private readonly WizardPage m_Next;
        private readonly string m_NextButtonText;
        private bool m_ReadyToMove = true;
        public Action OnChangeAction;
        private string m_Name;

        public StubbedPage(UserControl control, WizardPage next, string nextButtonText, string pageName)
        {
            m_Control = control;
            m_Next = next;
            m_NextButtonText = nextButtonText;
            m_Name = pageName;
        }

        public virtual UserControl GetControl()
        {
            return m_Control;
        }

        public void OnChangeDo(Action onChangeAction)
        {
            OnChangeAction = onChangeAction;
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

        public string getName()
        {
            return m_Name;
        }

        public void RaiseOnChangeDoAction()
        {
            OnChangeAction.Invoke();
        }
    }
}
