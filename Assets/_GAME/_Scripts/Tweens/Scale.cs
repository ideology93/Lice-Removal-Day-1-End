using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public enum ScaleType
{ ScaleTo, ScaleBy }


    public class Scale : TweenBase
    {
        [Header("Scale Tween Settings")]
        [SerializeField] private ScaleType _scaleType;

        [ShowIf("_isScaleTo")]
        [SerializeField] private Vector3 _scale;
        [ShowIf("_isScaleBy")]
        [SerializeField] private Vector3 _amount;

        private Vector3 _targetScale;
        private bool _isScaleTo { get { return _scaleType == ScaleType.ScaleTo; } }
        private bool _isScaleBy { get { return _scaleType == ScaleType.ScaleBy; } }

        protected override void SetTweener()
        {
            SetTargetScale();

            Tweener = transform
                .DOScale(_targetScale, LoopDuration)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay)
                .SetRelative(_isScaleBy);
        }

        private void SetTargetScale() => _targetScale = _isScaleTo ? _scale : _amount;
    }
