using UnityEngine;
using UnityEngine.EventSystems;
using Crosstales.RTVoice.Tool;
public class ObjectOnClick : MonoBehaviour, IPointerDownHandler
{
    //�������������갴�����ɿ����ڸ�������ʱ���������º���
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
        //��Ҫ�����Ĵ���
        Debug.Log(gameObject.name + "    sddf");
    }

    //����⵽����ڸ��������С����¡�����ʱ���������º���
    public void OnPointerDown(PointerEventData eventData)
    {
        //��Ҫ�����Ĵ���
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

