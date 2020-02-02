using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class number : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites;
    public SpriteRenderer sp;
    int index = 0;

    void Start()
    {
        // Check your sprites and sp
        InvokeRepeating("SwapSprite", 1.0f, 1.0f); // Start timer to swap each second
        sp.sprite = sprites[index]; // Set initial sprite
    }

    private void SwapSprite()
    {
        if (index == sprites.Length) // Increase the index and check we run out of sprites
        {
            CancelInvoke();
            sp.enabled = false; // Remove the counter 
            this.enabled = false; // That scripts is no more useful (could be destroyed)
            return;
        }
        sp.sprite = sprites[index]; // Set new sprite
    }
}