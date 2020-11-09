namespace Fight4Dream.Locomotors.MoveInPlace.Unity
{
    using System;
    using Malimbe.PropertySerializationAttribute;
    using Malimbe.XmlDocumentationAttribute;
    using UnityEngine;
    using UnityEngine.Events;
    using Zinnia.Data.Collection.List;
    using Zinnia.Data.Operation.Extraction;
    using Zinnia.Extension;

    /// <summary>
    /// Extracts a chosen axis of a list of <see cref="Transform"/> and takes their average.
    /// </summary>
    public class AverageDirectionExtractor : Vector3Extractor<GameObjectObservableList, AverageDirectionExtractor.UnityEvent>
    {
        /// <summary>
        /// Defines the event with the specified <see cref="Vector3"/>.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<Vector3> { }

        /// <summary>
        /// Determines whether to extract the local property or the world property.
        /// </summary>
        [Serialized]
        [field: DocumentedByXml]
        public bool UseLocal { get; set; }

        /// <summary>
        /// The direction axes of the transform.
        /// </summary>
        public enum AxisDirection
        {
            /// <summary>
            /// The axis moving right from the transform origin.
            /// </summary>
            Right,
            /// <summary>
            /// The axis moving up from the transform origin.
            /// </summary>
            Up,
            /// <summary>
            /// The axis moving forward from the transform origin.
            /// </summary>
            Forward
        }

        /// <summary>
        /// The direction to extract from the <see cref="Transform"/>.
        /// </summary>
        [Serialized]
        [field: DocumentedByXml]
        public AxisDirection Direction { get; set; }

        /// <summary>
        /// Sets the <see cref="Direction"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="AxisDirection"/>.</param>
        public virtual void SetDirection(int index)
        {
            Direction = EnumExtensions.GetByIndex<AxisDirection>(index);
        }

        /// <inheritdoc />
        protected override Vector3? ExtractValue()
        {
            if (Source == null)
            {
                return null;
            }

            int count = 0;
            Vector3 sum = Vector3.zero;
            foreach (GameObject source in Source.NonSubscribableElements)
            {
                if (source.activeInHierarchy)
                {
                    switch (Direction)
                    {
                        case AxisDirection.Right:
                            sum += UseLocal ? source.transform.localRotation * Vector3.right : source.transform.right;
                            break;
                        case AxisDirection.Up:
                            sum += UseLocal ? source.transform.localRotation * Vector3.up : source.transform.up;
                            break;
                        case AxisDirection.Forward:
                            sum += UseLocal ? source.transform.localRotation * Vector3.forward : source.transform.forward;
                            break;
                    }
                    ++count;
                }
            }

            if (count == 0)
            {
                return null;
            }
            return sum / count;
        }
    }
}
