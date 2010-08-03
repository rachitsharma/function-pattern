using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunctionPattern.Chain4Action;

namespace Test
{
    class TestRecord : ActionRecord
    {
        public List<int> Numbers { set; get; }

        public List<int> Current { set; get; }

        public TestRecord(List<int> lst)
        {
            Numbers = lst;
            Current = new List<int>();
        }

        protected override void ToEmptyCore()
        {
            IsEmpty = true;
        }

        protected override void RedoCore()
        {
            Numbers.AddRange(Current);
        }

        protected override void UndoCore()
        {
            Numbers.RemoveRange(Numbers.Count - Current.Count, Current.Count);
        }

        public void Add(int i)
        {
            Current.Add(i);
            IsEmpty = false;
        }

        public void End()
        {
            Numbers.AddRange(Current);
        }
    }
}
