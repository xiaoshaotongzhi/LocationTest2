using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class MicroPhoneManager : MonoBehaviour
{
    public int DeviceLength;
    /// <summary>
    /// ¼��Ƶ��

    /// </summary>
    public string Frequency = "44100";
    public int Samplerate = 44100;
    /// <summary>
    /// ¼��ʱ��

    /// </summary>
    public int MicSecond = 2;
    string infoLog = "";
    AudioSource _curAudioSource;
    AudioSource CurAudioSource
    {
        get
        {
            if (_curAudioSource == null)
            {
                _curAudioSource = gameObject.AddComponent<AudioSource>();
            }
            return _curAudioSource;
        }
    }
    #region ��public Way��

    /// <summary>
    /// ��ȡ��˷��豸

    /// </summary>
    public void GetMicrophoneDevice()
    {
        string[] mDevice = Microphone.devices;
        DeviceLength = mDevice.Length;
        if (DeviceLength == 0)
            ShowInfoLog("�Ҳ�����˷��豸��");
    }
    /// <summary>
    /// ��ʼ¼��

    /// </summary>
    public void StartRecordAudio()
    {
        CurAudioSource.Stop();
        CurAudioSource.loop = false;
        CurAudioSource.mute = true;
        CurAudioSource.clip = Microphone.Start(null, true, MicSecond, int.Parse(Frequency));
        while (!(Microphone.GetPosition(null) > 0))
        {
        }
        CurAudioSource.Play();
        ShowInfoLog("��ʼ¼��.....");
    }
 /// <summary>
 /// ֹͣ¼��

 /// </summary> 
 public void StopRecordAudio()
 {
 ShowInfoLog("����¼��.....");
 if (!Microphone.IsRecording(null))
 return;
 Microphone.End(null);
 CurAudioSource.Stop();
 }
/// <summary>s
/// �ط�¼��

/// </summary>
public void PlayRecordAudio()
{
    if (Microphone.IsRecording(null))
        return;
    if (CurAudioSource.clip == null)
        return;
    CurAudioSource.mute = false;
    CurAudioSource.loop = false;
    CurAudioSource.Play();
    ShowInfoLog("����¼��.....");
}
/// <summary>
/// ��ӡ¼����Ϣ

/// </summary>
public void PrintRecordData()
{
    if (Microphone.IsRecording(null))
        return;
    byte[] data = GetClipData();
    #region �û����ɹ̶�¼��ʱ��

    int position = Microphone.GetPosition(null);
    var soundata = new float[CurAudioSource.clip.samples * CurAudioSource.clip.channels];
    CurAudioSource.clip.GetData(soundata, 0);
    var newdata = new float[position * CurAudioSource.clip.channels];
    for (int i = 0; i < newdata.Length; i++)
    {
        newdata[i] = soundata[i];
    }
    CurAudioSource.clip = AudioClip.Create(CurAudioSource.clip.name, position, CurAudioSource.clip.channels, CurAudioSource.clip.frequency, false);
    CurAudioSource.clip.SetData(newdata, 0);
    Microphone.End(null);
    #endregion
    using (FileStream fs = CreateEmpty(Application.persistentDataPath + "/dd.wav"))
    {
        ConvertAndWrite(fs, CurAudioSource.clip);
        WriteHeader(fs, CurAudioSource.clip);
    }
    string infoLog = "total length:" + data.Length + " time:" + CurAudioSource.time;
    ShowInfoLog(infoLog);
}
private void WriteHeader(FileStream stream, AudioClip clip)
{
    int hz = clip.frequency;
    int channels = clip.channels;
    int samples = clip.samples;
    stream.Seek(0, SeekOrigin.Begin);
    Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
    stream.Write(riff, 0, 4);
    Byte[] chunkSize = BitConverter.GetBytes(stream.Length - 8);
    stream.Write(chunkSize, 0, 4);
    Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
    stream.Write(wave, 0, 4);
    Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
    stream.Write(fmt, 0, 4);
    Byte[] subChunk1 = BitConverter.GetBytes(16);
    stream.Write(subChunk1, 0, 4);
    UInt16 two = 2;
    UInt16 one = 1;
    Byte[] audioFormat = BitConverter.GetBytes(one); stream.Write(audioFormat, 0, 2);
    Byte[] numChannels = BitConverter.GetBytes(channels);
    stream.Write(numChannels, 0, 2);
    Byte[] sampleRate = BitConverter.GetBytes(hz);
    stream.Write(sampleRate, 0, 4);
    Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2 
    stream.Write(byteRate, 0, 4);
    UInt16 blockAlign = (ushort)(channels * 2);
    stream.Write(BitConverter.GetBytes(blockAlign), 0, 2);
    UInt16 bps = 16;
    Byte[] bitsPerSample = BitConverter.GetBytes(bps);
    stream.Write(bitsPerSample, 0, 2);
    Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
    stream.Write(datastring, 0, 4);
    Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
    stream.Write(subChunk2, 0, 4);
}
private FileStream CreateEmpty(string filepath)
{
    FileStream fileStream = new FileStream(filepath, FileMode.Create);
    byte emptyByte = new byte();
    for (int i = 0; i < 44; i++) //preparing the header 
    {
        fileStream.WriteByte(emptyByte);
    }
    return fileStream;
}
private void ConvertAndWrite(FileStream fileStream, AudioClip clip)
{
    float[] samples = new float[clip.samples];
    clip.GetData(samples, 0);
    Int16[] intData = new Int16[samples.Length];
    Byte[] bytesData = new Byte[samples.Length * 2];
    int rescaleFactor = 32767; //to convert float to Int16 
    for (int i = 0; i < samples.Length; i++)
    {
        intData[i] = (short)(samples[i] * rescaleFactor);
        Byte[] byteArr = new Byte[2];
        byteArr = BitConverter.GetBytes(intData[i]);
        byteArr.CopyTo(bytesData, i * 2);
    }
    fileStream.Write(bytesData, 0, bytesData.Length);
}
/// <summary>
/// ��ȡ��Ƶ����

/// </summary>
/// <returns>The clip data.</returns>
public byte[] GetClipData()
{
    if (CurAudioSource.clip == null)
    {
        ShowInfoLog("ȱ����Ƶ��Դ��");
        return null;
    }
    float[] samples = new float[CurAudioSource.clip.samples];
    CurAudioSource.clip.GetData(samples, 0);
    byte[] outData = new byte[samples.Length * 2];
    int reScaleFactor = 32767;
    for (int i = 0; i < samples.Length; i++)
    {
        short tempShort = (short)(samples[i] * reScaleFactor);
        byte[] tempData = System.BitConverter.GetBytes(tempShort); outData[i * 2] = tempData[0];
        outData[i * 2 + 1] = tempData[1];
    }
    if (outData == null || outData.Length <= 0)
    {
        ShowInfoLog("��ȡ��Ƶ����ʧ�ܣ�");
        return null;
    }
    return outData;
}
#endregion
void OnGUI()
{
    if (DeviceLength == 0)
    {
        if (ShowGUIButton("��ȡ��˷��豸"))
        {
            GetMicrophoneDevice();
        }
    }
    else if (DeviceLength > 0)
    {
        GUILayout.Label("¼��Ƶ��:");
        Frequency = GUILayout.TextField(Frequency, GUILayout.Width(Screen.width / 5), GUILayout.Height(Screen.height / 20));
        GUILayout.BeginVertical();
        if (ShowGUIButton("��ʼ¼��"))
        {
            StartRecordAudio();
        }
        if (ShowGUIButton("����¼��"))
        {
            StopRecordAudio();
        }
        if (ShowGUIButton("�ط�¼��"))
        {
            PlayRecordAudio();
        }
        if (ShowGUIButton("��ȡ¼������"))
        {
            PrintRecordData();
        }
        GUILayout.EndVertical();
    }
    GUILayout.Label(infoLog);
}
#region [Private Way]
/// <summary>
/// ��ʾGUI ��ť

/// </summary>
/// <returns><c>true</c>, if GUI button was shown, <c>false</c> otherwise.</returns>
/// <param name="buttonName">Button name.</param>
bool ShowGUIButton(string buttonName)
{
    return GUILayout.Button(buttonName, GUILayout.Height(Screen.height / 20), GUILayout.Width(Screen.width / 5));
}
void ShowInfoLog(string info)
{
    infoLog += info;
    infoLog += "\r\n";
}
 #endregion
}
