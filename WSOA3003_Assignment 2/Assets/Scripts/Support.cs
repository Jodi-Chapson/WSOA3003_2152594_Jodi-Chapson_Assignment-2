using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{
    
    public bool canfollow;
    public SpriteRenderer sprite;
    void Start()
    {
        
    }

    
    void Update()
    {



        if (canfollow)
        { sprite.enabled = true;
            canfollow = false;
        }

    }
}
