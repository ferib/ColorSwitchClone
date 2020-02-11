using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public float JumpForce = 10.0f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Color colorCyan;
    public Color colorYellow;
    public Color colorPink;
    public Color colorMagenta;

    private ObjColor PlayerColor;
    private Color[] colorArray;

    void Start()
    {
        colorArray = new Color[] { colorCyan, colorYellow, colorPink, colorMagenta };
        SetRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == PlayerColor.ToString())
        {
            SetRandomColor();
            Debug.Log("nailed it");
        }
        else
            Debug.Log("ded");
    }

    void SetRandomColor()
    {
        ObjColor newColor = ObjColor.None; 
        while(newColor == ObjColor.None || newColor == PlayerColor) //its not exactly "random" since color should always change (noticable change ;) )
            newColor = (ObjColor)UnityEngine.Random.Range(0, 3); 

        PlayerColor = newColor;
        sr.color = colorArray[(int)PlayerColor];
        Debug.Log("Color set to " + colorArray[(int)PlayerColor].ToString());
    }

    //i prefer this :p
    enum ObjColor
    {
        Cyan = 0,
        Yellow = 1,
        Pink = 2,
        Magenta = 3,
        None = 4
    }
}
