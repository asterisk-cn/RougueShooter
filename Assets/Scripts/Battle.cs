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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
#if UNITY_EDITOR 
        // キー入力でデバッグ
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
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
        Debug.Log($"Player {playerId} is upgradable.");
        _upgradablePlayerId = playerId;
    }

    void OnBattleStart()
    {
        Debug.Log("Battle Start");
    }

    void OnUpgrade()
    {
        Debug.Log("Upgrade");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
