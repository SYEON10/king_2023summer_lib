using static CamControl;

public class CamControl 
{
    public GameObject player; //����ٴ� ������Ʈ ���� 
    public float xmove = 0; //x ���� ������ ��
    public float ymove = 0; //y ���� ������ 
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
    xmove += Input.GetAxis("Mouse X"); //���콺�� �¿� �̵����� xmove�� ����
    ymove -= Input.GetAxis("Mouse Y"); //���콺�� ���� �̵����� ymove�� ����
    transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0); //�̵����� ���� ī�޶� �ٶ󺸴� ���� ���� , �巡�� �ӵ��� ������ *2���� 

}

switch (view)
        {
            case CameraView.player1:
                Vector3 reverseDistance = new Vector3(0.0f, 0.4f, 0.2f); // ī�޶� �ٶ󺸴� �չ����� Z ��, �̵����� ���� Z ������� ���͸� ���� 
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
