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

namespace FunctionPattern.Chain4Action
{
    public abstract class ActionRecorder
    {
        protected ActionRecord CurrentBaseRecord { set; get; }

        protected abstract object RecorderIdCore { get; }

        protected abstract ActionRecord StartRecordCore();

        protected abstract void EndRecordCore();

        protected abstract void CutRecordCore();

        protected abstract ActionRecord GenerateEmptyRecordCore();

        public object RecorderId
        {
            get
            {
                return RecorderIdCore;
            }
        }

        public ActionRecord StartRecord()
        {
            CurrentBaseRecord = StartRecordCore();
            CurrentBaseRecord.MyRecorder = this;
            return CurrentBaseRecord;
        }

        public void EndRecord()
        {
            EndRecordCore();
            CurrentBaseRecord = null;
        }

        public void CutRecord()
        {
            CutRecordCore();
            CurrentBaseRecord = null;
        }

        public ActionRecord GenerateEmptyRecord()
        {
            return GenerateEmptyRecordCore();
        }
    }
}
