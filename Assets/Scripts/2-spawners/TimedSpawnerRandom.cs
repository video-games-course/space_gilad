using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/**
 * This component instantiates a given prefab at random time intervals and random bias from its object position.
 */
public class TimedSpawnerRandom: MonoBehaviour {
    [SerializeField] Mover prefabToSpawn;
    [SerializeField] Vector3 velocityOfSpawnedObject;
    //מהירות מקס ומינימלית בין יצירת עצמים
    [Tooltip("Minimum time between consecutive spawns, in seconds")] [SerializeField] float minTimeBetweenSpawns = 0.2f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")] [SerializeField] float maxTimeBetweenSpawns = 1.0f;
    //המרחק המקסימלי בין עצמים
    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")][SerializeField] float maxXDistance = 1.5f;
    [SerializeField] int worth = 0;

    void Start() {
         this.StartCoroutine(SpawnRoutine());    // co-routines
    }

    IEnumerator SpawnRoutine() {    // co-routines
        while (true)
        {
            //יצירת עצמים באופן רנדומלי בין הזמן המינימלי למקסימלי
            float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            //נחזיר ערך ואז נחזור לשורה הזאת ונמשיך בקוד
            yield return new WaitForSeconds(timeBetweenSpawnsInSeconds);       // co-routines
            //עושה מיקום רנדומלי לחדש בשביל שאוכל לייצר עוד עצם חללית
            Vector3 positionOfSpawnedObject = new Vector3(
                transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
                transform.position.y,
                transform.position.z);
            //נייצר עצם חדש
            GameObject newObject = Instantiate(prefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
            //נעדכן לו את התזוזה
            newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);
            newObject.GetComponent<Mover>().SetWorth(this.worth);
        }
    }


    public int getWorth()
    {
        return this.worth;
    }
}
