using R3;

namespace Managers
{
    public interface IBattleStateProvider
    {
        ReadOnlyReactiveProperty<BattleState> CurrentState { get; }
    }
}
