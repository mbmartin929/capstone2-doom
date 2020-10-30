using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDirection : MonoBehaviour
{
    public int colCount = 4;
    public int rowCount = 4;
    public int rowNumber = 0; //Zero Indexed
    public int colNumber = 0; //Zero Indexed
    public int totalCells = 4;
    public int fps = 10;

    int[] animIndex = { 0, 1, 2, 3, 4, 5, 6, 7 };

    int index;
    int uIndex;
    int vIndex;

    public Vector2 size;
    Vector2 offset;
    float offsetX;
    float offsetY;

    Vector3 playerPos;
    Vector3 spritePos;
    Vector2 spriteFacing;
    Vector2 playerToSprite;

    Transform player;
    Vector3 direction;
    float angle;

    public Renderer spriteObj;


    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        spritePos = transform.position;

        spriteFacing = new Vector2(transform.forward.x, transform.forward.y);
        playerToSprite = new Vector2(spritePos.x, spritePos.z) - new Vector2(playerPos.x, playerPos.z);

        direction = playerToSprite - spriteFacing;
        angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        //Set orientation and snimation set
        SetSpriteOrientation();
        SetSpriteAnimation(rowNumber, colNumber, totalCells, fps);
    }

    //Set sprite orientation
    void SetSpriteOrientation()
    {
        if (angle < 0) angle += 360;
        rowNumber = animIndex[(int)Mathf.Round(angle / 360f * colCount) % rowCount];
    }

    //SetSpriteAnimation
    void SetSpriteAnimation(int rowNumber, int colNumber, int totalCells, int fps)
    {

        // Calculate index
        index = (int)(Time.time * fps);
        // Repeat when exhausting all cells
        index = index % totalCells;

        // split into horizontal and vertical index
        uIndex = index % colCount;
        vIndex = index / colCount;

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        offsetX = (uIndex + colNumber) * size.x;
        offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
        offset = new Vector2(offsetX, offsetY);

        spriteObj.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
