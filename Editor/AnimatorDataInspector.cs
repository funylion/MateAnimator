using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AnimatorData))]
public class AnimatorDataInspector : Editor {
    public override void OnInspectorGUI() {
        AnimatorData dat = target as AnimatorData;

        GUILayout.BeginVertical();

        string[] takeNames = dat.GenerateTakeNames();
        int[] takeInds = new int[takeNames.Length];
        for(int i = 0; i < takeNames.Length; i++)
            takeInds[i] = i;

        int playOnStartInd = dat.playOnStart != null ? System.Array.IndexOf(takeNames, dat.playOnStart.name) : 0;
        if(playOnStartInd == -1)
            playOnStartInd = 0;

        playOnStartInd = EditorGUILayout.IntPopup("Play On Start", playOnStartInd, takeNames, takeInds);

        if(playOnStartInd == 0)
            dat.playOnStart = null;
        else
            dat.playOnStart = dat.takes[playOnStartInd - 1];

        dat.sequenceLoadAll = GUILayout.Toggle(dat.sequenceLoadAll, "Build All Sequence On Start");
        dat.sequenceKillWhenDone = GUILayout.Toggle(dat.sequenceKillWhenDone, "Kill Sequence When Done");
        
        if(GUILayout.Button("Edit Timeline")) {
            AMTimeline timeline = AMTimeline.window;

            if(timeline != null) {
                timeline.aData = dat;
                timeline.Repaint();
            }
            else {
                EditorWindow.GetWindow(typeof(AMTimeline));
            }
        }

        GUILayout.EndVertical();
    }
}