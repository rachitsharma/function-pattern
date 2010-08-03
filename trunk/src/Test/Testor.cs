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
    class Testor
    {
        public void TestActorId()
        {
            var objId1 = ActorId.Generate<object>();
            var objId2 = ActorId.Generate<object>();

            if (objId1.Equals(objId2) == false || objId2.Equals(objId1) == false)
            {
                throw new NotImplementedException();
            }

            var intId1 = ActorId.Generate<int>();
        }

        public void TestChainManager()
        {
            List<int> lst = new List<int>();

            TestRecorder trr = new TestRecorder(lst);
            ChainManager.IniParams p = new ChainManager.IniParams();
            p.RecordCollection.Add(trr);
            ChainManager cm = new ChainManager(p);

            // Start End Test
            cm.StartRecord();

            trr.Add(1);
            trr.Add(2);

            cm.EndRecord();

            List<int> result1 = new List<int> { 1, 2 };

            if (result1.SequenceEqual(lst) == false)
            {
                throw new NotImplementedException();
            }

            cm.StartRecord();

            trr.Add(3);
            trr.Add(4);

            cm.EndRecord();

            List<int> result2 = new List<int> { 1, 2, 3, 4 };

            if (result2.SequenceEqual(lst) == false)
            {
                throw new NotImplementedException();
            }

            // Undo Test
            cm.Undo();

            if (result1.SequenceEqual(lst) == false)
            {
                throw new NotImplementedException();
            }

            // Redo Test
            cm.Redo();

            if (result2.SequenceEqual(lst) == false)
            {
                throw new NotImplementedException();
            }

            // Cut Test
            cm.StartRecord();

            trr.Add(3);
            trr.Add(4);

            cm.CutRecord();

            if (result2.SequenceEqual(lst) == false)
            {
                throw new NotImplementedException();
            }

            // Rollback Test
            cm.Rollback();

            if (result1.SequenceEqual(lst) == false)
            {
                throw new NotImplementedException();
            }
        }
    }
}
