using UnityEngine;
using R3;
using VContainer;
using VContainer.Unity;
using Players;
using Managers;

public enum BattleState
{
    Start,
    Battle,
    Upgrade,
    End
}

public class Battle : MonoBehaviour, IBattleStateProvider
{
    public ReadOnlyReactiveProperty<BattleState> CurrentState => _state;
    public int UpgradablePlayerId => _upgradablePlayerId;

    private ReactiveProperty<BattleState> _state = new ReactiveProperty<BattleState>(BattleState.Start);
    private int _upgradablePlayerId;

    [Inject]
    public void Construct()
    {
        PostInitialize();
    }

    public void PostInitialize()
    {
        _state
            .Where(s => s == BattleState.Battle)
            .Subscribe(_ => OnBattle())
            .AddTo(this);


        CurrentState.AddTo(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
#if UNITY_EDITOR
        // キー入力でデバッグ
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.U))
            .Subscribe(_ =>
            {
                SetUpgradablePlayer(0);
                SetState(BattleState.Upgrade);
            });
#endif
    }

    public void SetState(BattleState state)
    {
        _state.Value = state;
    }

    public void SetUpgradablePlayer(int playerId)
    {
        _upgradablePlayerId = playerId;
    }

    public void OnPlayerDead(int playerId)
    {
        SetUpgradablePlayer(playerId);
        SetState(BattleState.Upgrade);
        Debug.Log($"Player {playerId} is dead.");
    }

    private void OnBattle()
    {
        Debug.Log("Battle phase started.");
    }
}
