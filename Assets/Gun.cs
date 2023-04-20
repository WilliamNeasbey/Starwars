using UnityEngine;

public class Gun : MonoBehaviour
{
   // Audio source component
    public AudioSource source;
    //Accual sound
    public AudioClip laserSound;
    //The laser that gets shoot out
    public GameObject laser;
    //Where the laser is instansiated
    public Transform barrelLocation;

    //How fast we will fire when holding down the trigger. lower number is faster
    private float fireRate = 0.05f;
    //Timer to keep desired firerate.
    private float nextFire = 0.0F;

    //Speed of the lasers
    public float shootPoower = 4000;
    //Flag telling us if trigger is pulled.
    private bool triggerDown = false;
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
        Shoot();

    }

    private void CheckTrigger()
    {
        // if trigger is pulled set flag to true
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            triggerDown = true;
        }
        ///if trigger is let go set flag to false
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            triggerDown = false;
        }
    }


    private void Shoot()
    {

        //if trigger is down and time is bigger than nextFire, fire a laser
        if (triggerDown && Time.time > nextFire)
        {
            //set nextFire to Time plus firerate
            nextFire = Time.time + fireRate;
            //Instansiate a new laser at the barrellocation, in the barrel rotation and set laser rotation to Quaternion.Euler(90f, 0f, 0f). If your laser is in the wrong direction change those numbers around.
            //Then add force forward times the shoot poser.
            Instantiate(laser, barrelLocation.position, barrelLocation.rotation * Quaternion.Euler(90f, 0f, 0f)).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shootPoower);
            //Play the laser sound
            source.PlayOneShot(laserSound);
        }
    }
}
