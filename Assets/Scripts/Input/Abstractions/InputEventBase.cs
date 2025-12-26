using System;
using UnityEngine;

namespace MChojniak.Input.Abstractions
{
    public abstract class InputEventBase : ScriptableObject
    {
        public abstract void Start(object sender);
        public abstract void Cancel(object sender);
        public abstract void Perform(object sender, object arg);
    }
}