using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField]private Position pos;
    [SerializeField]private PositionSide posSide;
    [SerializeField] private Taggs objTagg;
    private enum Taggs { Ship, Asteroid}
    private enum Position { X, Y} 
    private enum PositionSide { Left, Right} 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void OnTriggerEnter2D(Collider2D other)
    {
        float a = 1.5f;

        if(posSide == PositionSide.Right)
        {
            a = -a;
        }

        if(objTagg == Taggs.Asteroid)
        {
            if (other.tag == "Asteroid")
            {
                if (pos == Position.X)
                {
                    other.transform.position = new Vector3(-other.transform.position.x + a*1.5f, other.transform.position.y, other.transform.position.z);

                }
                else
                {
                    other.transform.position = new Vector3(other.transform.position.x, -other.transform.position.y + a*1.5f, other.transform.position.z);

                }
            }
        }
        else if (objTagg == Taggs.Ship)
        {
            if (other.tag == "Player")
            {
                if (pos == Position.X)
                {
                    other.transform.position = new Vector3(-other.transform.position.x + a, other.transform.position.y, other.transform.position.z);

                }
                else
                {
                    other.transform.position = new Vector3(other.transform.position.x, -other.transform.position.y + a, other.transform.position.z);

                }
            }
        }

      
    }*/



    private void OnTriggerExit2D(Collider2D other)
    {


        if (objTagg == Taggs.Asteroid)
        {
            if (other.tag == "Asteroid" || other.tag == "Bullet")
            {
                other.transform.position = new Vector3(-other.transform.position.x, -other.transform.position.y, other.transform.position.z);
            }
        }
        else if (objTagg == Taggs.Ship)
        {
            if (other.tag == "Player" || other.tag == "Alien")
            {
                other.transform.position = new Vector3(-other.transform.position.x, -other.transform.position.y, other.transform.position.z);
            }
        }




    }
    
}
