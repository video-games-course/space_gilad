using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component spawns the given object whenever the player clicks a given key.
 */
public class ClickSpawner: MonoBehaviour {
    [SerializeField] protected InputAction spawnAction = new InputAction(type: InputActionType.Button); // pressing space action 
    [SerializeField] protected GameObject prefabToSpawn;    // prefab that we want to create' cre
    [SerializeField] protected Vector3 velocityOfSpawnedObject; //what speed we create the subjuect

    void OnEnable()  {
        spawnAction.Enable();
    }

    void OnDisable()  {
        spawnAction.Disable();
    }

    protected virtual GameObject spawnObject() {
        Debug.Log("Spawning a new object");

        // Step 1: spawn the new object.
        Vector3 positionOfSpawnedObject = transform.position;  // span at the containing object position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.
      
        //יצירת עצם חדש, מקבל את הדגם שאני רוצה ליצור, המיקום והסיבוב (מתייחס לשניים למעלה
        GameObject newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);    

        // Step 2: modify the velocity of the new object.\\ אם יש mover אז נעשה לו setVelocity
        Mover newObjectMover = newObject.GetComponent<Mover>();
        Mover newObjectMover2 = newObject.GetComponent<Mover>();
        if (newObjectMover) {
            newObjectMover.SetVelocity(velocityOfSpawnedObject);
        }

        return newObject;
    }
    
    private void Update() {
        if (spawnAction.WasPressedThisFrame())
        {
            spawnObject();
        }
    }
}
