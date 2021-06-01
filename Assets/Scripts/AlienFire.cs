using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFire : MonoBehaviour
{
    public Rigidbody2D rb2D;
    [SerializeField] private float thrust = 1.5f;
    [SerializeField] private float lifeTime = 0.1f;
    [SerializeField] private Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        //targetPosition = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        //gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().position + targetPosition * Time.fixedDeltaTime * thrust);

        float step = thrust * Time.deltaTime/2;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);


        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void SetTargetPosition(Vector2 pos)
    {
        targetPosition = pos;
    }

}
