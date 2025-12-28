using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MChojniak.Collections
{
    [Serializable]
    public class UnityQueue<TValue> : Queue<TValue>, ISerializableQueue
    {
        [SerializeField] List<TValue> _serializedValues = new();

        public UnityQueue() : base() {}
        public UnityQueue(IEnumerable<TValue> collection) : base(collection) { OnBeforeSerialize(); }
        public UnityQueue(int capacity) : base(capacity) { OnBeforeSerialize(); }

        public void OnBeforeSerialize() 
        { 
            _serializedValues.Clear();

            foreach(var value in this.ToArray()) 
             _serializedValues.Add(value); 
        }

        public void OnAfterDeserialize() 
        {
            Clear();

            foreach(var value in _serializedValues)
                Enqueue(value);
        }

    }
}