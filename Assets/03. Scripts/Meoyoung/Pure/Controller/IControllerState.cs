public interface IControllerState
{
    public void OnStateEnter(PureController controller);

    public void OnStateUpdate();

    public void OnStateExit();
}