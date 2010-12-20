using System.Windows.Forms;
using DowntoolsSvrExperiment.WizardControl;

namespace UnitTests
{
    public class StubbedPage : WizardPage
    {
        private readonly UserControl m_Control;
        private readonly WizardPage m_Next;
        private readonly string m_NextButtonText;
        private bool m_ReadyToMove = true;
        public DowntoolsSvrExperiment.Utilities.Action OnChangeAction;
        private string m_Name;
        public bool PostValidation = true;

        public StubbedPage(UserControl control, WizardPage next, string nextButtonText, string pageName) : base(null)
        {
            m_Control = control;
            m_Next = next;
            m_NextButtonText = nextButtonText;
            m_Name = pageName;
        }

        public override UserControl GetControl()
        {
            return m_Control;
        }

        public override void OnChangeDo(DowntoolsSvrExperiment.Utilities.Action onChangeAction)
        {
            OnChangeAction = onChangeAction;
        }

        public override bool ReadyToMove()
        {
            return m_ReadyToMove;
        }

        public void ReadyToMove(bool value)
        {
            m_ReadyToMove = value;
        }

        public override WizardPage GetNextPage()
        {
            return m_Next;
        }

        public override string GetNextButtonText()
        {
            return m_NextButtonText;
        }

        public override string getName()
        {
            return m_Name;
        }

        public override void PostValidate(DowntoolsSvrExperiment.Utilities.Action andThen)
        {
            if(PostValidation)
            {
                andThen();
            }
        }

        public void RaiseOnChangeDoAction()
        {
            OnChangeAction.Invoke();
        }
    }
}