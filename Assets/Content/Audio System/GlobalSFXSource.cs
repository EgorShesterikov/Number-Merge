using UnityEngine;

namespace MiniIT.AUDIO
{
    [RequireComponent(typeof(AudioSource))]
    public class GlobalSFXSource : MonoBehaviour
    {
        [Header("Global UI")]
        [SerializeField] private AudioClip _sfxUIClick;
        [SerializeField] private AudioClip _sfxUILock;

        [Header("Play")]
        [SerializeField] private AudioClip _startGame;
        [SerializeField] private AudioClip _gameOver;

        [Space]
        [SerializeField] private AudioSource _sfxUISource;

        public void PlayClick()
            => _sfxUISource.PlayOneShot(_sfxUIClick);
        public void PlayLock() 
            => _sfxUISource.PlayOneShot(_sfxUILock);

        public void PlayStartGame()
            => _sfxUISource.PlayOneShot(_startGame);
        public void PlayGameOver()
            => _sfxUISource.PlayOneShot(_gameOver);
    }
}
