using MChojniak.Collections;
using MChojniak.Input.Abstractions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MChojniak.Input
{
    public class InputManager : MonoBehaviour
    {
        public UnityDictionary<string, InputEventBase> Events;

        InputSystem_Actions _inputSystem;
        Dictionary<string, InputAction> _inputActions;

        void Awake()
        {
            Events = Events ?? new();

            _inputSystem = new();

            _inputActions = new();
            foreach(var k in Events.Keys)
                _inputActions[k] = _inputSystem.FindAction(k);

            foreach(var k in Events.Keys)
            {
                _inputActions[k].started += _ => Events[k].Start(this);
                _inputActions[k].canceled += _ => Events[k].Cancel(this);
                _inputActions[k].performed += callback => Events[k].Perform(this, callback.ReadValueAsObject());
            }
        }

        void OnEnable()
        {
            _inputSystem.Enable();
        }

        void OnDisable()
        {
            
            _inputSystem.Disable();
        }

    }
}