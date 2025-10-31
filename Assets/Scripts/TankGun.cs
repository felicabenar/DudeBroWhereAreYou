using Mirror;
using UnityEngine;

public class TankGun : NetworkBehaviour
{

    [SerializeField] Transform bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
            CmdShootBullet();
        }
    }

    void ShootBullet()
    {
        Transform bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.forward * bulletSpeed;

    }

    [Command]
    void CmdShootBullet()
    {
        RpcShootBullet();
    }

    [ClientRpc(includeOwner = false)]
    void RpcShootBullet()
    {
        ShootBullet();   
    } 
}


