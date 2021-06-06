using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField] private Taggs objTagg;
    private enum Taggs { Ship, Asteroid }


    private void OnTriggerExit2D(Collider2D other)
    {
        switch (objTagg)
        {
            case Taggs.Asteroid:
                if (other.tag == "Asteroid" || other.tag == "Bullet" || other.tag == "AlienBullet")
                {
                    other.transform.position = new Vector3(-other.transform.position.x,
                        -other.transform.position.y, other.transform.position.z);
                }
                break;
            case Taggs.Ship:
                if (other.tag == "Player" || other.tag == "Alien")
                {
                    other.transform.position = new Vector3(-other.transform.position.x,
                        -other.transform.position.y, other.transform.position.z);
                }
                break;
        }
 

    }
    
}
