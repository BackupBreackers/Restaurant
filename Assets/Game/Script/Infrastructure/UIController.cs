using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button exitToMainMenuButton;
        
    private SceneController _sceneController;

    [Inject]
    private void Initialize(IObjectResolver objectResolver)
    {
        _sceneController = objectResolver.Resolve<SceneController>();
    }
        
    private void Start()
    {
        exitToMainMenuButton.onClick.AddListener(_sceneController.LoadMainMenuScene);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
}