﻿using System;
using UnityEngine;

namespace Core.InputSystem
{
    public enum EInputEvent
    {
        None = 0,
        KeyUp = 1,
        KeyHold = 2,
        KeyDown = 3,
        MouseButtonUp = 4,
        MouseButtonHold = 5,
        MouseButtonDown = 6,
        HorizontalAxis = 7,
        VerticalAxis = 8,
    }

    public abstract class InputListenerBase : MonoBehaviour
    {
        public abstract EInputEvent GetInputEventType();
    }
    
    public abstract class InputListenerBase<T> : InputListenerBase
        where T : InputData
    {
        #region Events

        public abstract Action<T> OnInputEventTriggered { get; set; }

        #endregion
    }
}