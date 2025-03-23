using System.Collections;
using UnityEngine;

public class Shooter : LockedCamPlayerMovement
{
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] Material idleFaceMaterial;
    [SerializeField] Material shootFaceMaterial;
    [SerializeField] GameObject peaBulletPrefab;
    [SerializeField] Transform firePos;

    //private float bulletForce = 2f;
    //private float shootDelay = 1f;
    //private bool canShoot = true;

    private IEnumerator Shoot()
    {
        // Face material is index 1
        skinnedMeshRenderer.materials[1] = shootFaceMaterial;
        Instantiate(peaBulletPrefab, firePos.position, firePos.rotation);
        yield return new WaitForSeconds(0.5f);
        skinnedMeshRenderer.materials[1] = idleFaceMaterial;
    }

    private new void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Shoot());
        }
    }
}
