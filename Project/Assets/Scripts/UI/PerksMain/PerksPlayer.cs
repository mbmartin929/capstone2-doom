using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PerksPlayer : MonoBehaviour
{
    private Image image;


    [SerializeField] private Text counterText;


    [SerializeField] private int maxCount;

    private int currCount;


    [SerializeField] private Text countText;


    [SerializeField] private bool unlocked;


    [SerializeField] private PerksPlayer childPerk;

    public int MyCurrentCount { get => currCount; set => currCount = value; }

    private void Awake()
    {
        //Commented out temporarily
        image = GetComponent<Image>();
        countText.text = $"{MyCurrentCount}/{maxCount}";
        if (unlocked)
        {
            unlockPerk();
        }
    }

    public virtual bool Click()
    {
        if (MyCurrentCount < maxCount && unlocked)
        {
            MyCurrentCount++;
            countText.text = $"{MyCurrentCount}/{maxCount}";

            if (MyCurrentCount == maxCount)
            {
                if (childPerk != null)
                {
                    childPerk.unlockPerk();
                }
            }
            return true;
        }

        return false;
    }
    // Start is called before the first frame update
    public void LockPerk()
    {
        image.color = Color.gray;
        counterText.color = Color.gray;
    }

    public void unlockPerk()
    {
        image.color = Color.white;
        counterText.color = Color.white;

        unlocked = true;

    }

}
