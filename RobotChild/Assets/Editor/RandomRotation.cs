using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class RandomRotation : EditorWindow {

	[MenuItem("Window/RandomRotation")]
	static void Init() {
		RandomRotation window = (RandomRotation)EditorWindow.GetWindow(typeof(RandomRotation));
		window.Show();
	}

	public bool x, y, z;

	void OnGUI() {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Axis (X, Y, Z)");
		x = EditorGUILayout.Toggle(x);
		y = EditorGUILayout.Toggle(y);
		z = EditorGUILayout.Toggle(z);
		EditorGUILayout.EndHorizontal();

		var objects = Selection.transforms;
		if (GUILayout.Button("Rotate")) {			
			for (int i = 0; i < objects.Length; i++) {
				var rot = objects[i].rotation.eulerAngles;
				if (x) { objects[i].Rotate(90 * Random.Range(0, 4), 0, 0, Space.Self); }
				if (y) { objects[i].Rotate(0, 90 * Random.Range(0, 4), 0, Space.Self); }
				if (z) { objects[i].Rotate(0, 0, 90 * Random.Range(0, 4), Space.Self); }
			}
		}

		if (GUILayout.Button("Mirror")) {
			for (int i = 0; i < objects.Length; i++) {
				var scale = objects[i].localScale;
				if (x && Random.Range(0, 2) == 1) { scale.x = scale.x * -1; }
				if (y && Random.Range(0, 2) == 1) { scale.y = scale.y * -1; }
				if (z && Random.Range(0, 2) == 1) { scale.z = scale.z * -1; }
				objects[i].localScale = scale;
			}
		}
	}
}
