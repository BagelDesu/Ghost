using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private int bulletMax = 0;
    private int curBullets = 20;
    private float reloadSpeed = 0f;

    [SerializeField]
    private Transform bulletOrigin= null;
    [SerializeField]
    private Projectile projectilePrefab = null;
    private Dictionary<int,Projectile> projectiles = new Dictionary<int, Projectile>();
    
    public void Shoot(InputAction.CallbackContext context)
    {
        
        if (context.action.WasPressedThisFrame() && curBullets > 0f)
        {
            Projectile spawnedProjectile = Instantiate(projectilePrefab, bulletOrigin.position, bulletOrigin.rotation);
            int key = GenerateBulletID();
            projectiles.Add(key, spawnedProjectile);
            spawnedProjectile.SetKey(key);
            spawnedProjectile.OnProjectileDestoryed.AddListener(
                delegate {
                RemoveBulletFromList(spawnedProjectile.GetKey()); 
            });
        }
    }

    // Add a way to reload the gun.

    //Generate a random key for us to use in the projectiles list
    private int GenerateBulletID()
    {
        int id = 0;
        while (projectiles.ContainsKey(id))
        {
            id = Random.Range(0, 999);
        }

        return id;
    }

    private void RemoveBulletFromList(int index)
    {
        projectiles.Remove(index);
    }
}
