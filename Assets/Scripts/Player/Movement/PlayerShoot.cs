using GameAssets;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private Animator animator;
    private float aimLayerWeight = 0f;

    private bool mouseLock = true;



    private void Start()
    {
        animator = GetComponent<Animator>();
        virtualCamera.Lens.FieldOfView = 60;
        HandleCursor();
    }
    void Update()
    {
        HandleAimingShoot();
        
    }

    private void HandleAimingShoot()
    {
        bool isAiming = Input.GetMouseButton(1); // Se mantiene mientras el botón derecho está presionado
        

        //aimLayerWeight = Mathf.MoveTowards(aimLayerWeight, targetWeight, Time.deltaTime * 5f);
        //animator.SetLayerWeight(1, aimLayerWeight);
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCentrePoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        if (isAiming)
        {
            virtualCamera.Lens.FieldOfView = 40;

            //playerMovement.SetRotateOnMove(false);
            //animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            aimLayerWeight = Mathf.MoveTowards(aimLayerWeight, 1f, Time.deltaTime * 5f);
            animator.SetLayerWeight(1, aimLayerWeight);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            //if (Input.GetMouseButtonDown(0))
            //{

            //    Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            //    Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

            //}
        }
        else
        {
            virtualCamera.Lens.FieldOfView = 60;

            //playerMovement.SetRotateOnMove(true);
            //animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            aimLayerWeight = Mathf.MoveTowards(aimLayerWeight, 0f, Time.deltaTime * 5f);
            animator.SetLayerWeight(1, aimLayerWeight);
        }
        

    }

    public void HandleCursor()
    {
        if (mouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    
}
