using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;

namespace UnitTests
{
    public class MockingBase
    {
        public MockRepository m_Mocks = new MockRepository();

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

    }
}
