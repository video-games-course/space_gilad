using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/**
 * This component spawns the given object at fixed time-intervals at its object position.
 */
public class TimedSpawner: MonoBehaviour {
    [SerializeField] Mover prefabToSpawn;
    [SerializeField] Vector3 velocityOfSpawnedObject;   //the speed
    [SerializeField] float secondsBetweenSpawns = 1f;   // 1 second between creation of spaceship

    // OLD CODE using coroutines:
    //
    void Start()
    {
        this.StartCoroutine(SpawnRoutine());
        Debug.Log("Start finished");
    }

    //איך מייצרים עצם כל כמה שניות בצורה קבועה
    IEnumerator SpawnRoutine() {
        while (true)
        {
            //מייצר את העצם
            GameObject newObject = Instantiate(prefabToSpawn.gameObject, transform.position, Quaternion.identity);
            //עושה לו סט למהירות
            newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);
            //תחזיר ליוניטי, ובעוד (שניה) תחזור ללולאה
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    // NEW CODE using async-await - works on Windows but not on WebGL
    //
    // void Start() {
    //     // SpawnRoutine();  // generates a warning
    //     // SpawnRoutine().GetAwaiter();    
    //     _ = SpawnRoutine();  // discard the awaiter - do not wait for this task to finish
    //     Debug.Log("Start finished");
    // }

    // async Task SpawnRoutine() {
    //     while (true) {
    //         GameObject newObject = Instantiate(prefabToSpawn.gameObject, transform.position, Quaternion.identity);
    //         newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);
    //         await Task.Delay((int)(secondsBetweenSpawns*1000));
    //         // To wait to the next frame, use:
    //         // await Task.Yield();
    //         // Credit: https://www.youtube.com/watch?v=WY-mk-ZGAq8
    //     }
    // }

}
