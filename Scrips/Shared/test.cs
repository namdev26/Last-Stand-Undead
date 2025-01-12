// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Pathfinding;

// public class ZombieAI : MonoBehaviour
// {
//     public Seeker seeker;
//     public float moveSpeed;
//     public float nextWaypointDistance;
//     public SpriteRenderer zombieSR;
//     public Transform target;
//     private Coroutine moveCoroutine;
//     private Path path;

//     private void Start()
//     {
//         // Ensure the Player object exists in the scene
//         target = FindObjectOfType<Player>()?.transform;
//         if (target == null)
//         {
//             Debug.LogError("Player not found in the scene!");
//             return;
//         }

//         // Ensure the Seeker and SpriteRenderer components are assigned
//         if (seeker == null)
//         {
//             Debug.LogError("Seeker component is missing from the Zombie!");
//             return;
//         }

//         if (zombieSR == null)
//         {
//             Debug.LogError("Zombie SpriteRenderer is missing!");
//             return;
//         }

//         // Start pathfinding updates
//         InvokeRepeating("CaculatorPath", 0f, 0.5f);
//     }

//     void CaculatorPath()
//     {
//         if (seeker.IsDone())
//         {
//             seeker.StartPath(transform.position, target.position, OnPathCallback);
//         }
//     }

//     void OnPathCallback(Path p)
//     {
//         if (!p.error)
//         {
//             path = p;
//             if (moveCoroutine != null)
//             {
//                 StopCoroutine(moveCoroutine);
//             }
//             moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
//         }
//     }

//     IEnumerator MoveToTargetCoroutine()
//     {
//         int currentWP = 0;
//         if (path == null || path.vectorPath == null || path.vectorPath.Count == 0) yield break;

//         while (currentWP < path.vectorPath.Count)
//         {
//             Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
//             Vector3 force = direction * moveSpeed * Time.deltaTime;
//             transform.position += force;
//             float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);

//             if (distance < nextWaypointDistance)
//             {
//                 currentWP++;
//             }

//             // Flip zombie sprite based on direction
//             if (Mathf.Abs(direction.x) > 0.01f)
//             {
//                 zombieSR.transform.localScale = new Vector3(Mathf.Sign(direction.x) * 2, 2, 1);
//             }

//             yield return null;
//         }
//     }
// }
