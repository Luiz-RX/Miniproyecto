using UnityEngine;

public class Gun : Weapon
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

    private float nextFireTime = 0f;

    public override void Shoot()
    {
        if (Time.time >= nextFireTime && ammo > 0)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            ammo--;
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Update()
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
            
            mouseWorldPosition = raycastHit.point;
            //hitTransform = raycastHit.transform;
        }
        if (isAiming)
        {
            

            //playerMovement.SetRotateOnMove(false);
            //animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
    }
}
