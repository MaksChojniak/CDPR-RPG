using System;

namespace MChojniak.Input.Abstractions
{
    public class InputEventArgs : EventArgs
    {
        
    }

    public class InputEventArgs<T> : EventArgs
    {
        public T Value { get; set; }

        public InputEventArgs(T value)
        {
            Value = value;
        }
    }
}