<?php 

include_once('connects.php');
$mealID = $_GET['mealID'];
$servings = $_GET['servings'];


$result = mysqli_query($con,"UPDATE mealtbl SET servings = $servings WHERE mealID = $mealID");

echo "Data Updated";

?>