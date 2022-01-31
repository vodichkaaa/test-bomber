using Pathfinding;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _bombPrefab;

    private PlayerMovement _player;
    
    private static readonly int SPEED = Animator.StringToHash("Speed");

    public bool isBombPlaced = false;

    private void Start()
    {
        _player = GetComponent<PlayerMovement>();
        isBombPlaced = false;
    }

    private void Update() 
    {
        if ((SimpleInput.GetButton("Bomb") || Input.GetKeyDown(KeyCode.Space)) && 
            _player.animator.GetFloat(SPEED) == 0 && 
            !isBombPlaced) DropBomb();
        
    }

    private void DropBomb()
    {
        Instantiate(_bombPrefab, gameObject.transform.position, Quaternion.identity);
        isBombPlaced = true;
        AstarPath.active.Scan();
    }
}