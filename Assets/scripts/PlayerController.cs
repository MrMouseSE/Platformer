using gameSystems.skills;
using playerSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatsData _playerStatsData;
    [SerializeField] private Rigidbody2D _playerRigidbody2D;
    
    private PlayerStats _playerStats;
    private PlayerMovementController _playerMovementController;

    private PlayerSkillsController _playerSkillsController;

    private SkillContainer _nearestPlayerSkill;

    private void Awake()
    {
        _playerStats = new PlayerStats(_playerStatsData);
        _playerMovementController = new PlayerMovementController(_playerRigidbody2D, _playerStats);
        _playerSkillsController = new PlayerSkillsController(_playerStats);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _playerMovementController.MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _playerSkillsController.ManipulatePlayerSkills(_nearestPlayerSkill);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _nearestPlayerSkill = other.GetComponent<SkillContainer>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _nearestPlayerSkill = null;
    }

    public void StopPlayerRigidbody()
    {
        _playerRigidbody2D.angularVelocity = 0;
        _playerRigidbody2D.velocity = Vector2.zero;
    }
}