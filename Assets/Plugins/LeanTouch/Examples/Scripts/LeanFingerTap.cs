using System;
using UnityEngine;
using UnityEngine.Events;

namespace Lean.Touch
{
    // This script calls the OnFingerTap event when a finger taps the screen
    public class LeanFingerTap : MonoBehaviour
    {
        [Tooltip("If the finger is over the GUI, ignore it?")]
        public bool IgnoreIfOverGui;

        [Tooltip("If the finger started over the GUI, ignore it?")]
        public bool IgnoreIfStartedOverGui;

        [Tooltip("How many times must this finger tap before OnFingerTap gets called? (0 = every time)")]
        public int RequiredTapCount;

        [Tooltip(
            "How many times repeating must this finger tap before OnFingerTap gets called? (e.g. 2 = 2, 4, 6, 8, etc) (0 = every time)")]
        public int RequiredTapInterval;

        public LeanFingerEvent OnFingerTap;

        protected virtual void OnEnable()
        {
            // Hook events
            LeanTouch.OnFingerTap += FingerTap;
        }

        protected virtual void OnDisable()
        {
            // Unhook events
            LeanTouch.OnFingerTap -= FingerTap;
        }

        private void FingerTap(LeanFinger finger)
        {
            // Ignore?

            if (IgnoreIfOverGui && finger.IsOverGui) return;

            if (IgnoreIfStartedOverGui && finger.StartedOverGui) return;

            if (RequiredTapCount > 0 && finger.TapCount != RequiredTapCount) return;

            if (RequiredTapInterval > 0 && finger.TapCount % RequiredTapInterval != 0) return;

            // Call event
            OnFingerTap.Invoke(finger);
        }

        // Event signature
        [Serializable]
        public class LeanFingerEvent : UnityEvent<LeanFinger>
        {
        }
    }
}