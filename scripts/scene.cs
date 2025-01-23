using Godot;
using System;

public partial class scene : Node3D
{
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) 
		{
			if (eventKey.Pressed && eventKey.Keycode == Key.R)
				GetTree().ReloadCurrentScene();  
			else if (eventKey.Pressed && eventKey.Keycode == Key.Escape)
				GetTree().Quit();
		}
	}
}
