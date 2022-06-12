<?php 

include_once('connects.php');
$foodID = $_GET['foodID'];
$username = $_GET['uname'];
$servings = $_GET['servings'];
$type = $_GET['type'];
$date = $_GET['date'];

$result = mysqli_query($con,"INSERT INTO mealtbl (foodID, username, type, servings, date) VALUES ($foodID, '$username', '$type', $servings, '$date')");

echo "Data Inserted";

?>