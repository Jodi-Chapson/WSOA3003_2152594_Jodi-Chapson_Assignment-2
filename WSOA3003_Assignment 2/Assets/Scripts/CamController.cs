using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform player;
    public float Lerp;
    
    public Vector3 CamPos;
    public float CamY;

    public bool CamFollow;


    private void Start()
    {
        CamPos = transform.position;
        CamFollow = true;

    }

    private void Update()
    {



        if (CamFollow)
        {
            CamPos = Vector3.Lerp(CamPos, player.position, Lerp);
            CamPos = new Vector3(CamPos.x, CamPos.y + CamY, -15);
            //CamPos.x = Mathf.Clamp(CamPos.x, -5, 34);
            //CamPos.y = Mathf.Clamp(CamPos.y, -2, 7);

            transform.position = CamPos;
        }




        


    }

    
    
}
