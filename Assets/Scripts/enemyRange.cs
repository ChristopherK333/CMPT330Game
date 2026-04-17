using UnityEngine;
using Pathfinding;


public class enemyRange : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 40f;

    private IAstarAI ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {

        // checks if player is within the tracking range
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            ai.destination = player.position;
            ai.isStopped = false;
        }
        else
        {
            ai.isStopped = true;
        }
    }
}
