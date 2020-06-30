using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EightDirectionalSpriteSystem
{
    [CustomEditor(typeof(EnemyAI))]
    public class FOVEditor : Editor
    {
        void OnSceneGUI()
        {
            EnemyAI enemy = (EnemyAI)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.viewRadius);
            Vector3 viewAngleA = enemy.DirFromAngle(-enemy.viewAngle / 2, false);
            Vector3 viewAngleB = enemy.DirFromAngle(enemy.viewAngle / 2, false);

            Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleA * enemy.viewRadius);
            Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleB * enemy.viewRadius);

            Handles.color = Color.red;
            foreach (Transform visibleTarget in enemy.visibleTargets)
            {
                Handles.DrawLine(enemy.transform.position, visibleTarget.position);
            }
        }
    }
}
