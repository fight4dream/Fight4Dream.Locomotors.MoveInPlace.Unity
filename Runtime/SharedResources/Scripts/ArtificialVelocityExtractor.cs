namespace Fight4Dream.Locomotors.MoveInPlace.Unity
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using Zinnia.Data.Operation.Extraction;
    using Zinnia.Tracking.Velocity;

    /// <summary>
    /// Get the current Velocity from the given <see cref="ArtificialVelocityApplier"/>.
    /// </summary>
    public class ArtificialVelocityExtractor : Vector3Extractor<ArtificialVelocityApplier, ArtificialVelocityExtractor.UnityEvent>
    {
        /// <summary>
        /// Defines an event with a <see cref="Vector3"/> value.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<Vector3> { }

        /// <inheritdoc />
        protected override Vector3? ExtractValue()
        {
            if (Source == null)
            {
                return null;
            }

            return Source.Velocity;
        }
    }
}
