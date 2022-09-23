using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class adddatas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void collectdata(string text) {
        int number = PlayerPrefs.GetInt("numbers") - 5;
        string res = (number + 4).ToString();
        PlayerPrefs.SetString(res, text);
        Debug.Log("voice  " + PlayerPrefs.GetString(res) + "  " + res);
    }
    private void Awake()
    {
        var input = gameObject.GetComponent<InputField>();
        input.onEndEdit.AddListener(collectdata);
    }
}
