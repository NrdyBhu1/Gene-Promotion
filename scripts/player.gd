extends CharacterBody3D

signal interact_object

@export var speed : int = 5
@export var fall_acceleration : int = 75

var target_velocity :Vector3 = Vector3.ZERO
var cam : Camera3D
var raycast : RayCast3D
var text_label : Label

func _ready():
	cam = get_node("Camera3D")
	raycast = get_node("RayCast3D")
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	text_label = get_parent().get_node("Control/Label")
	text_label.text = ""
	
func _input(event):
	if (event is InputEventMouseMotion):
		rotation = Vector3(rotation.x - event.relative.y * 0.01,rotation.y - event.relative.x * 0.01,rotation.z)

func _process(_delta):
	if (raycast.is_colliding()):
		var collider = raycast.get_collider()
		emit_signal("interact_object", collider)
		text_label.text = collider.obj_name
	else:
		emit_signal("interact_object", null)
		text_label.text = ""

func _physics_process(delta):
	var direction = Vector3.ZERO;
	if (Input.is_action_pressed("move_right")):
		direction.x += 1.0;
	if (Input.is_action_pressed("move_left")):
		direction.x -= 1.0;
	if (Input.is_action_pressed("move_backward")):
		direction.z += 1.0;
	if (Input.is_action_pressed("move_forward")):
		direction.z -= 1.0;
	
	if (direction != Vector3.ZERO):
		direction = direction.normalized()
		direction = (cam.global_transform.basis * direction).normalized()

	target_velocity.x = direction.x * speed
	target_velocity.z = direction.z * speed
	# Vertical Velocity
	if not is_on_floor(): # If in the air, fall towards the floor. Literally gravity
		target_velocity.y = target_velocity.y - (fall_acceleration * delta)
		
	velocity = target_velocity

	move_and_slide()
