using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MChojniak.Collections
{
    [Serializable]
    public class UnityStack<TValue> : Stack<TValue>, ISerializableStack
    {
        [SerializeField] List<TValue> _serializedValues = new();

        public UnityStack() : base() {}

        public void OnBeforeSerialize() 
        { 
            _serializedValues.Clear();

            foreach(var value in this.ToArray().Reverse()) 
                _serializedValues.Add(value); 
        }

        public void OnAfterDeserialize() 
        {
            Clear();

            foreach(var value in _serializedValues)
                Push(value);
        }

    }
}