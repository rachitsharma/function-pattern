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
    public class ChainManager
    {
        protected ActionRecorderCollection RecorderCollection { private set; get; }
        protected MultiActionRecordCollection RecordCollection { private set; get; }

        protected MultiActionRecord CurrentRecord { private set; get; }
        protected int m_currentIndex { private set; get; }
        protected int m_headIndex { private set; get; }
        protected int m_tailIndex { private set; get; }

        public ActStatus ActStatus { private set; get; }

        public bool DontRecordWhenStatusError { private set; get; }

        public bool CanUndo
        {
            get
            {
                return m_currentIndex > m_headIndex;
            }
        }

        public bool CanRedo
        {
            get
            {
                return m_tailIndex > m_currentIndex;
            }
        }

        public bool CanRollback
        {
            get
            {
                return CanUndo;
            }
        }

        #region Constructor
        public ChainManager(IniParams iniParams)
        {
            RecorderCollection = new ActionRecorderCollection(iniParams.RecordCollection.GetActionRecorderCollection());
            RecordCollection = new MultiActionRecordCollection();
            DontRecordWhenStatusError = iniParams.DontRecordWhenStatusError;
        }
        #endregion

        #region Private Methods
        private void RemoveExcrescentRecords()
        {
            if (RecordCollection.Count > m_currentIndex)
            {
                for (int i = RecordCollection.Count - 1; i >= m_currentIndex; i--)
                {
                    RecordCollection.RemoveAt(i);
                }
            }
        }
        #endregion

        public void StartRecord()
        {
            if (ActStatus != ActStatus.None)
            {
                if (DontRecordWhenStatusError) return;
                throw new InvalidOperationException();
            }

            if (CurrentRecord != null) throw new InvalidOperationException();

            ActStatus = ActStatus.Acting;
            CurrentRecord = new MultiActionRecord();

            RecorderCollection.Do(recorder => CurrentRecord.AddActionRecord(recorder.StartRecord()));
        }

        public void EndRecord()
        {
            if (ActStatus != ActStatus.Acting)
            {
                if (DontRecordWhenStatusError) return;
                throw new InvalidOperationException();
            }

            if (CurrentRecord == null) throw new InvalidOperationException();

            RemoveExcrescentRecords();

            RecorderCollection.Do(recorder => recorder.EndRecord());

            RecordCollection.Add(CurrentRecord);

            m_currentIndex++;
            m_tailIndex = m_currentIndex;
            CurrentRecord = null;

            ActStatus = ActStatus.None;
        }

        public void CutRecord()
        {
            if (ActStatus != ActStatus.Acting)
            {
                if (DontRecordWhenStatusError) return;
                throw new InvalidOperationException();
            }

            if (CurrentRecord == null) throw new InvalidOperationException();

            RecorderCollection.Do(recorder => recorder.CutRecord());

            CurrentRecord = null;

            ActStatus = ActStatus.None;
        }

        public void Undo()
        {
            if (ActStatus != ActStatus.None)
            {
                if (DontRecordWhenStatusError) return;
                throw new InvalidOperationException();
            }

            if (CanUndo == false) throw new InvalidOperationException();

            ActStatus = ActStatus.Undoing;
            m_currentIndex--;
            var record = RecordCollection[m_currentIndex];
            record.Undo();
            ActStatus = ActStatus.None;
        }

        public void Redo()
        {
            if (ActStatus != ActStatus.None)
            {
                if (DontRecordWhenStatusError) return;
                throw new InvalidOperationException();
            }

            if (CanRedo == false) throw new InvalidOperationException();

            ActStatus = ActStatus.Redoing;
            var record = RecordCollection[m_currentIndex];
            record.Redo();
            m_currentIndex++;

            ActStatus = ActStatus.None;
        }

        public void Rollback()
        {
            if (ActStatus != ActStatus.None)
            {
                if (DontRecordWhenStatusError) return;
                throw new InvalidOperationException();
            }

            if (CanRollback == false) throw new InvalidOperationException();

            ActStatus = ActStatus.Rollbacking;

            m_currentIndex--;
            var record = RecordCollection[m_currentIndex];
            record.Undo();

            m_tailIndex = m_currentIndex;
            RemoveExcrescentRecords();
            ActStatus = ActStatus.None;
        }

        #region Nested Class
        public class ActionRecordableCollection : Collection<IActionRecordable>
        {
            internal Collection<ActionRecorder> GetActionRecorderCollection()
            {
                ActionRecorderCollection collection = new ActionRecorderCollection();
                this.Do(item => collection.Add(item.GetActonRecorder()));
                return collection;
            }
        }

        protected class MultiActionRecordCollection : Collection<MultiActionRecord>
        {
        }

        protected class ActionRecorderCollection : Collection<ActionRecorder>
        {
            public ActionRecorderCollection()
            {
            }

            public ActionRecorderCollection(IList<ActionRecorder> list)
                : base(list)
            {
            }
        }

        public sealed class IniParams
        {
            public ActionRecordableCollection RecordCollection { private set; get; }

            public bool DontRecordWhenStatusError { set; get; }

            public IniParams()
            {
                RecordCollection = new ActionRecordableCollection();
            }
        }
        #endregion
    }
}
