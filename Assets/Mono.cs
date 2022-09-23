using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Crosstales.RTVoice.Tool;

public class Mono : MonoBehaviour
{
    string GetGps = "";
    private  List<string[]> table ;
    public Text ShowGPS;
    public Text showname;
    public GameObject player;
    public GameObject news;
    public int number=1;
    public int h=1;
    public GameObject ob;
    public Button btn;
    public Button btn2;
    public int i = 0;
    public Button btn3;
    [SerializeField]

    private SpeechText speechText;

    /// <summary>
    /// ��ʼ��һ��λ��
    /// </summary>
    void Start()
    {
        showname.text = "�˺ţ� " + PlayerPrefs.GetString("3");
        
        table = new List<string[]>();
        h = PlayerPrefs.GetInt("numbers");
        btn.onClick.AddListener(updateGps);
        btn2.onClick.AddListener(endGame);
        btn3.onClick.AddListener(alld);
        StartCoroutine(StartGPS());
        GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
        GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;
       
      //  PlayerPrefs.SetFloat("1",player.transform.position.x);
      //  PlayerPrefs.SetFloat("2", player.transform.position.z);

        Debug.Log(PlayerPrefs.GetFloat("1"));
        Debug.Log(PlayerPrefs.GetFloat("2"));
        int[] result = { 1, 2 };
        
        ShowGPS.text = GetGps;
        Debug.Log(GetGps);
    }
    public void Update()
    {
        
        
    }
    public void alld() {
        string res = PlayerPrefs.GetString("3");
        PlayerPrefs.DeleteAll();
        Debug.Log("��ȫ��ɾ��");
        PlayerPrefs.SetString("3", res);
        h = 1;
    
    
    }
    public void endGame() {
        int k = 0;
        string key = k.ToString();
        while (PlayerPrefs.HasKey(key))
        {
            string key2 = (k + 2).ToString();
            
                PlayerPrefs.SetInt(key2, 0);
                
            k += 5;
            key = k.ToString();

        }
        Application.Quit();
    
    
    }
    /// <summary>
    /// ˢ��λ����Ϣ����ť�ĵ���¼���
    /// </summary>
    public void updateGps()
    {

        StartCoroutine(StartGPS());
        GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
        GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;
        ShowGPS.text = GetGps;
        Debug.Log("hhhhhh   " + h);
        string result1 = h.ToString();
        string result2 = (h + 1).ToString();
        string result3 = (h + 2).ToString();
        string[] result = { result1, result2,0.ToString() };
        table.Add(result);
        Debug.Log(result1);
        Debug.Log(result2);
        Instantiate(news, new Vector3(player.transform.position.x, 0, player.transform.position.z), Quaternion.identity);
        news.GetComponent<ObjectOnClick>().speechText = speechText;
        // Debug.Log("ssss");
        int number = h;
            string namenew = number.ToString();
            GameObject.Find("candidate 1(Clone)").name = namenew;
        Debug.Log("namenew   " + namenew);
        PlayerPrefs.SetFloat(result1, player.transform.position.x);
        PlayerPrefs.SetFloat(result2, player.transform.position.z);
        PlayerPrefs.SetInt(result3, 1);
        PlayerPrefs.SetFloat((h+3).ToString(), Input.location.lastData.latitude);
        PlayerPrefs.SetFloat((h+4).ToString(), Input.location.lastData.longitude);
        
        h += 5;
        PlayerPrefs.SetInt("numbers", h);
        Debug.Log("h:" + "  " + h);
        Debug.Log("shulilang"+"  "+PlayerPrefs.GetInt("numbers"));
        Debug.Log(GetGps);
        Debug.Log(PlayerPrefs.GetFloat("1"));

        Debug.Log("shuliang   "+table.Count);
        int k = 0;
        string key = k.ToString();
        Debug.Log(PlayerPrefs.HasKey(key));
        while (PlayerPrefs.HasKey(key)) {
            string key2 = (k+2).ToString();
            Debug.Log("key2  " + key2 + " " + (PlayerPrefs.GetInt(key2)));
                if (PlayerPrefs.GetInt(key2)==0)
            {
                Debug.Log("chenggong");
                Instantiate(ob, new Vector3(PlayerPrefs.GetFloat(k.ToString()), 0, PlayerPrefs.GetFloat((k + 1).ToString())), Quaternion.identity);
                GameObject.Find("candidate 1(Clone)").name = k.ToString();
                Debug.Log(k + "  asfds");
                PlayerPrefs.SetInt(key2, 1);
                Debug.Log("number " + i + " ; " + "shengcheng");
                i++;
            }
            k += 5;
            key = k.ToString();
        
        }
     /*  for(int i = 0;i < table.Count; i++){
            if (table[i][2].Equals("0"))
            {
                Instantiate(ob, new Vector3(PlayerPrefs.GetFloat(k.ToString()), 0, PlayerPrefs.GetFloat((k + 1).ToString())), Quaternion.identity);
                table[i][2] = "1";
                Debug.Log("number "+i+" ; "+"shengcheng");

            }
            Debug.Log("number"+i+"  "+table[i][0] + PlayerPrefs.GetFloat(k.ToString())+"   "+table[i][1]+"  "+PlayerPrefs.GetFloat((k+1).ToString()));
            k += 2;
        }
     */



    }
    /// <summary>
    /// ֹͣˢ��λ�ã���ʡ�ֻ�������
    /// </summary>
    void StopGPS()
    {   
        Input.location.Stop();
    }

    IEnumerator StartGPS()
    {
        // Input.location ���ڷ����豸��λ�����ԣ��ֳ��豸��, ��̬��LocationServiceλ��  
        // LocationService.isEnabledByUser �û�������Ķ�λ�����Ƿ�����  
        if (!Input.location.isEnabledByUser)
        {
            GetGps = "isEnabledByUser value is:" + Input.location.isEnabledByUser.ToString() + " Please turn on the GPS";
            yield return false;
        }

        // LocationService.Start() ����λ�÷���ĸ���,���һ��λ������ᱻʹ��  
        Input.location.Start(10.0f, 10.0f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            // ��ͣЭͬ�����ִ��(1��)  
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            GetGps = "Init GPS service time out";
            yield return false;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GetGps = "Unable to determine device location";
            yield return false;
        }
        else
        {
            GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
            GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;
            yield return new WaitForSeconds(100);
        }
    }
}