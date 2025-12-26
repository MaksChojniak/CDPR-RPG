using UnityEngine;

namespace MChojniak.Input.Abstractions
{
    public abstract class InputEvent : InputEventBase
    {
        public event InputEventDelegate Started;
        public event InputEventDelegate Canceled;
        public event InputEventDelegate Performed;

        public override void Start(object sender) => Started?.Invoke(sender, new InputEventArgs());
        public override void Cancel(object sender) => Canceled?.Invoke(sender, new InputEventArgs());
        public override void Perform(object sender, object _) => Performed?.Invoke(sender, new InputEventArgs());
    }

    public abstract class InputEvent<T> : InputEventBase
    {
        public event InputEventDelegate Started;
        public event InputEventDelegate Canceled;
        public event InputEventDelegate<T> Performed;

        public override void Start(object sender) => Started?.Invoke(sender, new InputEventArgs());
        public override void Cancel(object sender) => Canceled?.Invoke(sender, new InputEventArgs());
        public override void Perform(object sender, object arg) => Performed?.Invoke(sender, new InputEventArgs<T>((T)arg));
    }
}