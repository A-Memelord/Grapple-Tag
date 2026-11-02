using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnPreset : MonoBehaviour
{

    public List<GameObject> MapList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        SpawnMapPresetFromFolder();
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }



    public void SpawnMapPresetFromFolder()
    {
        if (MapList == null || MapList.Count == 0) return;

        int randIndex = UnityEngine.Random.Range(0, MapList.Count);
        GameObject selectedPrefab = MapList[randIndex];



        Instantiate(selectedPrefab, selectedPrefab.transform.position, selectedPrefab.transform.rotation);



    }


    
}
