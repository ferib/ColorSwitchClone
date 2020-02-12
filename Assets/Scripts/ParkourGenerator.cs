using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourGenerator : MonoBehaviour
{

    public Camera MainCamera;
    public GameObject PrefabCircle;

    private List<GameObject> PrefabCircles;
    private float CirclesPerMeter = 8.0f; //screen is 10.0f
    private int LastCircleCount = 0;

    void Start()
    {
        PrefabCircles = new List<GameObject>();
    }


    void Update()
    {
        //Subtract 12.0f or main stuff
        int CicleCount = (int)((MainCamera.transform.position.y + 5.0f ) / CirclesPerMeter);
        Debug.Log("Cicles needed: " + ((MainCamera.transform.position.y + 5.0f) / CirclesPerMeter));

        if(CicleCount > PrefabCircles.Count)
        {
            //Add circle
            GameObject pCircle = Instantiate(PrefabCircle) as GameObject;
            pCircle.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y + 10.0f, 0);

            //random scale
            float scaleFactor = Random.Range(100, 150) / 100.0f;
            pCircle.transform.localScale = new Vector3(scaleFactor, scaleFactor, 0f);

            pCircle.SetActive(true);
            PrefabCircles.Add(pCircle);
        }

        //Start removing circles
        if (PrefabCircles.Count > LastCircleCount)
        {
            LastCircleCount++; //inc by 1
            if (PrefabCircles.Count >= 3) //Remove circles when no longer needed
                Destroy(PrefabCircles[PrefabCircles.Count - 4].gameObject);
        }
    }
}
