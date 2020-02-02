﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {
    private Cat cat;

    void Start() {
        cat = transform.parent.parent.GetComponent<Cat>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            cat.DetectPlayer(collision.gameObject);
        }
    }

}
