<?php 

include_once('connects.php');
$username = $_GET['uname'];
$password = $_GET['pword'];
$weight = $_GET['weight'];
$height = $_GET['height'];
$age = $_GET['age'];
$gender = $_GET['gender'];
$goal = $_GET['goal'];

$result = mysqli_query($con,"INSERT INTO usertbl (username, password, weight, height, gender, age, calorie_goal) VALUES ('$username', '$password', $weight, $height, '$gender', $age, $goal)");


echo "Data Inserted";

?>