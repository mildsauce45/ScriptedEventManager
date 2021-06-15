using System;
using UnityEngine;

namespace FirstWave.ScriptedEvents.Common
{
    [Serializable]
    public class ActorToMove
    {
        private int _currentTargetIndex = -1;
        private bool _isMoving;
        private Vector2 _target;
        private Transform _transform;

        public GameObject obj;
        public Vector2[] targets;
        public float moveSpeed;

        public bool IsPathDone => _currentTargetIndex >= targets.Length;

        public void Initialize()
        {
            _currentTargetIndex = -1;
            _transform = obj.transform;
        }

        public void UpdateTarget()
        {
            _currentTargetIndex++;

            if (!IsPathDone)
            {
                _target = targets[_currentTargetIndex];
                _isMoving = true;
            }
        }

        public void Move()
        {
            if (_isMoving)
            {
                var newPosition = Vector2.MoveTowards(_transform.position, _target, Time.deltaTime * moveSpeed);

                // At a certain point just clamp down to the target
                if ((newPosition - _target).magnitude < 0.025f)
                {
                    _transform.position = _target;
                    _isMoving = false;

                    UpdateTarget();
                }
                else
                {
                    _transform.position = newPosition;
                }
            }
        }
    }
}
