using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableManagerUI : MonoBehaviour {
    
    [Header("Dependiences")]
    [SerializeField] private Camera myCamera;
    [SerializeField] private Canvas canvas;

    [Header("Text Prefab")]
    [SerializeField] private GameObject textPrefab;
    [SerializeField] bool flag = true;
    private Dictionary<GameObject, TMP_Text> textAssets;

    private void Start() {
        textAssets = new Dictionary<GameObject, TMP_Text>();
    }

    private void Update() {
        if(flag || textAssets.Count <= 0) return;

        foreach(var asset in textAssets) {
            
            GameObject obj = asset.Key;
            TMP_Text text = asset.Value;

            Vector3 screenPos = myCamera.WorldToScreenPoint(obj.transform.position);
    
            text.transform.position = screenPos;
            text.text = obj.name;
        }
    }

    public void OnInteractablesChanged(List<GameObject> gameObjects) {

        flag = gameObjects.Count < 0 || gameObjects == null;
        if (flag) return;

        foreach(var v in textAssets) {
            TMP_Text text = v.Value;
            Destroy(text);
        }
        textAssets.Clear();

        foreach(GameObject g in gameObjects) {
            GameObject o = Instantiate(textPrefab, canvas.transform);
            TMP_Text text = o.GetComponent<TMP_Text>();

            textAssets.Add(g, text);
        }
    }

}
