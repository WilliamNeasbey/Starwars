using UnityEngine;

public class GunOneShot : MonoBehaviour
{
    // Audio source component
    public AudioSource source;
    //Actual sound
    public AudioClip laserSound;
    //The laser that gets shoot out
    public GameObject laser;
    //Where the laser is instansiated
    public Transform barrelLocation;

    //Speed of the lasers
    public float shootPower = 2000;
    //Flag telling us if trigger is pulled.
    private bool triggerDown = false;
    // Flag indicating if we've already fired the laser during this trigger press
    private bool hasFired = false;

    // Start is called before the first frame update
    void Start()
    {
        //If there is no barrel location added. Set barrel location to middel of the gameobject.
        if (barrelLocation == null)
            barrelLocation = transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrigger();
    }
    private void Shoot()
    {
        // Create a new laser instance at the barrelLocation
        GameObject newLaser = Instantiate(laser, barrelLocation.position, barrelLocation.rotation);

        // Add force to the new laser instance to shoot it forward
        Rigidbody laserRigidbody = newLaser.GetComponent<Rigidbody>();
        if (laserRigidbody != null)
        {
            laserRigidbody.AddForce(barrelLocation.forward * shootPower);
        }

        // Play the laser sound
        source.PlayOneShot(laserSound);

        // Set hasFired to true to prevent multiple shots during the same trigger press
        hasFired = true;
    }

    private void CheckTrigger()
    {
        // if trigger is pulled and was not already down, set flag to true and shoot
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && !triggerDown)
        {
            triggerDown = true;
            Shoot();
        }
        // if trigger is released, set flag to false
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            triggerDown = false;
        }
    }

}