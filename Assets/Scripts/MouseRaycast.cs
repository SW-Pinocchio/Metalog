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
                // ���̰� �浹�� ��ü�� �±׸� Ȯ��
                string hitObjectTag = hit.collider.gameObject.tag;

                // Ư�� �±׿� �浹�� ��쿡�� �������� ����
                if (hitObjectTag == "Floor")
                {
                    // ���̰� �浹�� ������ ��ǥ�� ����
                    Vector3 spawnPosition = hit.point;

                    Debug.Log(spawnPosition);

                    // �������� �ش� ��ġ�� ����
                    //Instantiate(yourPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
