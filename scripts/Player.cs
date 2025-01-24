using Godot;
using System;

public partial class Player : CharacterBody3D
{
	
	[Export]
	public int Speed { get; set; } = 5;
	
	[Export]
	public int FallAcceleration { get; set; } = 75;
	
	private const float RayLength = 10.0f;
	
	private Vector3 _targetVelocity = Vector3.Zero;
	private Camera3D cam;
	private PhysicsDirectSpaceState3D collision;
	
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		cam = GetNode<Camera3D>("Camera3D");
		collision = GetWorld3D().DirectSpaceState;
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			Rotation = new Vector3(
				Rotation.X - eventMouseMotion.Relative.Y * 0.01f,
				Rotation.Y - eventMouseMotion.Relative.X * 0.01f,
				Rotation.Z
			);

			var from = cam.ProjectRayOrigin(eventMouseMotion.Position);
			var to = from + cam.ProjectRayNormal(eventMouseMotion.Position) * RayLength;
			var query = PhysicsRayQueryParameters3D.Create(from, to, 2);
			var result = collision.IntersectRay(query);
			if (result.ContainsKey("collider")) {
				Bed2 node = (Bed2)result["collider"];
				node.ChangeHighlight();
			}
		}

	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = Vector3.Zero;

		// We check for each move input and update the direction accordingly.
		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1.0f;
		}
		if (Input.IsActionPressed("move_backward"))
		{
			// Notice how we are working with the vector's X and Z axes.
			// In 3D, the XZ plane is the ground plane.
			direction.Z += 1.0f;
		}
		if (Input.IsActionPressed("move_forward"))
		{
			direction.Z -= 1.0f;
		}
		
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			// Setting the basis property will affect the rotation of the node.
			// GetNode<Camera3D>("Camera3D").Basis = Basis.LookingAt(direction);
			direction = (cam.GlobalTransform.Basis * direction).Normalized();
		}
		
		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;
		
		// Vertical velocity
		if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
		{
			_targetVelocity.Y -= FallAcceleration * (float)delta;
		}
		
		Velocity = _targetVelocity;
		MoveAndSlide();
	}
}
