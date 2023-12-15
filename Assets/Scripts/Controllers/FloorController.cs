using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject floorTile1, floorTile2;

    public GameObject[] tiles;

    private void FixedUpdate()
    {
        if (!GameManager.instance.inGame) return;

        floorTile1.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0f, 0f);
        floorTile2.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0f, 0f);

        if(floorTile2.transform.position.x < 0f)
        {
            var newTile = Instantiate(tiles[Random.Range(0, tiles.Length)], floorTile2.transform.position + new Vector3(16f, 0f, 0f), Quaternion.identity);

            Destroy(floorTile1);

            floorTile1 = floorTile2;
            floorTile2 = newTile;
        }
    }
}
