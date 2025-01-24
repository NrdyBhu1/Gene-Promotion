using Godot;
using System;

public partial class Bed2 : StaticBody3D
{
	private MeshInstance3D bed;
	private StandardMaterial3D mat;
	private bool isHighlighted = false;
	
	public override void _Ready() {
		bed = GetNode<MeshInstance3D>("Bed");
		mat = GD.Load<StandardMaterial3D>("res://shaders/outline_mat_alt.tres");
		bed.SetSurfaceOverrideMaterial(0, null);
	}
	
	public void _Process() {
		bed.SetSurfaceOverrideMaterial(0, null	);
	}
	
	public void ChangeHighlight() {
		bed.SetSurfaceOverrideMaterial(0, mat);
	}
}
