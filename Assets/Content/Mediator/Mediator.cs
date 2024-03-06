using MiniIT.AUDIO;
using MiniIT.GAME;
using MiniIT.SAVE;
using System;
using Zenject;

namespace MiniIT.UI
{
    public class Mediator : IInitializable, IDisposable
    {
        private AudioSystem _audioSystem = null;
        private GlobalSFXSource _sfxSource = null;

        private MainMenu _mainMenu = null;
        private SettingsMenu _settingsMenu = null;
        private PlayMenu _playMenu = null;
        private ResultMenu _resultMenu = null;

        private ILoad<SettingsJSON> _loadSettingsManager = null;
        private ISave<SettingsJSON> _saveSettingsManager = null;

        private ILoad<ProgressJSON> _loadProgressManager = null;
        private ISave<ProgressJSON> _saveProgressManager = null;

        private GameController _gameController = null;

        public Mediator(AudioSystem audioSystem, GlobalSFXSource globalSFXSource,
            MainMenu mainMenu, SettingsMenu settingsMenu, PlayMenu playMenu, ResultMenu resultMenu,
            ISave<SettingsJSON> saveSettingsManager, ILoad<SettingsJSON> loadSettingsManager, 
            ILoad<ProgressJSON> loadProgressManager, ISave<ProgressJSON> saveProgressManager,
            GameController gameController)
        {
            _audioSystem = audioSystem;
            _sfxSource = globalSFXSource;

            _mainMenu = mainMenu;
            _settingsMenu = settingsMenu;
            _playMenu = playMenu;
            _resultMenu = resultMenu;

            _loadSettingsManager = loadSettingsManager;
            _saveSettingsManager = saveSettingsManager;

            _loadProgressManager = loadProgressManager;
            _saveProgressManager = saveProgressManager;

            _gameController = gameController;

            Binding();
        }

        public void Binding()
        {
            _mainMenu.ClickedPlayButton += OnClickPlayButtonInMainMenu;
            _mainMenu.ClickedSettingsButton += OnClickSettingsButtonInMainMenu;

            _settingsMenu.ChangedMusicSlider += OnChangeMusicValueInSettingsMenu;
            _settingsMenu.ChangedSFXSlider += OnChangeSFXValueInSettingsMenu;
            _settingsMenu.ClickedBackButton += OnClickBackButtonInSettingsMenu;
            _settingsMenu.MusicSliderPointer.PointerUp += OnPointerUpSliderInSettingsMenu;
            _settingsMenu.SFXSliderPointer.PointerUp += OnPointerUpSliderInSettingsMenu;

            _playMenu.ClickedSettingsButton += OnClickSettingsButtonInPlayMenu;
            _playMenu.ClickedSurrendButton += OnClickSurrendButtonInPlayMenu;

            _resultMenu.ClickedMenuButton += OnClickMenuButtonInResultMenu;

            _gameController.ScoreChanged += DisplayCurrentScore;
            _gameController.OnEndGame += FinishGame;
        }
        public void Dispose()
        {
            _mainMenu.ClickedPlayButton -= OnClickPlayButtonInMainMenu;
            _mainMenu.ClickedSettingsButton -= OnClickSettingsButtonInMainMenu;

            _settingsMenu.ChangedMusicSlider -= OnChangeMusicValueInSettingsMenu;
            _settingsMenu.ChangedSFXSlider -= OnChangeSFXValueInSettingsMenu;
            _settingsMenu.ClickedBackButton -= OnClickBackButtonInSettingsMenu;
            _settingsMenu.MusicSliderPointer.PointerUp -= OnPointerUpSliderInSettingsMenu;
            _settingsMenu.SFXSliderPointer.PointerUp -= OnPointerUpSliderInSettingsMenu;

            _playMenu.ClickedSettingsButton -= OnClickSettingsButtonInPlayMenu;
            _playMenu.ClickedSurrendButton -= OnClickSurrendButtonInPlayMenu;

            _resultMenu.ClickedMenuButton -= OnClickMenuButtonInResultMenu;

            _gameController.ScoreChanged -= DisplayCurrentScore;
            _gameController.OnEndGame -= FinishGame;
        }

        public void Initialize()
        {
            SettingsJSON settingsJSON = _loadSettingsManager.Load();

            OnChangeMusicValueInSettingsMenu(settingsJSON.Music);
            OnChangeSFXValueInSettingsMenu(settingsJSON.SFX);

            _settingsMenu.MusicSliderValue = settingsJSON.Music;
            _settingsMenu.SFXSliderValue = settingsJSON.SFX;

            ProgressJSON progressJSON = _loadProgressManager.Load();

            _mainMenu.SetRecordText(progressJSON.Record);
        }

        private void OnClickPlayButtonInMainMenu()
        {
            _sfxSource.PlayStartGame();

            _mainMenu.Close();
            _playMenu.Open();

            DisplayCurrentScore(0);

            _gameController.StartGame();
        }
        private void OnClickSettingsButtonInMainMenu()
        {
            _sfxSource.PlayClick();

            _mainMenu.Close();
            _settingsMenu.Open();
        }

        private void OnChangeMusicValueInSettingsMenu(float volume)
            => _audioSystem.ChangeAudioMixerVolumeMusic(volume);
        private void OnChangeSFXValueInSettingsMenu(float volume)
            => _audioSystem.ChangeAudioMixerVolumeSFX(volume);
        private void OnPointerUpSliderInSettingsMenu()
            => _sfxSource.PlayClick();
        private void OnClickBackButtonInSettingsMenu()
        {
            _sfxSource.PlayClick();

            _saveSettingsManager.Save(new SettingsJSON(_settingsMenu.MusicSliderValue, _settingsMenu.SFXSliderValue));

            _settingsMenu.Close();

            if (_gameController.IsPlaying == false)
            {
                _mainMenu.Open();
            }
            else
            {
                _gameController.SetPause(false);
                _playMenu.Open();
            }
        }

        private void OnClickSettingsButtonInPlayMenu()
        {
            _sfxSource.PlayClick();

            _gameController.SetPause(true);

            _playMenu.Close();
            _settingsMenu.Open();
        }
        private void OnClickSurrendButtonInPlayMenu()
            => FinishGame();

        private void OnClickMenuButtonInResultMenu()
        {
            _sfxSource.PlayClick();

            _resultMenu.Close();
            _mainMenu.Open();
        }

        private void DisplayCurrentScore(int value)
        {
            _playMenu.SetScoreText(value);
            _resultMenu.SetScoreTest(value);
        }
        private void FinishGame()
        {
            _sfxSource.PlayGameOver();

            _gameController.StopGame();

            ProgressJSON progressJSON = _loadProgressManager.Load();

            if (_gameController.Score > progressJSON.Record)
            {
                progressJSON.Record = _gameController.Score;
                _mainMenu.SetRecordText(progressJSON.Record);

                _saveProgressManager.Save(progressJSON);
            }

            _playMenu.Close();
            _resultMenu.Open();
        }
    }
}