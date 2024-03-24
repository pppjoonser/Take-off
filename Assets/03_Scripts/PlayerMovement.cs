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
        Vector3 mousePos = Input.mousePosition;//마우스 위치
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//마우스 위치를 월드 위치로 변환
        Vector3 objectPosition = transform.position;//자신의 위치 받기

        float directionX = (objectPosition.x - mPos.x); //x방향 위치차이 받기
        float directionY = mPos.y - objectPosition.y; //y방향 위치차이 받기


        float rotateDegree = Mathf.Atan2(directionX, directionY) * Mathf.Rad2Deg;
        //y값 x값 을 아크탄젠트로 변환 => 라디안 값을 반환하므로 각도값으로 변환

        //transform.rotation = Quaternion.AngleAxis(rotateDegree, Vector3.forward); 휙휙 돌아감. 맛 없는 코드. 밑에거 씁시다. 쉽게 만들고 싶다면 추천.

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotateDegree); //오일러 각을 받는다.(중요)
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed * turnigOverRoad);
        //현재 각도에서 목표 각도까지 회전속도만큼의 속도로 회전한다.

        Debug.Log(rotateDegree);

        if (Input.GetKey(KeyCode.C))
        {
            turnigOverRoad = overRoadScale;
        }
        else
        {
            turnigOverRoad = 1;
        }
        //변경사항추가
    }
}
