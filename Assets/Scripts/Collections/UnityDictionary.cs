using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MChojniak.Collections
{
    [Serializable]
    public class UnityDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializableCollections
    {
        [SerializeField] List<TKey> _serializedKeys = new();
        [SerializeField] List<TValue> _serializedValues = new();

        public UnityDictionary() : base() {}

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
                Add(key, _serializedValues[i]);

                i++;
            }
        }

    }
}