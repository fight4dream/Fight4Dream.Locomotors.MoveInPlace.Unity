namespace Fight4Dream.Locomotors.MoveInPlace.Unity
{
    using System;
    using Malimbe.BehaviourStateRequirementMethod;
    using Malimbe.PropertySerializationAttribute;
    using Malimbe.XmlDocumentationAttribute;
    using UnityEngine;
    using UnityEngine.Events;
    using Zinnia.Tracking.Velocity;
    using Zinnia.Tracking.Velocity.Collection;

    /// <summary>
    /// Processes the average speed of all active <see cref="VelocityTracker"/> found in the given <see cref="VelocityTrackers"/>.
    /// </summary>
    public class AverageSpeedEmitter : MonoBehaviour
    {
        /// <summary>
        /// Defines the event with the <see cref="float"/>.
        /// </summary>
        [Serializable]
        public class FloatUnityEvent : UnityEvent<float>
        {
        }

        /// <summary>
        /// The <see cref="VelocityTracker"/> collection to attempt to process.
        /// </summary>
        [Serialized]
        [field: DocumentedByXml]
        public VelocityTrackerObservableList VelocityTrackers { get; set; }

        /// <summary>
        /// Emitted when the Tracked Speed is emitted.
        /// </summary>
        [DocumentedByXml]
        public FloatUnityEvent SpeedEmitted = new FloatUnityEvent();

        /// <summary>
        /// Emits the average speed of all active <see cref="VelocityTracker"/>.
        /// </summary>
        [RequiresBehaviourState]
        public virtual void EmitSpeed()
        {
            if (VelocityTrackers == null)
            {
                return;
            }

            float sum = 0;
            int n = 0;
            foreach (VelocityTracker tracker in VelocityTrackers.NonSubscribableElements)
            {
                if (tracker.IsActive())
                {
                    sum += tracker.GetVelocity().magnitude;
                    ++n;
                }
            }
            SpeedEmitted?.Invoke(n == 0 ? 0 : sum / n);
        }
    }
}