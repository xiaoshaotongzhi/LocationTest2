using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{

	void Start()
	{
		InputField inputField = GetComponent<InputField>();
		inputField.onValueChanged.AddListener(OnValueChanged);
		inputField.onEndEdit.AddListener(OnEndEdit);
	}

	public void OnValueChanged(string text)
	{
		Debug.Log("OnValueChanged, text=" + text);
	}

	public void OnEndEdit(string text)
	{
		Debug.Log("OnEndEdit, text=" + text);
	}

}
