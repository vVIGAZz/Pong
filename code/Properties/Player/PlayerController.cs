using Sandbox;

public sealed class PlayerController : Component
{
	[Property] private float speed;
	[Property] private bool isAI;
	[Property] private GameObject ball;
	[Property] private Rigidbody rb;
	private Vector3 enemyMove;

	protected override void OnStart()
	{
		rb.Components.Get<Rigidbody>();
	}
	private void PlayerControll()
	{
		if (Input.Down( "Forward" ) )
		{
			rb.Velocity += Vector3.Up * speed;
		}
		if ( Input.Down( "Backward" ) )
		{
			rb.Velocity += Vector3.Down * speed;
		}
	}
	private void controllerAI()
	{
		if (ball.Transform.Position.z > Transform.Position.z + 0.5f)
		{
			enemyMove = new Vector3( 0, 0, 1 );
		}
		else if ( ball.Transform.Position.z < Transform.Position.z - 0.5f )
		{
			enemyMove = new Vector3( 0, 0, -1 );
		}
		else
		{
			enemyMove = new Vector3 ( 0,0,0 );
		}
	}
	protected override void OnUpdate() 
	{
		if (isAI)
		{
			controllerAI();
		}
		else
		{
			PlayerControll();
		}
	}
	protected override void OnFixedUpdate()
	{
		rb.Velocity = enemyMove * speed;
	}

}
