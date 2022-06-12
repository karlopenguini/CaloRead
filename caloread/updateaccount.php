<?php 

include_once('connects.php');
$username = $_GET['uname'];
$password = $_GET['pword'];
$weight = $_GET['weight'];
$height = $_GET['height'];
$age = $_GET['age'];
$gender = $_GET['gender'];
$goal = $_GET['goal'];

$result = mysqli_query($con,"UPDATE usertbl SET `password` = $password, `weight` = $weight, height = $height, gender = '$gender', age = $age, calorie_goal = $goal WHERE username = '$username'");


echo "Data Updated";

?>