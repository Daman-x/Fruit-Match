using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    private static Tracker instance = null;

    public static int tries = 2 ;
    public static Tracker Instance

    {

        get

        {
            if (instance == null)

            {

                instance = FindObjectOfType<Tracker>();

                if (instance == null)

                {

                    GameObject go = new GameObject();
                    go.name = "SingletonController";
                    instance = go.AddComponent<Tracker>();

                    DontDestroyOnLoad(go);

                }

            }


            return instance; 
        }

    }



    void Awake()

    {

        if (instance == null)

        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

        }

        else
        {
            Destroy(gameObject);

        }
    } 
}
