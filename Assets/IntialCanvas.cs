using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntialCanvas : MonoBehaviour
{
    private int ScreenWidth;
    private int ScreenHeight;
    public GameObject ob;
    // Start is called before the first frame update
    void Start()
    {
        ScreenHeight = UnityEngine.Screen.height;
        Debug.Log(ScreenHeight);
        ob.GetComponent<Canvas>().scaleFactor = ScreenHeight / (float)600;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
