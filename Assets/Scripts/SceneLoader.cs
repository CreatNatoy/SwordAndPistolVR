using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private ButtonPushClick _buttonPushClick;
    [SerializeField] private GameObject _centerEyeAnchor; 
    [SerializeField] private OVROverlay _overlayBackground;
    [SerializeField] private OVROverlay _overlayText; 

    private void Start() => 
        _buttonPushClick.OnPushedButton.AddListener(() => LoadScene(1));

    private void LoadScene(int indexScene) => 
        StartCoroutine(ShowOverlayAndLoad(indexScene));

    private IEnumerator ShowOverlayAndLoad(int indexScene) {
        EnableLoadScreen();
        SetPositionLoadText();
        yield return new WaitForSeconds(5f);

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(indexScene);

        while (!asyncLoad.isDone) {
            yield return null;
        }

        DisableLoadScreen();
        yield return null; 
    }

    private void EnableLoadScreen() {
        _overlayBackground.enabled = true;
        _overlayText.enabled = true;
    }

    private void DisableLoadScreen() {
        _overlayBackground.enabled = false;
        _overlayText.enabled = false;
    }

    private void SetPositionLoadText() => 
        _overlayText.transform.position = _centerEyeAnchor.transform.position + new Vector3(0, 0, 3f);
}
