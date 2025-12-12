using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private Button _exitButton;

    [Inject]
    private void Initialize(IObjectResolver container)
    {
        var sc = container.Resolve<SceneController>();
            
        _startButton.onClick.AddListener(sc.LoadMainGameScene);
        _tutorialButton.onClick.AddListener(sc.LoadTutorialScene);
        _exitButton.onClick.AddListener(sc.LoadExitScene);  
    }
}