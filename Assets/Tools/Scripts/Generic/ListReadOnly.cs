using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public class ListReadOnly<T>
    {
        public T this[int i]
        {
            get { return _internalList[i]; }
        }

        public int Count
        {
            get { return _internalList.Count; }
        }

        [SerializeField] private List<T> _internalList = new List<T>();

        public ListReadOnly(List<T> internalList)
        {
            _internalList = internalList;
        }
    }
}
