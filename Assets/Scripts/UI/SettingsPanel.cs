using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle, soundsToggle, vibrationToggle;

    private void Start()
    {
        musicToggle.isOn = !AudioController.Instance.MusicMuted;
        musicToggle.onValueChanged.AddListener(OnMusicSwitched);

        soundsToggle.isOn = !AudioController.Instance.SoundsMuted;
        soundsToggle.onValueChanged.AddListener(OnSoundsSwitched);

        vibrationToggle.isOn = VibrationManager.Instance.VibrationEnabled;
        vibrationToggle.onValueChanged.AddListener(OnVibrationSwitched);
    }

    private void OnMusicSwitched(bool v)
    {
        AudioController.Instance.SetMusicMuted(!v);
    }

    private void OnSoundsSwitched(bool v)
    {
        AudioController.Instance.SetSoundsMuted(!v);
    }

    private void OnVibrationSwitched(bool v)
    {
        VibrationManager.Instance.SetVibrationEnabled(v);
    }
}
