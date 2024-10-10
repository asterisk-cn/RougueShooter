using R3;
using Managers;

namespace Players
{
    public interface IBattleStateProvider
    {
        ReadOnlyReactiveProperty<BattleState> CurrentState { get; }
    }
}
