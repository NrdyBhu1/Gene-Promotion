extends Node3D


func _input(event):
	if (event is InputEventKey):
		if (event.is_pressed() && event.keycode == KEY_R):
			get_tree().reload_current_scene()
		elif (event.is_pressed() && event.keycode == KEY_ESCAPE):
			get_tree().quit()
