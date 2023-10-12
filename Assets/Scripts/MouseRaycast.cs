using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    [SerializeField]
    private Camera camDeco;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = camDeco.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 레이가 충돌한 객체의 태그를 확인
                string hitObjectTag = hit.collider.gameObject.tag;

                // 특정 태그와 충돌한 경우에만 프리팹을 생성
                if (hitObjectTag == "Floor")
                {
                    // 레이가 충돌한 지점의 좌표를 얻어옴
                    Vector3 spawnPosition = hit.point;

                    Debug.Log(spawnPosition);

                    // 프리팹을 해당 위치에 생성
                    //Instantiate(yourPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
