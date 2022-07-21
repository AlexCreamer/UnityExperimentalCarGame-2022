    using UnityEngine;
    using Cinemachine;
    using Cinemachine.Utility;
     
    public class SimpleFollowRecenter : CinemachineExtension
    {
        public bool Recenter;
        public float RecenterTime = 0.5f;
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body)
                return;
     
            Transform target = vcam != null ? vcam.Follow : null;
            if (target == null)
                return;
     
            if (Recenter)
            {
                // How far away from centered are we?
                Vector3 up = vcam.State.ReferenceUp;
                Vector3 back = vcam.transform.position - target.position;
                float angle = UnityVectorExtensions.SignedAngle(
                    back.ProjectOntoPlane(up), -target.forward.ProjectOntoPlane(up), up);
                if (Mathf.Abs(angle) < 0.1f)
                    Recenter = false; // done!
                else
                {
                    angle = Damper.Damp(angle, RecenterTime, deltaTime);
                    Vector3 pos = state.RawPosition - target.position;
                    pos = Quaternion.AngleAxis(angle, up) * pos;
                    state.RawPosition = pos + target.position;
                }
            }
        }
    }
     
