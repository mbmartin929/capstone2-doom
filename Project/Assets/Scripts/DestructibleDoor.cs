﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class DestructibleDoor : MonoBehaviour
    {
        public int health = 30;
        private MeshDestroy meshDestroy;
        public int secretAmount = 0;

        // Start is called before the first frame update
        void Start()
        {
            EndGameScreen.Instance.totalSecrets += secretAmount;
            meshDestroy = transform.GetChild(1).GetComponent<MeshDestroy>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DestroyMesh()
        {
            meshDestroy.gameObject.SetActive(true);
            meshDestroy.transform.parent = null;
            meshDestroy.DestroyMesh();

            EndGameScreen.Instance.totalSecretsFound += secretAmount;

            Destroy(gameObject);
        }
    }
}