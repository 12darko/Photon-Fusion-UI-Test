using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EMA.Scripts.Utils
{
    public class RotateAround : MonoBehaviour
    {
        [SerializeField] private Vector3 rotatePos;
        [SerializeField] private float duration;
        
        [SerializeField] private bool randomizeDuration;
        [SerializeField] private bool randomizePos;

        private void OnEnable()
        {
            if (randomizeDuration)
                duration = Random.Range(duration - 1f, duration + 1f);
            if (randomizePos && MyShortcuts.MyShortcuts.GetRandomBoolByChance(.5f))
                rotatePos *= -1f;

            Rotate();
        }

        private void OnDisable()
        {
            StopRotating();
        }
        
        public void Rotate()
        {
            transform.DOLocalRotate(transform.localRotation.eulerAngles + (rotatePos * 360f), duration,
                RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        public void StopRotating()
        {
            DOTween.Kill(transform);
        }
    }
}
