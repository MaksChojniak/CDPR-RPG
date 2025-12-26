using System;
using System.Collections;
using System.IO;
using MChojniak.Input.Abstractions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace MChojniak.Input.Tests
{
    public class InputEvent_Test
    {
        string _basePath => Path.Combine("Assets", "Scripts", "Input", "Tests", "Temp");

        InputEvent _inputEventMock;
        string _inputEventMock_Path => Path.Combine(_basePath, $"InputEvent_Mock.asset");

        [OneTimeSetUp]
        public void Init()
        {            
            _inputEventMock = AssetDatabase.LoadAssetAtPath<InputEvent_Mock>(_inputEventMock_Path);
        }     
        

        [Test]
        public void Event_Started_Passes()
        {
            InputEventBase inputEventBase = _inputEventMock;

            bool eventRaised = false;
            _inputEventMock.Started += (sender, args) => eventRaised = true;

            inputEventBase.Start(inputEventBase); 

            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void Event_Canceled_Passes()
        {
            InputEventBase inputEventBase = _inputEventMock;

            bool eventRaised = false;
            _inputEventMock.Canceled += (sender, args) => eventRaised = true;

            inputEventBase.Cancel(inputEventBase); 

            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void Event_Performed_Passes()
        {
            InputEventBase inputEventBase = _inputEventMock;

            bool eventRaised = false;
            _inputEventMock.Performed += (sender, args) => {
                eventRaised = true;
            };

            inputEventBase.Perform(inputEventBase, null); 

            Assert.IsTrue(eventRaised);
        }
    }
}
