//using UnityEngine;
//using GameAssets;
//using UnityEngine.InputSystem;
//using Unity.Mathematics;
//using Unity.Cinemachine;

//public class ThirdPersonShooterController : MonoBehaviour
//{
//    [SerializeField] private CinemachineCamera aimVirtualCamera;
//    [SerializeField] private float normalSensitivity;
//    [SerializeField] private float aimSensitivity;
//    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
//    [SerializeField] private Transform debugTransform;
//    [SerializeField] private Transform pfBulletProjectile;
//    [SerializeField] private Transform spawnBulletPosition;

//    private PlayerMovement playerMovement;
//    private GameAssetsInputs starterAssetsInputs;
//    private Animator animator;

//    private void Awake()
//    {
//        starterAssetsInputs = GetComponent<GameAssetsInputs>();
//        playerMovement = GetComponent<ThirdPersonController>();
//        animator = GetComponent<Animator>();
//    }
//    private void Update()
//    {
//        Vector3 mouseWorldPosition = Vector3.zero;
//        Vector2 screenCentrePoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
//        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);
//        Transform hitTransform = null;
//        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
//        {
//            debugTransform.position = raycastHit.point;
//            mouseWorldPosition = raycastHit.point;
//            hitTransform = raycastHit.transform;
//        }
//        if (starterAssetsInputs.aim)
//        {
//            aimVirtualCamera.gameObject.SetActive(true);
//            playerMovement.SetSensitivity(aimSensitivity);
//            playerMovement.SetRotateOnMove(false);
//            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

//            Vector3 worldAimTarget = mouseWorldPosition;
//            worldAimTarget.y = transform.position.y;
//            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

//            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
//        }
//        else
//        {
//            aimVirtualCamera.gameObject.SetActive(false);
//            playerMovement.SetSensitivity(normalSensitivity);
//            playerMovement.SetRotateOnMove(true);
//            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
//        }

//        if (starterAssetsInputs.shoot)
//        {

//            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
//            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
//            starterAssetsInputs.shoot = false;
//        }

//    }
//}
