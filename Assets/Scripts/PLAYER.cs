using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public static float VIDA = 100;
    void Update()
    {
        if(VIDA<=0){
            print("MURIO...");
        }
    }
}
