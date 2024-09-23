using Microsoft.Win32;
using Sandbox;
using System.Diagnostics.Tracing;

public sealed class BallMovement : Component, Component.ICollisionListener, Component.ITriggerListener
{
	[Property] public float speed;
	[Property] public Rigidbody rb;
	[Property] public int playerScore = 0;
	[Property] public int enemyScore = 0;
	[Property] public Vector3 direction = new Vector3();
	public bool returnPosition = false;
	protected override void OnStart()
	{
		direction = Game.Random.Float( -1f, 1f );
		rb.Components.Get<Rigidbody>();
		rb.Velocity = (direction.WithZ(0) * speed);
	}

	public void OnTriggerEnter(Collider other) 
	{
		if (other.Tags.Has("lboard"))
		{
			speed = 180;
			enemyScore++;
			returnPosition = true;
		}
		if ( other.Tags.Has( "rboard" ) )
		{
			speed = 180;
			playerScore++;
			returnPosition = true;
		}
	}
	protected override void OnFixedUpdate()
	{
		if (returnPosition == true) 
		{
			rb.Transform.Position = new Vector3(0,0,0);
			returnPosition = false;
		}
	}
	public void OnCollisionStart(Collision other)
	{
		direction = Vector3.Reflect( direction, other.Contact.Normal ).Normal.WithX(0);
		other.Self.Body.Velocity = direction * speed;
		other.Self.Body.AngularVelocity = 0;
		speed++;
	}
}
