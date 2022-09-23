

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//使用 该引用，才能获得 Text 组件。

public class test : MonoBehaviour
{


    public GameObject text;
    public GameObject inputtext;

    // Use this for initialization
    void Start()
    {
        text.GetComponent<Text>().text = "Text";
        inputtext.GetComponent<InputField>().text = "InputText";
    }
    void Update()
    {

    }
}
