using R3;
using Players;
using Cysharp.Threading.Tasks;

public class PlayerPresenter
{
    public async void BindHealth(Health model, HealthView view)
    {
        await model.InitializedAsync;

        view.Initialize(model.CurrentHealth / model.MaxHealth);

        model.OnCurrentHealthChangedAsObservable
            .Subscribe(x => view.SetHealth(model.CurrentHealth / model.MaxHealth));

        model.OnCurrentHealthChangedWithoutAnimationAsObservable
            .Subscribe(_ => view.SetHealthWithoutAnimation(model.CurrentHealth / model.MaxHealth));
    }
}
