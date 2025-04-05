using UnityEngine;

public class SpawnerC : MonoBehaviour
{
    public GameObject[] EnemyArray;

    public GameObject Boss;

    public float SpawnInterval;

    private float _spawnTimer;

    private int _currentChance;

    private Camera _mainCamera;

    private Vector3 _cameraLeftBottom;

    private Vector3 _cameraRightTop;

    private int _randomSideNumber;

    private Vector3 _spawnPosition;

    private ScoreScript _scoreManager;

    public bool _bossFight;

    bool _haveBoss;
    
    void Start()
    {

        _scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreScript>();

        SpawnInterval = 0.68f; 

        _spawnTimer = SpawnInterval;

        _mainCamera = Camera.main;

        _bossFight = false;

        _haveBoss = false;
    }

    
    
    void Update()
    {
        _cameraLeftBottom = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _mainCamera.nearClipPlane));
        _cameraRightTop = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, _mainCamera.nearClipPlane));

        if (_spawnTimer > 0)
        {
            _spawnTimer -= Time.deltaTime;
            return;
        }

        //Choose which side to spawn enemy




        _randomSideNumber = Random.Range(0, 4);

        switch (_randomSideNumber)
        {
            case 0: // Spawn on top

                _spawnPosition = new Vector3(Random.Range(_cameraLeftBottom.x, _cameraRightTop.x), _cameraRightTop.y + 1, _mainCamera.nearClipPlane);
                break;

            case 1: // Spawn on bottom
                _spawnPosition = new Vector3(Random.Range(_cameraLeftBottom.x, _cameraRightTop.x), _cameraLeftBottom.y - 1, _mainCamera.nearClipPlane);
                break;

            case 2: // Spawn on left
                _spawnPosition = new Vector3(_cameraLeftBottom.x - 1, Random.Range(_cameraLeftBottom.y, _cameraRightTop.y), _mainCamera.nearClipPlane);
                break;

            case 3:// Spawn on right
                _spawnPosition = new Vector3(_cameraRightTop.x + 1, Random.Range(_cameraLeftBottom.y, _cameraRightTop.y),  _mainCamera.nearClipPlane);
                break;
        }
        

        _currentChance = Random.Range(0, EnemyArray.Length);

        
        if(_bossFight == false)
        {
            Instantiate(EnemyArray[_currentChance],_spawnPosition , transform.rotation);
        }
        
        
        if(_scoreManager.CurrentScore >= 50 && _haveBoss == false)
        {
            if ( Boss == null) { Debug.LogWarning(gameObject.name + ": Boss is missing something."); return; }
            _bossFight = true;

            Instantiate(Boss, Vector3.zero, transform.rotation);
            _haveBoss = true;

        }

        _spawnTimer = SpawnInterval;

;    }
}
