public class BaseEnemyBehaviour : ExplodingObject
{

    protected override void OnExplodeAnimation()
    {
        GameManager.Instance.AddMedals();
        GameUIController.Instance.MedalsIndicatorsContainer.Spawn(transform.position);
        base.OnExplodeAnimation();
    }
}
