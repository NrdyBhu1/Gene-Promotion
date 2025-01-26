extends StaticBody3D

@export var obj_name: String = "Bed"

#var outline : MeshInstance3D
#var selected : bool = false
#
## Called when the node enters the scene tree for the first time.
#func _ready():
	#outline = get_node("Bed/MeshInstance3D")
	#var p = get_parent().get_node("Player")
	#p.connect("interact_object", _set_selected)
#
#func _set_selected(object):
	#selected = self == object
#
#func _process(_delta):
	#outline.visible = selected
