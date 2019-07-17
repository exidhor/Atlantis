using System;
using System.Collections.Generic;

namespace MemoryManagement
{
    public class PoolStack<T>
    {
        Stack<T> _stack;

        int _expandSize;
        Func<T> _constructor;

        public PoolStack(Func<T> constructor, int initSize = 10, int expandSize = 5)
        {
            _constructor = constructor;

            _stack  = new Stack<T>(initSize);
            _expandSize = expandSize;

            Expand(initSize);
        }

        void Expand(int countToAdd)
        {
            for (int i = 0; i < countToAdd; i++)
            {
                _stack.Push(_constructor());
            }
        }

        public T Get()
        {
            if(_stack.Count == 0)
            {
                Expand(_expandSize);
            }

            return _stack.Pop();
        }

        public void Push(T e)
        {
            _stack.Push(e);
        }
    }
}
