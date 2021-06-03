using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFire : MonoBehaviour
{
    public Rigidbody2D rb2D;
    [SerializeField] private float thrust = 1.5f;
    [SerializeField] private float lifeTime = 0.1f;
    [SerializeField] private Vector2 targetPosition;

    void Start()
    {
        //targetPosition = new Vector2(0, 0);
    }

    void Update()
    {
        float step = thrust * Time.deltaTime/2;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        //Timer for destroy fire
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
