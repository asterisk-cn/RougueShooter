using UnityEngine;
using UniRx;

public class Battle : MonoBehaviour
{
    public enum BattleState
    {
        Start,
        Battle,
        Upgrade,
        End
    }

    public ReactiveProperty<BattleState> State { get; } = new ReactiveProperty<BattleState>(BattleState.Start);
    public int UpgradablePlayerId => _upgradablePlayerId;

    int _upgradablePlayerId;

    void Awake()
    {
        State.AddTo(this);
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
        State.Value = state;
    }

    public void SetUpgradablePlayer(int playerId)
    {
        _upgradablePlayerId = playerId;
    }
}
