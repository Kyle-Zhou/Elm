using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    private GameObject player;
    public SpriteRenderer sr;

    private float duration = 0.1f, activation, alpha, alphaSet = 0.8f;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        alpha = alphaSet;
        sr.sprite = player.GetComponent<SpriteRenderer>().sprite;

        if (player.GetComponent<SpriteRenderer>().flipX)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        transform.position = player.transform.position;
        activation = Time.time;
    }

    private void Update()
    {
        alpha *= 0.85f;
        sr.color = new Color(1f, 1f, 1f, alpha);

        if (Time.time >= (activation + duration))
        {
            AfterimagePool.instance.AppendPool(gameObject);
        }
    }
}
