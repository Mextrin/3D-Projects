using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public bool infiniteAmmo;
    public float shotsPerSec; //Rate of fire
    float currentCooldown;
    public float ammoLoadedCapacity;
    public float ammoLoaded, ammoStorage;
    float shotRange = 100f;
    public float shotSpeed, shotDamage;
    PlayerMovement shooter;
    public GameObject bullet;
    public Transform spawnPoint;
    public Text ammoCurrentText, ammoStorageText;
    public bool shoot;
    Health enemyHealth;
    Camera playerCam;

    // Use this for initialization

    void Start()
    {
        if (infiniteAmmo)
        {
            ammoLoadedCapacity = 1;
            ammoLoaded = 1;
        }

        shooter = GetComponentInParent<PlayerMovement>();
        ammoLoaded = ammoLoadedCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //Assert.IsTrue(playerCam != null);
        if (ammoLoaded > 0 && shoot)
        {
            if (ammoLoaded > 0)
            {
                if (Time.time > currentCooldown)
                {
                    currentCooldown = Time.time + (60f / shotsPerSec) / 60f;

                    Quaternion shotDirection;

                    //Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.red, shotRange);
                    if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit))
                    {
                        shotDirection = Quaternion.LookRotation(hit.point - spawnPoint.position);
                    }
                    else
                    {
                        Vector3 screenCenter = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
                        Vector3 raycastEnd = screenCenter + playerCam.transform.forward * shotRange;
                        shotDirection = Quaternion.LookRotation(raycastEnd - spawnPoint.position);
                    }

                    GameObject copy = (GameObject)Instantiate(bullet, spawnPoint.position, shotDirection);
                    Projectile bulletScript = copy.GetComponent<Projectile>();

                    bulletScript.speed = shotSpeed;
                    bulletScript.damage = shotDamage;

                    //Debug.Log(hit.point + " / " + shotDirection.eulerAngles);
                  
                    //GameObject copy = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
                    //copy.transform.parent = transform;
                    if (!infiniteAmmo)
                    ammoLoaded--;
                }

            }
        }

        if (ammoLoaded == 0)
        {
            if (ammoStorage >= ammoLoadedCapacity)
            {
                ammoLoaded = ammoLoadedCapacity;
                ammoStorage -= ammoLoadedCapacity;
            }
            else
            {
                ammoLoaded = ammoStorage;
                ammoStorage = 0;
            }
        }
        if (ammoCurrentText || ammoStorageText)
        {
            ammoStorageText.text = ammoStorage.ToString();
            ammoCurrentText.text = ammoLoaded.ToString();
        }
    }
}
