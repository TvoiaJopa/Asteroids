using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D rb2D;
    [SerializeField] private float thrust = 15f;
    [SerializeField] private float lifeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().position + new Vector2(gameObject.transform.up.x, gameObject.transform.up.y) * Time.fixedDeltaTime * thrust);

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


}
