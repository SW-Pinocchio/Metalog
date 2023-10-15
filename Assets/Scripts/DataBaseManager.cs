using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataBaseManager : MonoBehaviour
{
    public string addFurnitureURL = "http://localhost/Metalog/addFurniture.php?";
    public string loadFurnitureURL = "http://localhost/Metalog/loadFurniture.php";
    public string deleteFunitureURL = "http://localhost/Metalog/deleteFurniture.php";
    public string addPostURL = "http://localhost/Metalog/addPost.php";
    public string loadPostURL = "http://localhost/Metalog/loadPost.php";
    public string deletePostURL = "http://localhost/Metalog/deletePost.php";
    private ObjectManager objectManager;

    public static List<FurnitureData> fDataList;

    public static List<PostData> pDataList;

    [SerializeField]
    private GameObject[] furniturePrefab;
    [SerializeField]
    private GameObject Parent;

    [SerializeField]
    GameObject UICanvas;
    [SerializeField]
    InputField title_I;
    [SerializeField]
    InputField content_I;
    [SerializeField]
    Text title_O;
    [SerializeField]
    Text content_O;

    [System.Serializable]
    public class FurnitureData
    {
        public int userID;
        public int objNum;
        public int objType;
        public float posX;
        public float posY;
        public float posZ;
        public float rot;
    }
    
    [System.Serializable]
    public class FurnitureDataList
    {
        public List<FurnitureData> furnitureList;
    }

    [System.Serializable]
    public class PostData
    {
        public int userID;
        public int objID;
        public string title;
        public string content;
    }
    
    [System.Serializable]
    public class PostList
    {
        public List<PostData> postList;
    }

    private void Awake()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        Debug.Log(PlayerPrefs.GetInt("UserID"));
    }

    public void AddFurniture(GameObject obj, int UserID, int ObjID, int objectType)
    {
        // obj에서 필요한 정보 추출
        int userID = UserID;
        int objNum = ObjID;
        int objType = objectType;
        float furniturePositionX = obj.transform.position.x;
        float furniturePositionY = obj.transform.position.y;
        float furniturePositionZ = obj.transform.position.z;
        float furnitureRotation = obj.transform.rotation.y;

        // PHP로 데이터 전송
        StartCoroutine(SendFurnitureDataToPHP(userID, objNum, objType, furniturePositionX, furniturePositionY, furniturePositionZ, furnitureRotation));
    }

    public void AddPost(int uid, int oid, string tt, string ct)
    {
        StartCoroutine(SendPostData(uid, oid, tt, ct));
    }

    public void LoadDataFromPHP()
    {
        StartCoroutine(LoadDataCoroutine());
    }

    public void LoadPost(int uid, int oid)
    {
        StartCoroutine(LoadPostCoroutine(uid, oid));
    }
    public void DeleteDataForUserID(int userID)
    {
        StartCoroutine(DeleteDataCoroutine(userID));
    }

    public void DeletePost(int uid, int oid)
    {
        StartCoroutine(DeletePostCoroutine(uid, oid));
        StartCoroutine(LoadPostCoroutine(uid, oid));
    }
    IEnumerator SendFurnitureDataToPHP(int userID, int objNum, int objType, float posX, float posY, float posZ, float rot)
    {
        WWWForm form = new WWWForm();
        string GUserID = "userID=" + userID.ToString() + "&";
        string GobjNum = "objNum=" + objNum.ToString() + "&";
        string GobjType = "objType=" + objType.ToString() + "&";
        string GposX = "posX=" + posX.ToString() + "&";
        string GposY = "posY=" + posY.ToString() + "&";
        string GposZ = "posZ=" + posZ.ToString() + "&";
        string Grot = "rot=" + rot.ToString();

        UnityWebRequest www = UnityWebRequest.Get(addFurnitureURL + GUserID + GobjNum + GobjType + GposX + GposY + GposZ + Grot);
        yield return www.SendWebRequest();
    }

    IEnumerator SendPostData(int user_ID, int object_ID, string title, string content)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", user_ID);
        form.AddField("objID", object_ID);
        form.AddField("title", title);
        form.AddField("content", content);

        UnityWebRequest www = UnityWebRequest.Post(addPostURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Post request successful");
            title_I.text = "";
            content_I.text = "";
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    IEnumerator LoadDataCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(loadFurnitureURL))
        {
            yield return www.SendWebRequest();

            string jsonData = www.downloadHandler.text;
            FurnitureDataList furnitureDataList = JsonUtility.FromJson<FurnitureDataList>("{\"furnitureList\":" + jsonData + "}");

            // 데이터를 리스트에 저장
            fDataList = furnitureDataList.furnitureList;

            foreach (FurnitureData data in fDataList)
            {
                if (data.userID == PlayerPrefs.GetInt("UserID"))
                {
                    Vector3 spawnPosition = new Vector3(data.posX, data.posY, data.posZ);
                    Quaternion spawnRotation = Quaternion.Euler(0f, data.rot, 0f);
                    GameObject furnitureObject = Instantiate(furniturePrefab[data.objType], spawnPosition, spawnRotation);
                    furnitureObject.GetComponent<ObjectPost>().SetID(data.objNum);
                    furnitureObject.transform.SetParent(Parent.transform);
                }
            }
        }
    }

    IEnumerator LoadPostCoroutine(int uid, int oid)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(loadPostURL))
        {
            yield return www.SendWebRequest();

            bool ch = false;
            string jsonData = www.downloadHandler.text;
            PostList postDataList = JsonUtility.FromJson<PostList>("{\"postList\":" + jsonData + "}");
            foreach (PostData postData in postDataList.postList)
            {
                if (postData.userID == uid && postData.objID == oid)
                {
                    ch = true;
                    title_O.text = postData.title.ToString();
                    content_O.text = postData.content.ToString();
                }
            }
            if(!ch)
            {
                title_O.text = "";
                content_O.text = "";
            }
        }
    }

    IEnumerator DeleteDataCoroutine(int userID)
    {
        string encodedUserID = UnityWebRequest.EscapeURL(userID.ToString());
        string url = deleteFunitureURL + "?userID=" + encodedUserID;

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
        }
    }

    IEnumerator DeletePostCoroutine(int userID, int objID)
    {
        string encodedUserID = UnityWebRequest.EscapeURL(userID.ToString());
        string url = deletePostURL + "?userID=" + encodedUserID + "&objID=" + objID;

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
        }
    }
}