namespace PingPonger.Gameplay
{
    public class DeathTrigger : TypedTrigger<IDestractable>
    {
        protected override void OnEnterTriggered(IDestractable other)
        {
            other.DoDestroy();
        }
    }
}