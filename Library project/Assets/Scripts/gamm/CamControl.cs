using static CamControl;

public class CamControl 
{
    public GameObject player; //따라다닐 오브젝트 지정 
    public float xmove = 0; //x 누적 움직인 양
    public float ymove = 0; //y 누적 움직인 
    public float distance = 5;
    public float view = 1;
    public enum CameraView {player1, player3, upperSideCam, rigtSideCam, leftSideCam }


    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            view = (float)CameraView.player1;
        }
        if (Input.GetKeyDown("2"))
        {
            view = (float)CameraView.player3;
        }
        if (Input.GetKeyDown("3"))
        {
            view = (float)CameraView.upperSideCam;
        }
        if (Input.GetKeyDown("4"))
        {
            view = (float)CameraView.rightSideCam;
        }
        if (Input.GetKeyDown("5"))
        {
            view = (float)CameraView.leftSideCam;
        }

if (Input.GetMouseButton(0))
{
    xmove += Input.GetAxis("Mouse X"); //마우스의 좌우 이동량을 xmove에 누적
    ymove -= Input.GetAxis("Mouse Y"); //마우스의 상하 이동량을 ymove에 누적
    transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); //이동량에 따라 카메라가 바라보는 방향 조정 , 드래그 속도가 느려서 *2했음 

}

switch (view)
        {
            case CameraView.player1:
                Vector3 reverseDistance = new Vector3(0.0f, 0.4f, 0.2f); // 카메라가 바라보는 앞방향은 Z 축, 이동량에 따른 Z 축방향의 벡터를 구함 
                transform.position = player.transform.position + transform.rotation * reverseDistance;
                break;
            case CameraView.player3:
                Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance);
                transform.position = player.transform.position - transform.rotation * reverseDistance;
                break;
            case CameraView.upperSideCam:
                Vector3 upperSide = new Vector3(0.0f, 10, 0.0f);
                transform.position = player.transform.position + upperSide;
                transform.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case CameraView.rigtSideCam:
                Vector3 rightSide = new Vector3(10, 0.0f, 0.0f);
                transform.position = player.transform.position + rightSide;
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case CameraView.leftSideCam:
                Vector3 leftSide = new Vector3(-10, 0.0f, 0.0f);
                transform.position = player.transform.position + leftSide;
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
        }
}
}
