﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsPart : MonoBehaviour
{
    private float h;
    [SerializeField]private float timer = 1.5f;

    void Start()
    {
        h = Random.Range(-360, 360);
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, h);
    }

    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.GetComponent<Rigidbody2D>().position +
            new Vector2(gameObject.transform.up.x, gameObject.transform.up.y) * Time.fixedDeltaTime);
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a -= Time.deltaTime/4;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
