using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform childTranform;

    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        childTranform.localPosition = new Vector3(0, 2, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30));
        childTranform.localRotation = Quaternion.Euler(new Vector3(0, 60, 0));
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        if (zInput > 0)
        {
            // 초당 1만큼 이동
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime); // 지역 공간
            //transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime, Space.Self); // 지역 공간
            //transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime, Space.World); // 전역 공간
        }
        else if (zInput < 0)
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
        }

        if (xInput > 0)
        { // 초당 180도 회전
            transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);

            childTranform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
        }
        else if (xInput < 0)
        {
            transform.Rotate(new Vector3(0, 0, -180) * Time.deltaTime);

            childTranform.Rotate(new Vector3(0, -180, 0) * Time.deltaTime);
        }
    }
}