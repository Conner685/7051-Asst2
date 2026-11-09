using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public CharacterLocomotion player;
    public GameObject enemy;
    public Maze maze;

    public void Start()
    {
        Instantiate(maze.gameObject);
        player.startPoint = maze.startPoint.transform;
        player.enemyPoint = maze.enemyPoint.transform;

        player.transform.position = maze.startPoint.transform.position;
        enemy.transform.position = maze.enemyPoint.transform.position;
    }

}
