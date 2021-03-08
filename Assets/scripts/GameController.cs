using System.Collections.Generic;
using gameSystems.cameraSystems;
using gameSystems.levelGenerator;
using gameSystems.levelInteractiveControllers;
using gameSystems.skills;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CameraContainer _mainCamera;
    [Space]
    [SerializeField]
    private GameObject _playerGameObject;
        
    [Space]
    [SerializeField]
    private GameObject _skillObject;

    [Space]
    [SerializeField]
    private List<LevelGenerationData> _levelGenerationData;
    
    [Space]
    [SerializeField]
    private LevelPartFinalContainer _levelPartFinalContainer;

    private PlayerController _playerController;
    private LevelInteractiveController _levelInteractiveController;

    private int _level = 1;
    private float _gameSpeed = 1f;

    private void Start()
    {
        StartNewLevel();
    }

    private void PlayerInstantiation()
    {
        if (!_playerGameObject.scene.IsValid()) _playerGameObject = Instantiate(_playerGameObject);
        LevelGenerator.SetPlayer(_playerGameObject);
        _playerController = _playerGameObject.GetComponent<PlayerController>();
        _playerController.StopPlayerRigidbody();
        _mainCamera.FallowTransform = _playerGameObject.transform;
    }

    private void GenerateLevel()
    {
        LevelGenerationData data = _levelGenerationData.Find(levelComplexity => levelComplexity.Level == _level);
        List<GameObject> staticParts = new List<GameObject>();
        List<LevelPartContainer> dynamicParts = new List<LevelPartContainer>();
        LevelGenerator.GenerateLevel(data, ref staticParts, ref dynamicParts, _levelPartFinalContainer);
        _levelInteractiveController = new LevelInteractiveController(staticParts, dynamicParts);
        _levelPartFinalContainer.GameController = this;
        GameObject skillGameObject = SkillObjectGenerator.GenerateSkillGameObject(_skillObject, data.LevelSkill);
        skillGameObject.transform.position = data.StartSkillPosition;
    }

    public void LevelUp()
    {
        _level++;
        if (_level == _levelGenerationData.Capacity)
        {
            _level = 0;
        }
        _levelInteractiveController.DestroyLevelParts();
        StartNewLevel();
    }

    private void StartNewLevel()
    {
        GenerateLevel();
        PlayerInstantiation();
    }

    private void RestartLevel()
    {
        _levelInteractiveController.DestroyLevelParts();
        StartNewLevel();
    }

    private void Update()
    {
        _levelInteractiveController.UpdateLevelState(Time.deltaTime * _gameSpeed);

        if (_playerController.transform.position.y < -20)
        {
            RestartLevel();
        }
    }

    public void SetGameSpeed(float speedMultiplier)
    {
        _gameSpeed = speedMultiplier;
    }
}