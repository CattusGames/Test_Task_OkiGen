using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private CollectHandler _collectHandler;
    [SerializeField] private Button _endButton;
    [SerializeField] ProductSpawner _spawner;
    [SerializeField] GameObject _conveir;
    [SerializeField] Camera _camera;
    [SerializeField] Transform _endCameraPoint;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip backgroundSound;
    [SerializeField] private float volume = 0.9f;
    private AudioSource audioSource;
    private bool conveirAnimationIsPlayed = false;
    private void Start()
    {
        _collectHandler.onLevelPassed.AddListener(GameOver);
        audioSource = GetComponent<AudioSource>();
        PlaySound(backgroundSound);
    }
    private void GameOver()
    {
        _endButton.gameObject.SetActive(true);
        Destroy(_spawner);
        if (!conveirAnimationIsPlayed)
        {
            _conveir.GetComponent<Animation>().Play("Conveir_Gone");
            conveirAnimationIsPlayed = true;
            PlaySound(victorySound);
        }
        RotateCamera();
    }
    public void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.volume = volume;
        audioSource.Play();
    }
    private void RotateCamera()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _endCameraPoint.position, Time.deltaTime);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _endCameraPoint.rotation, Time.deltaTime);
    }
}
