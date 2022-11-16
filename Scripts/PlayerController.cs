using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool isPowerup = false;
    public float powerUpStrength = 15.0f;
    
    //private GameObject player
    public GameObject powerupIndicator;

    //Change Material
    public Material[] material;
    public int m;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        
        //for the player
        rb = GetComponent <Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");

        //for the material
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[m];
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f,0);

        rend.sharedMaterial = material[m];
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            isPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            Debug.Log("Power Up!");
            StartCoroutine(PowerUpCountDown());
        }
   }

   //Call powerup countdown 
   //the powerup ability should disable after 7 seconds
   IEnumerator PowerUpCountDown()
   {
       yield return new WaitForSeconds(7);
       isPowerup = false;
       powerupIndicator.gameObject.SetActive(false);
   }

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.CompareTag("Enemy") && isPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

           Debug.Log("Collided with" + collision.gameObject.name + "with powerup set to" + isPowerup);

           enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
           
           
           //increase player size and kill the enemy
           Vector3 newKill = transform.localScale;    
           transform.localScale += new Vector3 (0.5f,0.5f,0.5f); 
           Destroy(collision.gameObject);
        }
    }

    //method for next material
    public void NextMaterial()
    {
        if (m<2)
        {
            m++;
        }

        else 
        {
            m=0;
        }
    }

   
    }