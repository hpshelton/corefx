// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Linq.Tests
{
    /// <summary>
    /// Test enumerator - returns int values from 1 to 5 inclusive.
    /// </summary>
    class TestEnumerator : IEnumerable<int>, IEnumerator<int>
    {
        private int _current = 0;

        public virtual int Current { get { return _current; } }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose() { }

        public virtual IEnumerator<int> GetEnumerator()
        {
            return this;
        }

        public virtual bool MoveNext()
        {
            return _current++ < 5;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Test enumerator - throws InvalidOperationException from Current after MoveNext called once.
    /// </summary>
    class ThrowsOnCurrentEnumerator : TestEnumerator
    {
        public override int Current
        {
            get
            {
                var current = base.Current;
                if (current == 1)
                    throw new InvalidOperationException();
                return current;
            }
        }
    }

    /// <summary>
    /// Test enumerator - throws InvalidOperationException on first call to MoveNext.
    /// </summary>
    class ThrowsOnMoveNextEnumerator : TestEnumerator
    {
        public override bool MoveNext()
        {
            bool baseReturn = base.MoveNext();
            if (base.Current == 1)
                throw new InvalidOperationException();

            return baseReturn;
        }
    }

    /// <summary>
    /// Test enumerator - throws InvalidOperationException from GetEnumerator when called for the first time.
    /// </summary>
    class ThrowsOnGetEnumerator : TestEnumerator
    {
        private int getEnumeratorCallCount;
        public override IEnumerator<int> GetEnumerator()
        {
            if (getEnumeratorCallCount++ == 0)
                throw new InvalidOperationException();

            return base.GetEnumerator();
        }
    }
}
