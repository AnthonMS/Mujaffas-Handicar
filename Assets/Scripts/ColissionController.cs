using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionController : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;
    private GameObject audioObject;
    private AudioSource audioSrc;
    private AudioController audioCtrl;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        audioObject = GameObject.FindGameObjectWithTag("AudioManager");
        //audioSrc = audioObject.GetComponent<AudioSource>();
        audioCtrl = audioObject.GetComponent<AudioController>();

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //gameObject.SendMessage("PrintBoolVals");
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Car")
        {
            if (!playerStats.isBoosting)
            {
                if (playerStats.isShielded)
                    audioCtrl.PlayCrashShielded();
                else
                    audioCtrl.PlayCrash();
                float damage = collision.gameObject.GetComponent<CarMotor>().damage;
                gameObject.SendMessage("TakeDamage", damage);
                gameObject.SendMessage("RetractScore", collision.gameObject.GetComponent<CarMotor>().retractScore);
            }
            else
            {
                audioCtrl.PlayExplosion();
                Destroy(collision.gameObject);
                GameObject explosionInstance = Instantiate(Resources.Load("Explosion_Particles", typeof(GameObject))) as GameObject;
                explosionInstance.transform.position = collision.transform.position;
                //explosionInstance.transform.parent = collision.transform;
                gameObject.SendMessage("AddScore", collision.gameObject.GetComponent<CarMotor>().retractScore);
            }
            
        }
        else if (collision.gameObject.tag == "Kenny")
        {
            audioCtrl.PlaySplat(); // This plays a splat sound that also calls HeKilledKenny sound 0.2 seconds after
            Destroy(collision.gameObject);
            float damage = collision.gameObject.GetComponent<KennyMotor>().damage;
            gameObject.SendMessage("TakeDamage", damage);
            gameObject.SendMessage("RetractScore", collision.gameObject.GetComponent<KennyMotor>().retractScore);

            SpawnKyleAndStan();
        }
        else if (collision.gameObject.tag == "Booster")
        {
            audioCtrl.PlayRocketThrust();
            gameObject.SendMessage("SetBoosting", true);
        }
        else if (collision.gameObject.tag == "Shield_Pickup")
        {
            audioCtrl.PlayShieldPickup();
            gameObject.SendMessage("StartShield");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Repair")
        {
            audioCtrl.PlayRepairPickup();
            gameObject.SendMessage("GiveHealth", 25f);
            Destroy(collision.gameObject);
        }
    }

    private void SpawnKyleAndStan()
    {
        GameObject kyleAndStanInstance = Instantiate(Resources.Load("Kyle_And_Stan", typeof(GameObject))) as GameObject;
        //kyleAndStanInstance.transform.Translate(GetRandomLane());
        //kyleAndStanInstance.transform.parent = transform;
    }
}
