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
    public class WizardViewModelTests
    {
        MockRepository m_Mocks;
        private IWizardView m_View;
        private WizardPage m_FirstPage;
        private UserControl m_FirstPageControl;

        [SetUp]
        public void SetUp()
        {
            m_Mocks = new MockRepository();

            m_View = m_Mocks.PartialMock<StubbedView>();
            m_FirstPageControl = m_Mocks.Stub<UserControl>();
            m_FirstPage = m_Mocks.PartialMock<StubbedPage>(m_FirstPageControl);
        }

        public void Given()
        {
            // nothing
        }

        public void When()
        {
            m_Mocks.ReplayAll();
        }

        public void Then()
        {
            m_Mocks.VerifyAll();
        }

        [Test]
        public void ShouldSetupViewWithFirstPageControl()
        {
            Given();
            Expect.Call(() => m_View.SetPage(m_FirstPageControl));

            When();
            new WizardViewModel(m_View, m_FirstPage);

            Then();
        }

        [Test]
        public void ShouldSetupButtonStateForFirstPage()
        {
            Given();
            Expect.Call(() => m_View.EnableBackButton(false));
            Expect.Call(() => m_View.EnableNextButton(false));
            Expect.Call(() => m_View.EnableFinishButton(false));
            Expect.Call(() => m_View.EnableCancelButton(true));

            When();
            new WizardViewModel(m_View, m_FirstPage);

            Then();
        }

        [Test]
        public void ShouldSetupNextButtonIfPageReadyToMove()
        {
            Given();
            Expect.Call(m_FirstPage.ReadyToMove()).Return(true);
            Expect.Call(() => m_View.EnableNextButton(true));

            When();
            new WizardViewModel(m_View, m_FirstPage);

            Then();
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
        public virtual void SetPage(UserControl pageControl)
        { 
            // do nothing
        }

        public virtual void EnableBackButton(bool b)
        {
            // do nothing
        }

        public virtual void EnableNextButton(bool b)
        {
            // do nothing
        }

        public virtual void EnableFinishButton(bool b)
        {
            // do nothing
        }

        public virtual void EnableCancelButton(bool b)
        {
            // do nothing
        }
    }

    public class StubbedPage : WizardPage
    {
        private readonly UserControl m_Control;
        List<ChangeListener> listeners = new List<ChangeListener>();

        public StubbedPage(UserControl control)
        {
            m_Control = control;
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
            return false;
        }
    }
}
