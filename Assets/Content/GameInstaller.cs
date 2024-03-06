using MiniIT.AUDIO;
using MiniIT.GAME;
using MiniIT.INPUT;
using MiniIT.SAVE;
using MiniIT.UI;
using UnityEngine;
using Zenject;

namespace MiniIT.INSTALLER
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GlobalSFXSource _globalSFXSource = null;

        [Space]
        [SerializeField] private MainMenu _mainMenu = null;
        [SerializeField] private SettingsMenu _settingsMenu = null;
        [SerializeField] private PlayMenu _playMenu = null;
        [SerializeField] private ResultMenu _resultMenu = null;

        [Space]
        [SerializeField] private SettingsManagerConfig _settingsManagerConfig = null;
        [SerializeField] private ProgressManagerConfig _progressManagerConfig = null;

        [Space]
        [SerializeField] private GameCellsManager _gameCellsManager = null;
        [SerializeField] private GameControllerConfig _gameControllerConfig = null;

        [Space]
        [SerializeField] private MergeObjectFactoryConfig _mergeObjectFactoryConfig = null;
        [SerializeField] private MergeObjectSpawner _mergeObjectSpawner = null;

        public override void InstallBindings()
        {
            BindInputHandler();
            BindAudioSystem();
            BindUI();
            BindSaveSystem();
            BindGameController();
            BindMergeObjectSpawner();
        }

        private void BindInputHandler()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
                Container.Bind<IInputHandler>().To<MobileInput>().FromNew().AsSingle();
            else
                Container.Bind<IInputHandler>().To<DesktopInput>().FromNew().AsSingle();
        }
        private void BindAudioSystem()
        {
            Container.Bind<AudioSystem>().FromNew().AsSingle();
            Container.BindInstance(_globalSFXSource);
        }
        private void BindUI()
        {
            Container.BindInstance(_mainMenu);
            Container.BindInstance(_settingsMenu);
            Container.BindInstance(_playMenu);
            Container.BindInstance(_resultMenu);
            Container.BindInterfacesTo<Mediator>().FromNew().AsSingle();
        }
        private void BindSaveSystem()
        {
            Container.BindInstance(_settingsManagerConfig);
            Container.BindInterfacesAndSelfTo<SettingsManager>().FromNew().AsSingle();

            Container.BindInstance(_progressManagerConfig);
            Container.BindInterfacesAndSelfTo<ProgressManager>().FromNew().AsSingle();
        }
        private void BindGameController()
        {
            Container.BindInstance(_gameCellsManager);
            Container.BindInstance(_gameControllerConfig);
            Container.BindInterfacesAndSelfTo<GameController>().FromNew().AsSingle();
        }
        private void BindMergeObjectSpawner()
        {
            Container.BindInstance(_mergeObjectFactoryConfig);
            Container.BindInstance(_mergeObjectSpawner);
            Container.Bind<MergeObjectFactory>().FromNew().AsSingle();
        }
    }
}