using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLook : CinemachineExtension
{
    [SerializeField] private float speed;
    [SerializeField] private float clampAngle = 80f;
    private CursorMap cursorActions;
    Vector3 cameraRotation;


    protected override void Awake()
    {
        cursorActions = new CursorMap();
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (cameraRotation == null)
                    cameraRotation = transform.localRotation.eulerAngles;

                var deltaInput = cursorActions.Cursor.MoveCursor.ReadValue<Vector2>();
                cameraRotation.x += deltaInput.x * speed * Time.deltaTime;
                cameraRotation.y += deltaInput.y * speed * Time.deltaTime;
                cameraRotation.y = Mathf.Clamp(cameraRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0f);
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        cursorActions.Enable();
    }

    private void OnDisable()
    {
        cursorActions.Disable();
    }


}
