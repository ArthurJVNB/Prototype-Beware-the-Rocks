using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainingMeteors : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Vector2 startPosition;
    public float stepDistance;

    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Rain()
    {
        StartCoroutine(RainRoutine());
    }

    private IEnumerator RainRoutine()
    {
        Vector2 currentPosition = new Vector2(startPosition.x + player.transform.position.x, startPosition.y);

        for (int i = 0; i < 10; i++)
        {
            Instantiate(meteorPrefab, currentPosition, Quaternion.identity);
            currentPosition += new Vector2(stepDistance, 0);
            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(startPosition, Vector2.one * .2f);
        Gizmos.DrawWireCube(new Vector2(startPosition.x + stepDistance, startPosition.y), Vector2.one * .2f);
    }
}
