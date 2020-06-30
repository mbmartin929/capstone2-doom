using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FaceStatusBar : MonoBehaviour
{

    public GameObject player;
    protected UnitController unit;
    public Sprite[] faces;

    //Randomize face 
    //Face follow Enemy when hit (indicator where the enemy is)
    //if 20% hp is deducted ouchFace
    
    // Start is called before the first frame update
    void Start()
    {
        unit = player.GetComponent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Faces()
    {
        //Random faces when no enemy
    }
    private void OnGUI()
    {
        
    }
}
