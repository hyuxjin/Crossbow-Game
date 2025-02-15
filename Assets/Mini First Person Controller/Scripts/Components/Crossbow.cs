using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public GameObject bulletPrefab;  // Assign your arrow prefab in the Inspector
    public Transform bulletSpawn;    // Assign an empty GameObject where arrows spawn
    public float bulletVelocity = 20f;  // Bullet velocity when fired
    public float bulletPrefabLifeTime = 3f;  // Time before the bullet is destroyed
    public Animator bowAnim;
    private Animator animator;  // Animator for firing animation
    private bool canShoot = true;  // Cooldown flag to prevent firing too quickly

    // Add these for sound effect
    public AudioClip shootSound;  // Sound to play when the weapon is fired
    private AudioSource audioSource;  // AudioSource to play the sound

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component from the crossbow
        animator = GetComponent<Animator>();

        // // Get the AudioSource component from the crossbow (make sure there is one in the scene)
        // audioSource = GetComponent<AudioSource>();
        
        // If there is no AudioSource, you can add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the mouse button is clicked and shooting is allowed
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            StartCoroutine(FireWeapon());
            animator.SetBool("Fire", true);
        }
        if (Input.GetMouseButtonUp(0) && canShoot)
        {
            StartCoroutine(FireWeapon());
        }
    }

    // Coroutine for firing the weapon with a cooldown
    private IEnumerator FireWeapon()
    {
        canShoot = false;  // Prevent firing immediately again

        // Wait for a short period before instantiating the bullet to match the animation timing
        yield return new WaitForSeconds(0.2f);  // Adjust timing if needed

        // Instantiate the bullet at the spawn point with the correct rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Get the Rigidbody component of the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;  // Prevents tunneling
            rb.interpolation = RigidbodyInterpolation.Interpolate;  // Smoother physics

            // Fire the bullet in the direction of the spawn point's forward direction
            Vector3 fireDirection = bulletSpawn.forward;
            rb.AddForce(fireDirection * bulletVelocity, ForceMode.Impulse);  // Add force to the bullet
        }

        // Play the shooting sound if assigned
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        animator.SetBool("Fire", false);

        // Destroy the bullet after a set time
        Destroy(bullet, bulletPrefabLifeTime);

        // Wait before allowing the player to shoot again (cooldown)
        yield return new WaitForSeconds(1f);  // Cooldown before firing again (adjust as needed)

        canShoot = true;  // Re-enable shooting
    }
}
