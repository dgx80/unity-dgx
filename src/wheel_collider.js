var wheelForm : Transform;
var wheelCol : WheelCollider;
	

function Update () {

	wheelForm.transform.Rotate(0, -(wheelCol.rpm * -6 * Time.deltaTime),0);

}