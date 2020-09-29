namespace Fight4Dream.Locomotors.MoveInPlace.Unity
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using Zinnia.Data.Collection.List;
    using Zinnia.Data.Type.Transformation.Aggregation;

    /// <summary>
    /// Adds a collection of <see cref="Vector3"/>s by adding each one from the first entry in the collection.
    /// </summary>
    /// <example>
    /// Vector3.one + Vector3.one = Vector3(2f, 2f, 2f)
    /// </example>
    public class Vector3Adder : CollectionAggregator<Vector3, Vector3, Vector3Adder.UnityEvent, Vector3ObservableList, Vector3ObservableList.UnityEvent>
    {
        /// <summary>
        /// Defines the event with the aggregated <see cref="Vector3"/> value.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<Vector3>
        {
        }

        /// <inheritdoc />
        protected override Vector3 ProcessCollection()
        {
            if (Collection.NonSubscribableElements.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 sum = Collection.NonSubscribableElements[0];
            for (int index = 1; index < Collection.NonSubscribableElements.Count; index++)
            {
                sum += Collection.NonSubscribableElements[index];
            }

            return sum;
        }
    }
}