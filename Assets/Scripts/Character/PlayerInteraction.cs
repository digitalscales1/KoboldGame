using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField] GameObject closer;
    List<GameObject> interactables = new List<GameObject>();
    [Header("Updated List Event")]
    [SerializeField] UnityEvent<List<GameObject>> OnUpdatedList;

    private void OnTriggerEnter(Collider collider) {
        GameObject obj = collider.gameObject;
        
        IInteractable interactable = obj.GetComponent<IInteractable>();
        if(interactable == null) return;

        interactables.Add(obj);
        OnUpdatedList?.Invoke(interactables);
    }

    private void OnTriggerExit(Collider collider) {
        GameObject obj = collider.gameObject;

        IInteractable interactable = obj.GetComponent<IInteractable>();
        if(interactable == null) return;

        for(int i = 0; i < interactables.Count; i++) {
            GameObject current = interactables[i];
            if(current == obj) {
                interactables.RemoveAt(i);
                OnUpdatedList?.Invoke(interactables);
                return;
            }
        }
    }
}
