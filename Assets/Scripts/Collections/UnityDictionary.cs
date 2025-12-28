using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MChojniak.Collections
{
    [Serializable]
    public class UnityDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializableDictionary
    {
        [SerializeField] List<TKey> _serializedKeys = new();
        [SerializeField] List<TValue> _serializedValues = new();

        public UnityDictionary() : base() {}
        public UnityDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { OnBeforeSerialize(); }
        public UnityDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { OnBeforeSerialize(); }
        public UnityDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { OnBeforeSerialize(); }
        public UnityDictionary(int capacity) : base(capacity) { OnBeforeSerialize(); }
        public UnityDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { OnBeforeSerialize(); }
        public UnityDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer) { OnBeforeSerialize(); }
        public UnityDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { OnBeforeSerialize(); }

        public void OnBeforeSerialize() 
        {
            _serializedKeys.Clear(); 
            _serializedValues.Clear();

            foreach(var key in Keys) 
                _serializedKeys.Add(key); 

            foreach(var value in Values) 
                _serializedValues.Add(value); 
        }

        public void OnAfterDeserialize() 
        {
            Clear();

            int i = 0;
            foreach(var key in _serializedKeys)
            {
                TryAdd(key, _serializedValues[i]);

                i++;
            }
        }
        
    }
}