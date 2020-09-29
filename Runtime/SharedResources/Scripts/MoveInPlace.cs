namespace Fight4Dream.Locomotors.MoveInPlace.Unity
{
    using Fight4Dream.Tracking.Velocity;
    using Malimbe.MemberChangeMethod;
    using Malimbe.MemberClearanceMethod;
    using Malimbe.PropertySerializationAttribute;
    using UnityEngine;
    using Zinnia.Action;
    using Zinnia.Data.Attribute;
    using Zinnia.Data.Operation.Extraction;
    using Zinnia.Data.Type.Transformation.Aggregation;
    using Zinnia.Tracking.Velocity;

    public class MoveInPlace : MonoBehaviour
    {
        [Serialized, Cleared]
        public BooleanAction LeftControllerAction { get; set; }
        [Serialized, Cleared]
        public BooleanAction RightControllerAction { get; set; }
        [Serialized, Cleared]
        public VelocityTracker LeftControllerVelocityTracker { get; set; }
        [Serialized, Cleared]
        public VelocityTracker RightControllerVelocityTracker { get; set; }
        [Serialized, Cleared]
        public GameObject ForwardSource { get; set; }
        [Serialized, Cleared]
        public GameObject Target { get; set; }
        [Serialized]
        public float SpeedMultiplier { get; set; } = 1f;
        [Serialized]
        public float SpeedThreshold { get; set; } = 0.1f;
        [Serialized]
        public float Drag { get; set; } = 1f;

        [Serialized, Cleared]
        [field: Header("Reference Settings"), Restricted]
        public BooleanAction EngageLeftController { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public BooleanAction EngageRightController { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public ComponentTrackerProxy LeftControllerProxy { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public ComponentTrackerProxy RightControllerProxy { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public SpeedChecker SpeedChecker { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public FloatMultiplier SetSpeedMultiplier { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public TransformDirectionExtractor SetMoveVelocity { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public ArtificialVelocityApplier MoveTarget { get; set; }

        [CalledAfterChangeOf(nameof(LeftControllerVelocityTracker))]
        protected virtual void OnAfterLeftControllerVelocityTrackerChange()
        {
            if (LeftControllerProxy != null)
            {
                if (LeftControllerVelocityTracker != null)
                {
                    LeftControllerProxy.ProxySource = LeftControllerVelocityTracker.gameObject;
                }
                else
                {
                    LeftControllerProxy.ProxySource = null;
                }
            }
        }

        [CalledAfterChangeOf(nameof(RightControllerVelocityTracker))]
        protected virtual void OnAfterRightControllerVelocityTrackerChange()
        {
            if (RightControllerProxy != null)
            {
                if (RightControllerVelocityTracker != null)
                {
                    RightControllerProxy.ProxySource = RightControllerVelocityTracker.gameObject;
                }
                else
                {
                    RightControllerProxy.ProxySource = null;
                }
            }
        }

        [CalledAfterChangeOf(nameof(LeftControllerAction))]
        protected virtual void OnAfterLeftControllerActionChange()
        {
            if (EngageLeftController != null)
            {
                EngageLeftController.ClearSources();
                EngageLeftController.AddSource(LeftControllerAction);
            }
        }

        [CalledAfterChangeOf(nameof(RightControllerAction))]
        protected virtual void OnAfterRightControllerActionChange()
        {
            if (EngageRightController != null)
            {
                EngageRightController.ClearSources();
                EngageRightController.AddSource(RightControllerAction);
            }
        }

        [CalledAfterChangeOf(nameof(SpeedThreshold))]
        protected virtual void OnAfterSpeedThresholdChange()
        {
            if (SpeedChecker != null)
            {
                SpeedChecker.SpeedThreshold = SpeedThreshold;
            }
        }

        [CalledAfterChangeOf(nameof(SpeedMultiplier))]
        protected virtual void OnAfterSpeedMutiplierChange()
        {
            if (SetSpeedMultiplier != null)
            {
                SetSpeedMultiplier.Collection.SetAtOrAddIfEmpty(SpeedMultiplier, 0);
            }
        }

        [CalledAfterChangeOf(nameof(ForwardSource))]
        protected virtual void OnAfterForwardSourceChange()
        {
            if (SetMoveVelocity != null)
            {
                SetMoveVelocity.Source = ForwardSource;
            }
        }

        [CalledAfterChangeOf(nameof(Target))]
        protected virtual void OnAfterTargetChange()
        {
            if (MoveTarget != null)
            {
                MoveTarget.Target = Target;
            }
        }

        [CalledAfterChangeOf(nameof(Drag))]
        protected virtual void OnAfterDragChange()
        {
            if (MoveTarget != null)
            {
                MoveTarget.Drag = Drag;
            }
        }

        protected virtual void OnEnable()
        {
            OnAfterLeftControllerActionChange();
            OnAfterLeftControllerVelocityTrackerChange();
            OnAfterRightControllerActionChange();
            OnAfterRightControllerVelocityTrackerChange();
            OnAfterSpeedThresholdChange();
            OnAfterSpeedMutiplierChange();
            OnAfterForwardSourceChange();
            OnAfterTargetChange();
            OnAfterDragChange();
            if (LeftControllerProxy != null)
            {
                LeftControllerProxy.gameObject.SetActive(false);
            }
            if (RightControllerProxy != null)
            {
                RightControllerProxy.gameObject.SetActive(false);
            }
        }
    }
}