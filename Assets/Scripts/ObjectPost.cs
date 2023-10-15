using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class fPost
{
    public int fID;
    public int fNum;
}
public class ObjectPost : MonoBehaviour
{

    fPost Post;
    private void Awake()
    {
        Post = new fPost();
    }

    public int GetID()
    {
        return Post.fID;
    }

    public int GetNum()
    {
        return Post.fNum;
    }

    public void SetID(int n)
    {
        Post.fID = n;
    }

    public void AddNum()
    {
        Post.fNum++;
    }
}
