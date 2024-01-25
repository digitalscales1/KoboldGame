using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour {

    GameObject target;
    HashSet<GameObject> interactables = new HashSet<GameObject>();

    /*[Header("Dependencies")]
    [SerializeField] InputActionAsset asset;
    InputAction input;
    InputActionMap action;*/

    [Header("Parameters")]
    [SerializeField] float maxDistance = 5.0f;
    //[SerializeField] float minAngle = 0.8f;
    [SerializeField] LayerMask rayMask;

    [Header("Target Found Event")]
    [SerializeField] UnityEvent<GameObject> OnTargetFound;

    [Header("Targets on Range Event")]
    [SerializeField] UnityEvent<GameObject> OnTargetAdded;
    [SerializeField] UnityEvent<GameObject> OnTargetRemoved;

    private void OnTriggerEnter(Collider collider) {
        GameObject other = collider.gameObject;
        
        bool flag = other.GetComponent<IInteractable>() == null || interactables.Contains(other);
        if(flag) return;

        interactables.Add(other);
        OnTargetAdded?.Invoke(other);
    }

    private void OnTriggerExit(Collider collider) {
        GameObject other = collider.gameObject;
        
        bool flag = other.GetComponent<IInteractable>() == null || !interactables.Contains(other);
        if(flag) return;

        interactables.Remove(other);
        OnTargetRemoved?.Invoke(other);
    }

    /*private void Start() {
        action = asset.FindActionMap("Player");
        input = action.FindAction("Move"); 
    }*/

    private void Update() {   

        // Empty target
        target = null;
        if(interactables.Count <= 0) return;

        float distance = 100.0f;

        // Check distance and raycast through each interactable
        foreach(GameObject game in interactables) {
            Vector3 direction = game.transform.position - transform.position;
            float sqrDistance = direction.sqrMagnitude;

            // Check if the interactable object can be reached, if not skip
            RaycastHit hit;
            bool reach = Physics.Raycast(transform.position, direction, out hit, maxDistance);
            if (!reach || hit.collider.gameObject != game) continue;

            Debug.DrawLine(transform.position, game.transform.position, Color.green);

            //float angle = Vector3.Dot(transform.forward, direction.normalized);

            // If it is closer than previous, store it in target
            if (sqrDistance <= distance) {
                target = game;
                distance = sqrDistance;
            }
        }

        OnTargetFound?.Invoke(target);
        // At the end, if no reachable target was found, it remains empty
    }

    public void OnInteractionKey(InputAction.CallbackContext context) {
        if(!target) return;

        IInteractable i = target.GetComponent<IInteractable>();
        i.OnInteraction();
    }
}
