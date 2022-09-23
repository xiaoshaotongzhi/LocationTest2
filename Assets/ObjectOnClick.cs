using UnityEngine;
using UnityEngine.EventSystems;
using Crosstales.RTVoice.Tool;
public class ObjectOnClick : MonoBehaviour, IPointerDownHandler
{
    //当鼠标点击，即鼠标按下与松开均在该物体上时，触发以下函数
    public string mytext;
    public SpeechText speechText;
    public void Start()
    {
        string Name = gameObject.name;
        int num = int.Parse(Name);

        num = num + 4;
        string res = PlayerPrefs.GetString(num.ToString());

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //你要触发的代码
        Debug.Log(gameObject.name + "    sddf");
    }

    //当检测到鼠标在该物体上有“按下”操作时，触发以下函数
    public void OnPointerDown(PointerEventData eventData)
    {
        //你要触发的代码
        Debug.Log(gameObject.name + "    sdfsdf");
        string Name = gameObject.name;
        int num = int.Parse(Name);
  
        num = num + 4;
        string res=PlayerPrefs.GetString(num.ToString());
        Debug.Log(num.ToString() + "    hhhh");
        if (speechText != null)
        {
            speechText.Text = res;
            speechText.Speak();
        }

        /*TextAsset puzzle = Resources.Load<TextAsset>("puzzle1");
        puzzle.text = null;
        puzzle.text = res;*/
        //string filePath = "D:\LocationTest2\Assets\puzzle1.txt"
    }
}

