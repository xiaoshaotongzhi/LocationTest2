

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//ʹ�� �����ã����ܻ�� Text �����

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
