
//=============================================================================
//  ActorFrame
//  by Mariusz Skowroński from Healthbar Games (http://healthbargames.pl)
//
//  This class describes single frame of EDSS animation.
//  It contains eight sprites (0 .. 7) - one for each of eight world directions.
//  For animations that has only one-direction sprites we use only first sprite
//  from this array.
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EightDirectionalSpriteSystem
{
    [System.Serializable]
    public class ActorFrame
    {
        [SerializeField]
        private Material[] materials;

        public ActorFrame()
        {
            materials = new Material[8];
        }

        public ActorFrame(ActorFrame other)
        {
            materials = new Material[8];
            for (int i = 0; i < 8; i++)
                materials[i] = other.materials[i];
        }

        public ActorFrame(Material[] materials)
        {
            this.materials = new Material[8];
            for (int i = 0; i < 8; i++)
                this.materials[i] = materials[i];
        }

        public ActorFrame(Material material)
        {
            this.materials = new Material[8];
            for (int i = 1; i < 8; i++)
                this.materials[i] = null;

            materials[0] = material;
        }

        public void SetSprite(int direction, Material material)
        {
            if (materials == null)
                return;

            materials[direction % 8] = material;
        }

        public Material GetSprite(int direction)
        {
            if (materials == null)
                return null;

            return materials[direction % 8];
        }
    }
}
