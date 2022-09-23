using UnityEngine; using System.Collections; 
public class CameraFollow : MonoBehaviour
{public Transform character; //�����Ҫ���������

public float smoothTime = 0.01f; //�����ƽ���ƶ���ʱ��

private Vector3 cameraVelocity = Vector3.zero; private Camera mainCamera; //�������(��ʱ����ڹ������ж�������������ֻ����һ�����������)

void Awake()

{

    mainCamera = Camera.main;

}
void Update()

{

    transform.position = Vector3.SmoothDamp(transform.position, character.position + new Vector3(0, 200, 0), ref cameraVelocity, smoothTime);

}

}


