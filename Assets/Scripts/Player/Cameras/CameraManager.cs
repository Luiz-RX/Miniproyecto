using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera otsCamera; // Cámara en tercera persona
    public CinemachineCamera aimingCamera; // Cámara de apuntado

    private bool isAiming = false;

    private void Start()
    {
        otsCamera.Priority = 5;
        aimingCamera.Priority = 1;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Botón derecho del mouse
        {
            isAiming = true;
            SwitchCamera();
        }
        else if (Input.GetMouseButtonUp(1)) // Soltar botón derecho
        {
            isAiming = false;
            SwitchCamera();
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
