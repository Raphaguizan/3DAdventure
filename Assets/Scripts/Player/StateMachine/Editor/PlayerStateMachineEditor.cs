using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Player.StateMachine
{
    [CustomEditor(typeof(PlayerStateMachine))]
    public class PlayerStateMachineEditor : Editor
    {
        bool showFoldout;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PlayerStateMachine myTarget = (PlayerStateMachine)target;
            if (myTarget.StateMachine != null)
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("State Machine");

                if (myTarget.StateMachine.CurrentState != null)
                    EditorGUILayout.LabelField("current state : ", string.Format("{0} :: {1}", myTarget.StateMachine.CurrentStateType, myTarget.StateMachine.CurrentState));

                EditorGUILayout.Space(5);
                showFoldout = EditorGUILayout.Foldout(showFoldout, "All States");
                if (showFoldout)
                {
                    var keys = myTarget.StateMachine.Dictionary.Keys.ToArray();
                    var vals = myTarget.StateMachine.Dictionary.Values.ToArray();

                    for (int i = 0; i < myTarget.StateMachine.Dictionary.Count; i++)
                    {
                        EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                    }
                }
            }
        }
    }
}

