using UnityEngine;
using System;

namespace RunnerMovementSystem.Examples
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private MovementSystem _roadMovement;
        [SerializeField] private float _sensitivity = 0.01f;


        private bool _canRun;
        private Vector3 _mousePosition;
        private float _saveOffset;
        private Vector3 _mouseDownPosition;
        private Vector3 _previoutMousePosition;
        private float _offset;
        private StartLevelButton _startLevelButton;

        public event Action RunBegan;

        public bool IsMoved { get; private set; }

        private void OnEnable()
        {
            _startLevelButton = FindObjectOfType<StartLevelButton>();
            _startLevelButton.RunStarted += OnRunStarted;
            _roadMovement.PathChanged += OnPathChanged;
        }

        private void OnDisable()
        {
            _roadMovement.PathChanged -= OnPathChanged;
            _startLevelButton.RunStarted -= OnRunStarted;
        }

        private void OnPathChanged(PathSegment _)
        {
            _saveOffset = _roadMovement.Offset;
            _mousePosition = Input.mousePosition;
        }

        private void Update()
        {
            if (_canRun == false)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _saveOffset = _roadMovement.Offset;
                _mouseDownPosition = Input.mousePosition;
                _previoutMousePosition = Input.mousePosition;
                RunBegan?.Invoke();
                IsMoved = true;
            }

            if (Input.GetMouseButton(0))
            {
                var offset = Input.mousePosition - _previoutMousePosition;

                _roadMovement.SetOffset(_saveOffset + offset.x * _sensitivity);
                _saveOffset = _roadMovement.Offset;

                _previoutMousePosition = Input.mousePosition;
            }

            if(IsMoved)
                _roadMovement.MoveForward();
        }

        private void OnRunStarted()
        {
            _canRun = true;
            _startLevelButton.RunStarted -= OnRunStarted;
        }
    }
}
