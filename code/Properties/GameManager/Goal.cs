using Sandbox;

public sealed class Goal : Component, Component.ITriggerListener
{
	[Property] public GameObject board;
	[Property] public GameObject ball;
	[Property] public BallMovement ballScript;
	public void OnTriggerEnter(Collider other)
	{
		if (other.Tags.Has("pong"))
		{
			ballScript.speed++;
			ballScript.Reset();
		}
	}
	protected override void OnStart()
	{
		ballScript.Components.Get<BallMovement>();
	}
}
