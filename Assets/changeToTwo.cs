using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class changeToTwo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartNewScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
    public void store() {
        string res = PlayerPrefs.GetString("3");
        PlayerPrefs.SetString("3", res);

    }
    public void endgame() {
        Application.Quit();
    }
}
