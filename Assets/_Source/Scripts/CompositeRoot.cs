using System;
using System.Collections;
using System.Collections.Generic;
using App;
using MetaGameplay;
using PingPonger.Gameplay;
using UnityEngine;
using View;
using YG;

public class CompositeRoot : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private GameplayController _gameplayController;
    [SerializeField] private GameContextInstaller _gameContextInstaller;
    [SerializeField] private EnvironmentCreator _environmentCreator;

    [Header("Meta")]
    [SerializeField] private ContentPackController _contentPackController;
    [SerializeField] private GameSessionRewardManager _gameSessionRewardManager;
    
    [Header("UI")]
    [SerializeField] private UI _ui;

    private List<IDisposable> _disposables = new List<IDisposable>();

    private IEnumerator Start()
    {
        while (YandexGame.SDKEnabled == false)
        {
            yield return null;
        }

        StartGame();
    }

    private void StartGame()
    {
        //Application
        var defaultData = new GameData();

        var dataLoader = new YGDataLoader();
        dataLoader.Load(defaultData);

        var coinsCurrency = new Currency(dataLoader.CurrentGameData.CurrentCoins);
        var coinsInteractor = new CoinsInteractor(dataLoader, coinsCurrency);

        _disposables.Add(coinsInteractor);

        //Meta
        _contentPackController.Init(_gameContextInstaller, _environmentCreator, _ui.SessionPresenter.SessionScoreView);
        _gameSessionRewardManager.Init(coinsCurrency);

        //UI
        _ui.MainMenuPresenter.Init(_gameplayController);
        _ui.SessionPresenter.Init(_gameplayController, _gameSessionRewardManager);

        _ui.MainMenuPresenter.CoinsPresenter.Init(coinsCurrency);
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}