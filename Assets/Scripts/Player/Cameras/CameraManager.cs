using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera otsCamera; // C�mara en tercera persona
    public CinemachineCamera aimingCamera; // C�mara de apuntado

    private bool isAiming = false;
    public bool mouseLock;

    private void Start()
    {
        otsCamera.Priority = 5;
        aimingCamera.Priority = 1;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Bot�n derecho del mouse
        {
            isAiming = true;
            SwitchCamera();
        }
        else if (Input.GetMouseButtonUp(1)) // Soltar bot�n derecho
        {
            isAiming = false;
            SwitchCamera();
        }
        if (mouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void SwitchCamera()
    {
        if (isAiming)
        {
            otsCamera.Priority = 1;
            aimingCamera.Priority = 5;
        }
        else
        {
            otsCamera.Priority = 5;
            aimingCamera.Priority = 1;
        }
    }
}
