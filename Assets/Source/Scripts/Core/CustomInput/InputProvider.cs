using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BugStrategy.CustomInput
{
    public class InputProvider : ITickable, IInputProvider
    {
        public bool LmbDown { get; private set; }
        public bool LmbHold { get; private set; }
        public bool LmbUp { get; private set; }
        
        public bool RmbDown { get; private set; }
        public bool RmbHold { get; private set; }
        public bool RmbUp { get; private set; }
        
        public bool ScrollDown { get; private set; }
        public bool ScrollHold { get; private set; }
        public bool ScrollUp { get; private set; }

        public float ScrollDelta { get; private set; }

        
        public Vector3 MousePosition { get; private set; }

        public void Tick()
        {
            LmbDown = Input.GetMouseButtonDown(0);
            LmbHold = Input.GetMouseButton(0);
            LmbUp = Input.GetMouseButtonUp(0);
            
            RmbDown = Input.GetMouseButtonDown(1);
            RmbHold = Input.GetMouseButton(1);
            RmbUp = Input.GetMouseButtonUp(1);

            ScrollDown = Input.GetMouseButtonDown(2);
            ScrollHold = Input.GetMouseButton(2);
            ScrollUp = Input.GetMouseButtonUp(2);
            
            ScrollDelta = Input.mouseScrollDelta.y;
            
            MousePosition = Input.mousePosition;
        }
        
        public bool MouseCursorOverUi()
            => EventSystem.current.IsPointerOverGameObject();
    }

    public interface IInputProvider
    {
        public bool LmbDown { get; }
        public bool LmbHold { get; }
        public bool LmbUp { get; }

        public bool RmbDown { get; }
        public bool RmbHold { get; }
        public bool RmbUp { get; }
        
        public bool ScrollDown { get; }
        public bool ScrollHold { get; }
        public bool ScrollUp { get; }
        
        public float ScrollDelta { get; }
        public Vector3 MousePosition { get; }
        
        public bool MouseCursorOverUi();
    }
}