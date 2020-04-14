using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_findplayer : MonoBehaviour
{
    #region Singleton

    public static scr_findplayer instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

}
