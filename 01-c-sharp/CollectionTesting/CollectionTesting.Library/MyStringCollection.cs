using System;
using System.Collections.Generic;

namespace CollectionTesting.Library
{
    public class MyStringCollection : MyGenericCollection<string>
    {
        // use a private or protected field
        // to store a List<string> to handle all the list operations

        // now we are getting the field from the parent class
        //private readonly List<string> _list = new List<string>();

        // implement some collection methods like Add, Contains, Remove, and some
        // others that are not already on the List.

        // at least 5 methods

        /// <summary>
        /// Replace all contained strings with lowercased versions.
        /// </summary>
        public void MakeLowercase()
        {
            for (int i = 1; i < _list.Count; i++)
            {
                _list[i] = _list[i].ToLower();
            }
        }

        public void RemoveEmpty()
        {
            while (_list.Contains(""))
            {
                _list.Remove("");
            }
            // or...
            // using lambdas
            //_list.RemoveAll(x => x == "");
        }

        public bool Remove(string value)
        {
            return _list.Remove(value);
        }

        public string GetFirst()
        {
            if (_list.Count == 0)
            {
                throw new InvalidOperationException("list is empty");
            }
            return _list[0];
        }
    }
}
