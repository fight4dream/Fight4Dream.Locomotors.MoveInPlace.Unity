using UnityEngine;
using Zinnia.Extension;
using Zinnia.Process;
using Zinnia.Tracking.Velocity;

namespace Fight4Dream.Locomotors.MoveInPlace.Unity
{
    public class CustomArtificialVelocityApplier : ArtificialVelocityApplier, IProcessable
    {
        protected bool canProcess { get; set; }

        public override void Apply()
        {
            canProcess = true;
        }

        public void Process()
        {
            if (!canProcess)
            {
                return;
            }

            if (!Velocity.ApproxEquals(Vector3.zero, NilVelocityTolerance) || !AngularVelocity.ApproxEquals(Vector3.zero, NilAngularVelocityTolerance))
            {
                float deltaTime = Time.inFixedTimeStep ? Time.fixedDeltaTime : Time.deltaTime;
                Velocity = Vector3.Slerp(Velocity, Vector3.zero, Drag * deltaTime);
                AngularVelocity = Vector3.Slerp(AngularVelocity, Vector3.zero, AngularDrag * deltaTime);
                Target.transform.localRotation *= Quaternion.Euler(AngularVelocity);
                Target.transform.localPosition += Velocity * Time.deltaTime;
            }
            else
            {
                Velocity = Vector3.zero;
                AngularVelocity = Vector3.zero;
                canProcess = false;
            }
        }

        protected override void CancelDeceleration()
        {
            canProcess = false;
        }
    }
}
