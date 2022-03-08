using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f; // added range to avoid a negative number breaking travel percent,  

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }


    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear(); // clears list(path) when we find a new one
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    // move enemy to first waypoint
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f; // 0 is the start position while 1 if the end position

            transform.LookAt(endPosition); // always face the waypoint headed towards

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed; // this updates the travelpercentage using time
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent); // this moves the enemy
                yield return new WaitForEndOfFrame(); 
                    // waits till end of frame goes back to while loop to check if travelPereent has reached 1
            }
        }

        FinishPath();
    }

}
