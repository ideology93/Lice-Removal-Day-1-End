using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public enum MoveType
{ MoveTo, MoveBy, MoveTowards }


    public class Move : TweenBase
    {
        [Header("Move Tween Settings")]
        [SerializeField] private MoveType _moveType;

        [ShowIf("_isMovingTo")]
        [SerializeField] private Vector3 _position;
        [ShowIf("_isMovingBy")]
        [SerializeField] private Vector3 _amount;
        [ShowIf("_isMovingTowards")]
        [SerializeField] public Transform _target;

        private Vector3 _targetPosition;
        private bool _isMovingTo { get { return _moveType == MoveType.MoveTo; } }
        private bool _isMovingBy { get { return _moveType == MoveType.MoveBy; } }
        private bool _isMovingTowards { get { return _moveType == MoveType.MoveTowards; } }

        protected override void SetTweener()
        {
            SetTargetPosition();

            Tweener = transform
                .DOLocalMove(_targetPosition, LoopDuration)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay)
                .SetRelative(_isMovingBy);
                Debug.Log(_isMovingBy);
        }

        private void SetTargetPosition()
        {
            if (_isMovingTo)
            {
                _targetPosition = _position;
            }
            else if (_isMovingBy){
                _targetPosition = _amount;
            }
            else
                _targetPosition = _target.position;
        }

    }

