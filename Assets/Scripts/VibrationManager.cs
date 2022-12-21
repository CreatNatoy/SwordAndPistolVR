using System;
using System.Collections;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return; 
        }

        Instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    public void VibrateController(float duration, float frequency, float amplitude, OVRInput.Controller controller) =>
        StartCoroutine(VibrateForSeconds(duration, frequency, amplitude, controller));

    private IEnumerator VibrateForSeconds(float duration, float frequency, float amplitude, OVRInput.Controller controller) {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        yield return new WaitForSeconds(duration);
        
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
