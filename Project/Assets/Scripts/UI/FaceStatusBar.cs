using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FaceStatusBar : MonoBehaviour
{
    public GameObject player;
    protected UnitController unit;
    Texture2D[] faces;
    Image randomImage;
    bool doEvilGrin;
    int pick;
    float timer;
    int i;


    public float muchPain;
    float previousHealth;
    [Header("HP 100 - 80")]
    public Texture2D HP80100FW;
    public Texture2D HP80100L;
    public Texture2D HP80100R;
    [Header("HP 79 - 60")]
    public Texture2D HP60100FW;
    public Texture2D HP60100L;
    public Texture2D HP60100R;
    [Header("HP 59 - 40")]
    public Texture2D HP40100FW;
    public Texture2D HP40100L;
    public Texture2D HP40100R;
    [Header("HP 39 - 20")]
    public Texture2D HP20100FW;
    public Texture2D HP20100L;
    public Texture2D HP20100R;
    [Header("HP 19 - 1")]
    public Texture2D HP1100FW;
    public Texture2D HP1100L;
    public Texture2D HP1100R;
    [Header("Picked up Weapon")]
    public Texture2D HP80100FWeaponPick;
    public Texture2D HP60100FWeaponPick;
    public Texture2D HP40100FWeaponPick;
    public Texture2D HP20100FWeaponPick;
    public Texture2D HP1100FWeaponPick;

   
    [Header("HP <= 0")]
    public Texture2D HPDead;
 


    //Randomize face 
    //Face follow Enemy when hit (indicator where the enemy is)
    //if 20% hp is deducted ouchFace
    //DO Evil grin when picked up a weapon
    // Start is called before the first frame update
    void Start()
    {

 
        unit = player.GetComponent<UnitController>();
        faces = new Texture2D[16];

        faces[0] = HP80100FW;
        faces[1] = HP80100L;
        faces[2] = HP80100R;
        
        faces[3] = HP60100FW; 
        faces[4] = HP60100L;
        faces[5] = HP60100R;

        faces[6] = HP40100FW;
        faces[7] = HP40100L;
        faces[8] = HP40100R;

        faces[9] = HP20100FW;
        faces[10] = HP20100L;
        faces[11] = HP20100R;

        faces[12] = HP1100FW;
        faces[13] = HP1100L;
        faces[14] = HP1100R;

        faces[15] = HPDead;


    }

    // Update is called once per frame
    void Update()
    {
       
  
        ChangeFace();
    
    }
    void Faces()
    {
        //Random faces when no enemy
    }

    

    void ChangeFace()
    {

        timer -= Time.deltaTime;    
        if (unit.CurHealth >= 80 && unit.CurHealth <= 100)
        {
            timer = 1f;
            pick = 0;
            Debug.Log(timer + "ADS");
            if (timer < 0)
            {
                timer = 1.5f;
                i = pick;
                while (i == pick)
                {
                    pick = Random.Range(0, 2);
                
                }

            }
        }
        if(unit.CurHealth >= 60 && unit.CurHealth <= 79)
        {
            ;
        }
        if(unit.CurHealth >= 40 && unit.CurHealth <= 59)
        {
            ;
        }
        if(unit.CurHealth >= 20 && unit.CurHealth <= 39)
        {
            ;
        }
        if(unit.CurHealth >= 1 && unit.CurHealth <= 19)
        {
            ;
        }
        if(unit.CurHealth <= 0)
        {
            ;
        }
    }

    //private void GUIDrawSprite(Rect rect, Sprite faces)
    //{

    //    Rect spriteRect = faces.rect;
    //    Texture2D tex = faces.texture;
      
    //    GUI.DrawTextureWithTexCoords(rect, tex, new Rect(spriteRect.x / tex.width, spriteRect.y / tex.height, spriteRect.width / tex.width, spriteRect.height / tex.height));
        
    //}


    void muchPainCalc()
    {
      if(previousHealth - unit.CurHealth > muchPain)
        {
            //shock face;
        }
    }

    void calcPainOffset()
    {
        int health = unit.CurHealth;

        
    }

}
