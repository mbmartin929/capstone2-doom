using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class PlayerPerksHandler : MonoBehaviour
    {

        public UnitController Player;
        [SerializeField]

        private Canvas canvas;
        private bool seeCanvas = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("tab"))
            {
                if (canvas)
                {

                    seeCanvas = !seeCanvas;
                    canvas.gameObject.SetActive(seeCanvas);
                    isMouseOverUi();
                }

            }
        }
        private void isMouseOverUi()
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
