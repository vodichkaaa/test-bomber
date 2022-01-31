using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private int _range;

    private readonly Vector2 _right = new Vector2(3.12f, 0f);
    private readonly Vector2 _left = new Vector2(-3.12f, 0f);
    private readonly Vector2 _up = new Vector2(0.36f, 3.64f);
    private readonly Vector2 _down = new Vector2(-0.36f, -3.64f);

    private Player _player = null;
    private BombSpawner _spawner = null;
    private SpawnManager _spawnManager = null;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _spawner = FindObjectOfType<BombSpawner>();
        _spawnManager = FindObjectOfType<SpawnManager>();

        Invoke(nameof(Explode), 3f);
    }

    public void Explode() 
    {
        var explosion = Instantiate (_explosionPrefab, gameObject.transform.position, Quaternion.identity);
        
        Destroy(explosion, _duration);
        CreateExplosions (_right);
        CreateExplosions (_left);
        CreateExplosions (_up);
        CreateExplosions (_down);
        Destroy (gameObject);
        AstarPath.active.Scan();
        
        _spawner.isBombPlaced = false;
    }
    
    private void CreateExplosions(Vector2 direction) 
    {
        var contactFilter2D = new ContactFilter2D ();
        Vector2 explosionDimensions = _explosionPrefab.GetComponent<SpriteRenderer>().bounds.size;
        var explosionPosition = (Vector2)gameObject.transform.position + explosionDimensions.x * direction;
        
        for (var i = 1; i < _range; i++) 
        {
            var colliders = new Collider2D[4];
            var checkCollision = false;
            
            Physics2D.OverlapBox(explosionPosition, explosionDimensions, 0.0f, contactFilter2D, colliders);
            
            foreach (var collider in colliders)
            {
                if (!collider) continue;
                
                checkCollision = collider.CompareTag("Stone") || 
                                 collider.CompareTag("Bush") || 
                                 collider.CompareTag("Player") || 
                                 collider.CompareTag("Enemy");

                if (collider.CompareTag("Bush"))
                {
                    Destroy(collider.gameObject);
                    AstarPath.active.Scan();
                }
                if (collider.CompareTag("Enemy"))
                {
                    collider.gameObject.SetActive(false);
                    _spawnManager.enemies--;
                    checkCollision = false;
                }
                if (collider.CompareTag("Player"))
                {
                    _player.health--;
                    checkCollision = false;
                }
                if (collider.CompareTag("Stone")) break;
                
            }
            if(checkCollision) break;

            var explosion = Instantiate(_explosionPrefab, explosionPosition, Quaternion.identity);
            Destroy(explosion, _duration);
            explosionPosition += explosionDimensions.x * direction;
        }
    }
    
}