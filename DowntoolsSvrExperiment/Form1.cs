using System;
using System.Windows.Forms;
using DowntoolsSvrExperiment.Connection;
using DowntoolsSvrExperiment.Utilities;
using DowntoolsSvrExperiment.VRPages.IntroPage;
using DowntoolsSvrExperiment.VRPages.LocalServerPicker;
using DowntoolsSvrExperiment.WizardControl;

namespace DowntoolsSvrExperiment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var wizardView = new WizardViewWidget();
            var secondPage = new PageExample(null, "second page");
            var firstPage = new PageExample(secondPage, "first page");
            var localInstancePage = new LocalServerPickerPage(new LocalServerPickerWidget(), firstPage,
                                                              new TestConnection(), new GetLocalInstances());
            var introPage = new IntroPageViewModel(new IntroPageWidget(), localInstancePage);
            new WizardViewModel(wizardView, introPage, cancel, cancel);

            Controls.Add(wizardView);
        }

        private void cancel()
        {
            this.Close();
        }
    }

    public class PageExample : WizardPage
    {
        private readonly string m_PageName;

        public PageExample(WizardPage nextPage, string pageName) : base(nextPage)
        {
            m_PageName = pageName;
        }

        public override UserControl GetControl()
        {
            return new ExamplePageUserWidget();
        }

        public override void OnChangeDo(Action onChangeAction)
        {
        }

        public override bool ReadyToMove()
        {
            return true;
        }

        public override string GetNextButtonText()
        {
            return "Next";
        }

        public override string getName()
        {
            return m_PageName;
        }

        public override void PostValidate(Action andThen)
        {
            throw new NotImplementedException();
        }
    }


}
