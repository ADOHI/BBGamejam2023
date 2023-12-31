﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeLightStreamerSM : MonoBehaviour
{
    //if fixed light count, read
    public int maxLightsCount = 6; //if light count > 0 or < 0 (Infinite) then respect this count
    Object[] LightsList;
    public List<Light> LightList = new List<Light>();

    public Transform player;
    public bool disableOutOfViewLights = false;//v1.1.9c
    public bool includeInactive = false;//v1.1.9c
    // Start is called before the first frame update
    void Start()
    {
        //grab all scene lights
        LightsList = FindObjectsOfType(typeof(Light));
        for(int i=0;i < LightsList.Length; i++)
        {
            if (((Light)LightsList[i]).type != LightType.Directional)
            {
                if (includeInactive || (!includeInactive && ((Light)LightsList[i]).enabled))//v1.1.9c
                {
                    LightList.Add((Light)LightsList[i]);
                }
            }
        }
    }

    //https://answers.unity.com/questions/1286957/sorting-a-list-by-distance-to-an-object.html
    void Sort(List<Light> LightList)
    {
        LightList.Sort(delegate (Light a, Light b)
        {
            return Vector3.Distance(player.transform.position, a.transform.position).CompareTo(Vector3.Distance(player.transform.position, b.transform.position));
        });

        //foreach (Light point in LightList)
        //{
        //    Debug.Log(point.name);
        //}
    }

    public List<Light> LightListVISIBLE = new List<Light>();
    public float OufOfScreenOffset = 0.4f;//v1.1.9c
    // Update is called once per frame
    void Update()
    {
        LightListVISIBLE.Clear();
        for (int i = 0; i < LightList.Count; i++)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(LightList[i].transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x >= -OufOfScreenOffset && screenPoint.x <= 1.0f+ OufOfScreenOffset //v1.1.9c
                && screenPoint.y >= -OufOfScreenOffset && screenPoint.y <= 1.0f+ OufOfScreenOffset;
            if (onScreen) {
                LightListVISIBLE.Add(LightList[i]);
            }
        }

        //v1.1.9c
        if (disableOutOfViewLights)
        {
            for (int i = 0; i < LightList.Count; i++)
            {
                if (LightList[i].enabled)
                {
                    LightList[i].enabled = false;
                }
            }
        }

        //activate maxLightsCount closest lights, put rest to impostors if point lights, up to 32
        Sort(LightListVISIBLE);
        //rank lights and find closest IDs
        for (int i = 0; i < LightListVISIBLE.Count; i++)
        {
            if (i < maxLightsCount)
            {
                //enable lights
                LightListVISIBLE[i].enabled = true;
            }
            else
            {
                //disable lights
                LightListVISIBLE[i].enabled = false;
            }
        }

        
    }
}
