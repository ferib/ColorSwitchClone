using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{

    public float JumpForce = 10.0f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Text ScreenText;
    public Camera FollowCamera;
    public Color colorCyan;
    public Color colorYellow;
    public Color colorPink;
    public Color colorMagenta;
    

    private ObjColor PlayerColor;
    private Color[] colorArray;
    private bool PlayerAlive = false;
    private bool PlayerReady = true;
    private DateTime lastSwitch = DateTime.MinValue;

    void Start()
    {
        rb.simulated = false; //Player won't start falling yet
        colorArray = new Color[] { colorCyan, colorYellow, colorPink, colorMagenta };
        SetRandomColor();
    }

    // Update is called once per frame
    void Update()
    {

        //Update Player input
        if(PlayerReady)
        {
            //handle Player Controls
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector2.up * JumpForce;
                rb.simulated = true;
                PlayerAlive = true;
            }
        }

        //Kill player when 
        if (transform.position.y +5 < FollowCamera.transform.position.y && PlayerAlive)
            Reset();

        //Update Screen text
        ScreenText.enabled = !PlayerAlive;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == PlayerColor.ToString())
        {
            SetRandomColor();
            Debug.Log("nailed it");
        }
        else
        {
            if (lastSwitch.AddMilliseconds(500) < DateTime.Now)
                Reset();
            lastSwitch = DateTime.Now;
        }
    }

    void SetRandomColor()
    {
        ObjColor newColor = ObjColor.None; 
        while(newColor == ObjColor.None || newColor == PlayerColor) //its not exactly "random" since color should always change (noticable change ;) )
            newColor = (ObjColor)UnityEngine.Random.Range(0, 4); 

        PlayerColor = newColor;
        sr.color = colorArray[(int)PlayerColor];
    }

    void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload
        PlayerAlive = false;
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
