using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourGenerator : MonoBehaviour
{

    public Camera MainCamera;
    public GameObject PrefabCircle;

    private List<GameObject> PrefabCircles;
    private float CirclesPerMeter = 4.0f; //screen height is 10.0f
    private int LastCircleCount = 0;
    private float AddOffset = 2.75f;
    private float CircleSize = 3.3f;

    void Start()
    {
        PrefabCircles = new List<GameObject>();
    }


    void Update()
    {
        int CicleCount = (int)((MainCamera.transform.position.y - AddOffset + 10.0f) / CirclesPerMeter);
        //Subtract AddOffset to correct the Camera Y, Add 10.0f to make circles spawn out of camera range
        
        //Debug.Log("Cicles needed: " + ((MainCamera.transform.position.y ) / CirclesPerMeter));

        if(CicleCount > PrefabCircles.Count)
        {
            //Add circle
            GameObject pCircle = Instantiate(PrefabCircle) as GameObject;
            
            //random scale
            float scaleFactor = Random.Range(115, 190) / 100.0f;
            pCircle.transform.localScale = new Vector3(scaleFactor, scaleFactor, 0f);

            if (CicleCount == 1)
                AddOffset += 1.15f * CircleSize * 0.5f; //Use the radios of the first circle to do math
            else
                AddOffset += scaleFactor* CircleSize *0.5f; //add current circle's radius for current math

            pCircle.transform.position = new Vector3(MainCamera.transform.position.x, (CicleCount * CirclesPerMeter) + 2.75f + AddOffset, 0); //2.75f is the Y location of the first circle
            AddOffset += scaleFactor * CircleSize * 0.5f; //add current circle's radius for next circle's math
            

            //Add to list so it can be destroyed lateron
            pCircle.SetActive(true);
            PrefabCircles.Add(pCircle);
        }

        //Start removing circles
        if (PrefabCircles.Count > LastCircleCount)
        {
            LastCircleCount++; //inc by 1
            if (PrefabCircles.Count >= 4) //Remove circles when no longer needed
                Destroy(PrefabCircles[PrefabCircles.Count - 4].gameObject);
        }
    }
}
