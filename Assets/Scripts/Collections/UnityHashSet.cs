using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MChojniak.Collections
{
    [Serializable]
    public class UnityHashSet<TValue> : HashSet<TValue>, ISerializableHashSet
    {
        [SerializeField] List<TValue> _serializedValues = new();

        public UnityHashSet() : base() {}

        public void OnBeforeSerialize() 
        { 
            _serializedValues.Clear();

            foreach(var value in this) 
             _serializedValues.Add(value); 
        }

        public void OnAfterDeserialize() 
        {
            Clear();

            foreach(var value in _serializedValues)
                Add(value);
        }

    }
}