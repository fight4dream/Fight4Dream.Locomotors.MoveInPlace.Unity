namespace Fight4Dream.Locomotors.MoveInPlace.Unity
{
    using Fight4Dream.Tracking.Velocity;
    using UnityEngine;
    using Zinnia.Action;
    using Zinnia.Action.Collection;
    using Zinnia.Data.Attribute;
    using Zinnia.Data.Collection.List;
    using Zinnia.Data.Type.Transformation.Aggregation;
    using Zinnia.Extension;
    using Zinnia.Tracking.Velocity;

    public class MoveInPlace : MonoBehaviour
    {
        public enum EngageMode { AnyEngaged, AllEngaged }
        [SerializeField]
        private EngageMode moveWhen = EngageMode.AnyEngaged;
        public EngageMode MoveWhen
        {
            get
            {
                return moveWhen;
            }
            set
            {
                moveWhen = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterMoveWhenChange();
                }
            }
        }

        [SerializeField]
        private BooleanAction leftControllerAction = null;
        public BooleanAction LeftControllerAction
        {
            get
            {
                return leftControllerAction;
            }
            set
            {
                leftControllerAction = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterLeftControllerActionChange();
                    OnAfterAnyControllerActionChange();
                }
            }
        }

        [SerializeField]
        private BooleanAction rightControllerAction = null;
        public BooleanAction RightControllerAction
        {
            get
            {
                return rightControllerAction;
            }
            set
            {
                rightControllerAction = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterRightControllerActionChange();
                    OnAfterAnyControllerActionChange();
                }
            }
        }

        [SerializeField]
        private VelocityTracker leftControllerVelocityTracker = null;
        public VelocityTracker LeftControllerVelocityTracker
        {
            get
            {
                return leftControllerVelocityTracker;
            }
            set
            {
                leftControllerVelocityTracker = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterLeftControllerVelocityTrackerChange();
                }
            }
        }

        [SerializeField]
        private VelocityTracker rightControllerVelocityTracker = null;
        public VelocityTracker RightControllerVelocityTracker
        {
            get
            {
                return rightControllerVelocityTracker;
            }
            set
            {
                rightControllerVelocityTracker = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterRightControllerVelocityTrackerChange();
                }
            }
        }

        [SerializeField]
        private GameObjectObservableList forwardSources = null;
        public GameObjectObservableList ForwardSources
        {
            get
            {
                return forwardSources;
            }
            set
            {
                forwardSources = value;
            }
        }

        [SerializeField]
        private GameObject target = null;
        public GameObject Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterTargetChange();
                }
            }
        }

        [SerializeField]
        private float speedMultiplier = 1f;
        public float SpeedMultiplier
        {
            get
            {
                return speedMultiplier;
            }
            set
            {
                speedMultiplier = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterSpeedMutiplierChange();
                }
            }
        }

        [SerializeField]
        private float speedThreshold = 0.1f;
        public float SpeedThreshold
        {
            get
            {
                return speedThreshold;
            }
            set
            {
                speedThreshold = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterSpeedThresholdChange();
                }
            }
        }

        [SerializeField]
        private float drag = 1f;
        public float Drag
        {
            get
            {
                return drag;
            }
            set
            {
                drag = value;
                if (this.IsMemberChangeAllowed())
                {
                    OnAfterDragChange();
                }
            }
        }

        [Header("Reference Settings")]
        [Restricted]
        [SerializeField]
        private AnyAction anyEngaged = null;
        public AnyAction AnyEngaged
        {
            get
            {
                return anyEngaged;
            }
            set
            {
                anyEngaged = value;
            }
        }

        [Restricted]
        [SerializeField]
        private AllAction allEngaged = null;
        public AllAction AllEngaged
        {
            get
            {
                return allEngaged;
            }
            set
            {
                allEngaged = value;
            }
        }

        [Restricted]
        [SerializeField]
        private ActionObservableList engagedControllers = null;
        public ActionObservableList EngagedControllers
        {
            get
            {
                return engagedControllers;
            }
            set
            {
                engagedControllers = value;
            }
        }

        [Restricted]
        [SerializeField]
        private BooleanAction engageLeftController = null;
        public BooleanAction EngageLeftController
        {
            get
            {
                return engageLeftController;
            }
            set
            {
                engageLeftController = value;
            }
        }

        [Restricted]
        [SerializeField]
        private BooleanAction engageRightController = null;
        public BooleanAction EngageRightController
        {
            get
            {
                return engageRightController;
            }
            set
            {
                engageRightController = value;
            }
        }

        [Restricted]
        [SerializeField]
        private ComponentTrackerProxy leftControllerProxy = null;
        public ComponentTrackerProxy LeftControllerProxy
        {
            get
            {
                return leftControllerProxy;
            }
            set
            {
                leftControllerProxy = value;
            }
        }

        [Restricted]
        [SerializeField]
        private ComponentTrackerProxy rightControllerProxy = null;
        public ComponentTrackerProxy RightControllerProxy
        {
            get
            {
                return rightControllerProxy;
            }
            set
            {
                rightControllerProxy = value;
            }
        }

        [Restricted]
        [SerializeField]
        private SpeedChecker speedChecker = null;
        public SpeedChecker SpeedChecker
        {
            get
            {
                return speedChecker;
            }
            set
            {
                speedChecker = value;
            }
        }

        [Restricted]
        [SerializeField]
        private FloatMultiplier setSpeedMultiplier = null;
        public FloatMultiplier SetSpeedMultiplier
        {
            get
            {
                return setSpeedMultiplier;
            }
            set
            {
                setSpeedMultiplier = value;
            }
        }

        [Restricted]
        [SerializeField]
        private ArtificialVelocityApplierProcess moveTarget = null;
        public ArtificialVelocityApplierProcess MoveTarget
        {
            get
            {
                return moveTarget;
            }
            set
            {
                moveTarget = value;
            }
        }

        protected virtual void OnAfterMoveWhenChange()
        {
            if (AnyEngaged != null && AllEngaged != null)
            {
                AnyEngaged.gameObject.SetActive(MoveWhen == EngageMode.AnyEngaged);
                AllEngaged.gameObject.SetActive(MoveWhen == EngageMode.AllEngaged);
            }
        }

        protected virtual void OnAfterLeftControllerActionChange()
        {
            if (EngageLeftController != null)
            {
                EngageLeftController.ClearSources();
                EngageLeftController.AddSource(LeftControllerAction);
            }
        }

        protected virtual void OnAfterRightControllerActionChange()
        {
            if (EngageRightController != null)
            {
                EngageRightController.ClearSources();
                EngageRightController.AddSource(RightControllerAction);
            }
        }

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

        protected virtual void OnAfterTargetChange()
        {
            if (MoveTarget != null)
            {
                MoveTarget.Target = Target;
            }
        }

        protected virtual void OnAfterSpeedMutiplierChange()
        {
            if (SetSpeedMultiplier != null)
            {
                SetSpeedMultiplier.Collection.SetAtOrAddIfEmpty(SpeedMultiplier, 0);
            }
        }

        protected virtual void OnAfterSpeedThresholdChange()
        {
            if (SpeedChecker != null)
            {
                SpeedChecker.SpeedThreshold = SpeedThreshold;
            }
        }

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
            OnAfterTargetChange();
            OnAfterSpeedMutiplierChange();
            OnAfterSpeedThresholdChange();
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