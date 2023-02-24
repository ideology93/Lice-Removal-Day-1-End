using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;


public enum ShakeType
{ Position, Rotation }

    public class Shake : TweenBase
    {
        //         DOShakeRotation(float duration, float/Vector3 strength, int vibrato, float randomness, bool fadeOut)

        [Header("Shake Tween Settings")]
        [SerializeField] private ShakeType _shakeType;

        [ShowIf("_isShakingPosition")]
        [SerializeField] private Vector3 _position;
        [ShowIf("_isShakingRotation")]
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _duration;
        [SerializeField] private float _strength;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _randomness;



        private Vector3 _targetShake;
        private bool _isShakingPosition { get { return _shakeType == ShakeType.Position; } }
        private bool _isShakingRotation { get { return _shakeType == ShakeType.Rotation; } }


        protected override void SetTweener()
        {
            SetShake();

            if(_isShakingPosition)
            Tweener = transform
                .DOShakePosition( _duration, _strength, _vibrato, _randomness)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay);
            else
            Tweener = transform
                .DOShakePosition( _duration, _strength, _vibrato, _randomness)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay);

        }

        private void SetShake() => _targetShake = _isShakingPosition ? _position : _rotation;

    }

