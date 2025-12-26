using UnityEngine;

namespace MChojniak.Input.Abstractions
{
    public delegate void InputEventDelegate(object sender, InputEventArgs args);

    public delegate void InputEventDelegate<T>(object sender, InputEventArgs<T> args);
}