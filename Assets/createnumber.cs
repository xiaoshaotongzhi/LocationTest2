using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class createnumber : MonoBehaviour
{
    public Text showname;
    // Start is called before the first frame update
    void Start()
    {
        showname.text = PlayerPrefs.GetString("3");
    }
    void collectdata(string text) {

        PlayerPrefs.SetString("3", text);
        showname.text = PlayerPrefs.GetString("3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        var input = gameObject.GetComponent<InputField>();
        input.onEndEdit.AddListener(collectdata);
    }
}
