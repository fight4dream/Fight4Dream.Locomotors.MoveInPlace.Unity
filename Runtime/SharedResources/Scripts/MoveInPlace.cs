namespace Fight4Dream.Locomotors.MoveInPlace.Unity
{
    using Fight4Dream.Tracking.Velocity;
    using Malimbe.MemberChangeMethod;
    using Malimbe.MemberClearanceMethod;
    using Malimbe.PropertySerializationAttribute;
    using UnityEngine;
    using Zinnia.Action;
    using Zinnia.Action.Collection;
    using Zinnia.Data.Attribute;
    using Zinnia.Data.Collection.List;
    using Zinnia.Data.Type.Transformation.Aggregation;
    using Zinnia.Tracking.Velocity;

    public class MoveInPlace : MonoBehaviour
    {
        public enum EngageMode { AnyEngaged, AllEngaged }
        [Serialized]
        public EngageMode MoveWhen { get; set; } = EngageMode.AnyEngaged;
        [Serialized, Cleared]
        public BooleanAction LeftControllerAction { get; set; }
        [Serialized, Cleared]
        public BooleanAction RightControllerAction { get; set; }
        [Serialized, Cleared]
        public VelocityTracker LeftControllerVelocityTracker { get; set; }
        [Serialized, Cleared]
        public VelocityTracker RightControllerVelocityTracker { get; set; }
        [Serialized, Cleared]
        public GameObjectObservableList ForwardSources { get; set; }
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
        public AnyAction AnyEngaged { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public AllAction AllEngaged { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
        public ActionObservableList EngagedControllers { get; set; }
        [Serialized, Cleared]
        [field: Restricted]
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
        public ArtificialVelocityApplierProcess MoveTarget { get; set; }

        [CalledAfterChangeOf(nameof(MoveWhen))]
        protected virtual void OnAfterMoveWhenChange()
        {
            if (AnyEngaged != null && AllEngaged != null)
            {
                AnyEngaged.gameObject.SetActive(MoveWhen == EngageMode.AnyEngaged);
                AllEngaged.gameObject.SetActive(MoveWhen == EngageMode.AllEngaged);
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

        [CalledAfterChangeOf(nameof(LeftControllerAction))]
        [CalledAfterChangeOf(nameof(RightControllerAction))]
        protected virtual void OnAfterAnyControllerActionChange()
        {
            if (EngagedControllers != null)
            {
                EngagedControllers.Clear();
                if (LeftControllerAction != null)
                {
                    EngagedControllers.Add(LeftControllerAction);
                }
                if (RightControllerAction != null)
                {
                    EngagedControllers.Add(RightControllerAction);
                }
            }
        }

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
            OnAfterMoveWhenChange();
            OnAfterLeftControllerActionChange();
            OnAfterRightControllerActionChange();
            OnAfterAnyControllerActionChange();
            OnAfterLeftControllerVelocityTrackerChange();
            OnAfterRightControllerVelocityTrackerChange();
            OnAfterSpeedThresholdChange();
            OnAfterSpeedMutiplierChange();
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