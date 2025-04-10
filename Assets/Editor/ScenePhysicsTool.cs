using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MysteryEditor
{
    public class EditorPhysicsSimulation : EditorWindow
    {
        bool isPlaying = false;

        private void OnEnable()
        {
            Undo.undoRedoEvent += UndoRedoEvent;
        }

        private void OnDisable()
        {
            Undo.undoRedoEvent -= UndoRedoEvent;
            Stop();
        }

        private void UndoRedoEvent(in UndoRedoInfo undo)
        {
            Stop();
        }

        private void OnGUI()
        {
            if (isPlaying == false)
            {
                if (GUILayout.Button("Play"))
                {
                    isPlaying = true;
                    RecordUndo();
                    EditorApplication.update += StepPhysics;
                }
            }
            else
            {
                if (GUILayout.Button("Stop")) Stop();
            }
        }

        void Stop()
        {
            isPlaying = false;
            EditorApplication.update -= StepPhysics;

            foreach (var rb in rigidbodies)
            {
                rb.angularVelocity = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
        }

        Rigidbody[] rigidbodies;

        void RecordUndo()
        {
            rigidbodies = GameObject.FindObjectsByType<Rigidbody>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            var transforms = rigidbodies.Select(x => (UnityEngine.Object)x.transform).ToArray();
            Undo.RecordObjects(transforms, "Simulate Physics");
        }

        private void StepPhysics()
        {
            var simMode = Physics.simulationMode;
            Physics.simulationMode = SimulationMode.Script;
            Physics.Simulate(Time.fixedDeltaTime);
            Physics.simulationMode = simMode;

            foreach (var rb in rigidbodies)
                EditorUtility.SetDirty(rb.transform);
        }

        [MenuItem("Tools/Skibidi Toilet")]
        private static void OpenWindow()
        {
            GetWindow<EditorPhysicsSimulation>(false, "Sigma Rizzler GYATT Tool", true);
        }
    }
}