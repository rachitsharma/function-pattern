#region License
/*
Copyright (c) 2010 ShanGuanDa etc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunctionPattern.Chain4Action;

namespace Test
{
    class RecorderTester : IActionRecordable
    {
        TestRecorder m_recorder;

        List<int> Numbers { set; get; }


        public RecorderTester(List<int> Numbers)
        {
            this.Numbers = Numbers;
            m_recorder = new TestRecorder(this);
        }

        public void Add(int i)
        {
            m_recorder.Add(i);
        }

        #region IActionRecordable 成员

        public ActionRecorder GetActonRecorder()
        {
            return m_recorder;
        }

        #endregion

        #region nested class
        class TestRecorder : ActionRecorder
        {
            RecorderTester m_host;
            TestRecord CurrentRecord { set; get; }

            public TestRecorder(RecorderTester host)
            {
                m_host = host;
            }

            protected override object RecorderIdCore
            {
                get { return typeof(TestRecorder); }
            }

            protected override ActionRecord StartRecordCore()
            {
                CurrentRecord = new TestRecord(m_host.Numbers);
                return CurrentRecord;
            }

            protected override void EndRecordCore()
            {
                CurrentRecord.End();
                CurrentRecord = null;
            }

            protected override void CutRecordCore()
            {
                CurrentRecord = null;
            }

            protected override ActionRecord GenerateEmptyRecordCore()
            {
                return new TestRecord(m_host.Numbers);
            }

            public void Add(int i)
            {
                CurrentRecord.Add(i);
            }
        }

        #endregion
    }
}
