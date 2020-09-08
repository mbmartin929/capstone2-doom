using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkDisplay : MonoBehaviour
{

    public Perk perk;
    public Text perkName;
    public Text description;
    public Image skillIcon;
    public Text perkgoldNeeded;
    public Text perkAttribute;
    public Text perkAttibiteAmount;

    [SerializeField]
    private UnitController playerHandler;
    // Start is called before the first frame update
    void Start()
    {
        playerHandler = this.GetComponent<PlayerPerksHandler>().Player;
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
