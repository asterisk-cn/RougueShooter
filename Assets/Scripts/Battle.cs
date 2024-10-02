using UnityEngine;
using R3;

public class Battle : MonoBehaviour
{
    public enum BattleState
    {
        Start,
        Battle,
        Upgrade,
        End
    }

    public ReadOnlyReactiveProperty<BattleState> State => _state;
    public int UpgradablePlayerId => _upgradablePlayerId;

    private ReactiveProperty<BattleState> _state = new ReactiveProperty<BattleState>(BattleState.Start);
    private int _upgradablePlayerId;

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
        _state.Value = state;
    }

    public void SetUpgradablePlayer(int playerId)
    {
        _upgradablePlayerId = playerId;
    }
}
