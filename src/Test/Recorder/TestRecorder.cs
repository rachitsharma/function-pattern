using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunctionPattern.Chain4Action;

namespace Test
{
    class TestRecorder : ActionRecorder
    {
        List<int> Numbers { set; get; }

        TestRecord CurrentRecord { set; get; }

        public TestRecorder(List<int> Numbers)
        {
            this.Numbers = Numbers;
        }

        protected override object RecorderIdCore
        {
            get { return ActorId.Generate<TestRecorder>(); }
        }

        protected override ActionRecord StartRecordCore()
        {
            CurrentRecord = new TestRecord(Numbers);
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
            return new TestRecord(Numbers);
        }

        public void Add(int i)
        {
            CurrentRecord.Add(i);
        }
    }
}
