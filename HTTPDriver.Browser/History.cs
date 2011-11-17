using System;
using System.Collections.Generic;

namespace HTTPDriver.Browser
{
    public class History
    {
        private readonly LinkedList<string> _urls;
        private LinkedListNode<string> _currentUrl; 

        public History()
        {
            _urls = new LinkedList<string>();
        }

        protected internal string CurrentUrl()
        {
            return _currentUrl.Value;
        }

        protected internal void Add(string url)
        {
            _currentUrl = new LinkedListNode<string>(url);
            _urls.AddLast(_currentUrl);
        }

        protected internal void Back()
        {
            if (HasNoPreviousUrl())
                throw new NoPreviousUrl();

            _currentUrl = _currentUrl.Previous;
        }

        protected internal void Forward()
        {
            if (HasNoNextUrl())
                throw new NoNextUrl();

            _currentUrl = _currentUrl.Next;
        }

        protected internal void SetCurrent(string url)
        {
            _currentUrl = _urls.Find(url);
        }

        private bool HasNoPreviousUrl()
        {
            return _currentUrl == null || _currentUrl.Previous == null;
        }

        private bool HasNoNextUrl()
        {
            return _currentUrl == null || _currentUrl.Next == null;
        }

        public class NoPreviousUrl : Exception { }

        public class NoNextUrl : Exception { }
    }
}