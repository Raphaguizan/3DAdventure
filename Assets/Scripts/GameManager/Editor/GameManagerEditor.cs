using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager myTarget = (GameManager)target;
        if (myTarget.StateMachine != null)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("State Machine");

            if (myTarget.StateMachine.CurrentState != null)
                EditorGUILayout.LabelField("current state : ", myTarget.StateMachine.CurrentState.ToString());

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
