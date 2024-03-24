using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turningSpeed;
    public float turnigOverRoad;
    public float overRoadScale;
    public float acceleration;

    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;//���콺 ��ġ
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���콺 ��ġ�� ���� ��ġ�� ��ȯ
        Vector3 objectPosition = transform.position;//�ڽ��� ��ġ �ޱ�

        float directionX = (objectPosition.x - mPos.x); //x���� ��ġ���� �ޱ�
        float directionY = mPos.y - objectPosition.y; //y���� ��ġ���� �ޱ�


        float rotateDegree = Mathf.Atan2(directionX, directionY) * Mathf.Rad2Deg;
        //y�� x�� �� ��ũź��Ʈ�� ��ȯ => ���� ���� ��ȯ�ϹǷ� ���������� ��ȯ

        //transform.rotation = Quaternion.AngleAxis(rotateDegree, Vector3.forward); ���� ���ư�. �� ���� �ڵ�. �ؿ��� ���ô�. ���� ����� �ʹٸ� ��õ.

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotateDegree); //���Ϸ� ���� �޴´�.(�߿�)
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed * turnigOverRoad);
        //���� �������� ��ǥ �������� ȸ���ӵ���ŭ�� �ӵ��� ȸ���Ѵ�.

        Debug.Log(rotateDegree);

        if (Input.GetKey(KeyCode.C))
        {
            turnigOverRoad = overRoadScale;
        }
        else
        {
            turnigOverRoad = 1;
        }
        //��������߰�
    }
}
