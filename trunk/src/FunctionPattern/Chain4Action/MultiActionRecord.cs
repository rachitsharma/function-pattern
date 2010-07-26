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
using System.Collections.ObjectModel;

namespace FunctionPattern.Chain4Action
{
    public class MultiActionRecord : ActionRecord
    {
        static readonly MultiActionRecorder myRecorder = new MultiActionRecorder();
        ActionRecordCollection m_recordCollection = new ActionRecordCollection();

        public MultiActionRecord()
            : base(myRecorder)
        {
        }

        ActionRecord FindRecord(ActorId actorId)
        {
            return m_recordCollection.Single(record => record.ActorId.Match(actorId));
        }

        protected override bool IsEmptyCore
        {
            get
            {
                return m_recordCollection.All(record => record.IsEmpty);
            }
        }

        protected override void ToEmptyCore()
        {
            m_recordCollection.Do(record => record.ToEmpty());
        }

        protected override void RedoCore()
        {
            m_recordCollection.Do(record => record.Redo());
        }

        protected override void UndoCore()
        {
            m_recordCollection.Do(record => record.Undo());
        }

        public bool HasRecord(ActorId actorId)
        {
            return m_recordCollection.Any(record => record.ActorId.Match(actorId));
        }

        public void ToEmpty(ActorId actorId)
        {
            FindRecord(actorId).ToEmpty();
        }

        public void Undo(ActorId actorId)
        {
            FindRecord(actorId).Undo();
        }

        public void Redo(ActorId actorId)
        {
            FindRecord(actorId).Redo();
        }

        public void AddActionRecord(ActionRecord record)
        {
            m_recordCollection.Add(record);
        }

        #region Nested Class
        class ActionRecordCollection : Collection<ActionRecord> { }

        class MultiActionRecorder : ActionRecorder
        {
            protected override ActorId ActorIdCore
            {
                get { return ActorId.Generate<MultiActionRecorder>(); }
            }

            protected override ActionRecord StartRecordCore()
            {
                throw new NotImplementedException();
            }

            protected override void EndRecordCore()
            {
                throw new NotImplementedException();
            }

            protected override ActionRecord GenerateEmptyRecordCore()
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
