using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableManagerUI : MonoBehaviour {
    
    TMP_Text target;

    [Header("Dependiences")]
    [SerializeField] private Camera myCamera;
    [SerializeField] private Canvas canvas;

    [Header("Text Prefab")]
    [SerializeField] private GameObject textPrefab;
    private Dictionary<GameObject, TMP_Text> textAssets;

    private void Start() {
        textAssets = new Dictionary<GameObject, TMP_Text>();
    }

    private void Update() {
        if (textAssets.Count <= 0) return;

        foreach(var pair in textAssets) {
            TMP_Text text = pair.Value;
            GameObject other = pair.Key;

            Vector3 uiPos = myCamera.WorldToScreenPoint(other.transform.position);

            text.transform.position = uiPos;
            text.text = other.name;
        }
    }

    public void OnInteractableAdded(GameObject other) {
        if (!other || textAssets.ContainsKey(other)) return;

        GameObject newText = Instantiate(textPrefab, canvas.transform);
        TMP_Text component = newText.GetComponent<TMP_Text>();

        textAssets.Add(other, component);
    }

    public void OnInteractableRemoved(GameObject other) {
        if (!other || !textAssets.ContainsKey(other)) return;

        TMP_Text component = textAssets[other];

        textAssets.Remove(other);
        Destroy(component.gameObject);
    }

    public void OnTargetFound(GameObject other) {
        if (!other || !textAssets.ContainsKey(other)) return;

        TMP_Text text = textAssets[other];
        if(text != target) {
            if(target) target.color = Color.white;
            target = text;
        }

        text.color = Color.green;
    }
}
