using UnityEditor;


namespace Game.Enemy.Boss.Editor
{
    [CustomEditor(typeof(BossStateMachineBase))]
    public class BossStateMachineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            BossStateMachineBase stateMachine = (BossStateMachineBase)target;
            EditorGUILayout.Space(5f);
            EditorGUILayout.LabelField("CURRENT STATE:      "+ stateMachine.CurrentStateType.ToString());
        }
    }
}