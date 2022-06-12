<?php 

include_once('connects.php');
$foodID = $_GET['foodID'];
$calories = $_GET['kcal'];
$grams = $_GET['grams'];
$protein = $_GET['protein'];
$carbs = $_GET['carbs'];
$fat = $_GET['fat'];
$foodname = $_GET['name'];

$result = mysqli_query($con,"UPDATE foodtbl SET calories = $calories, grams = $grams, protein = $protein, carbs = $carbs, fat = $fat, foodname = '$foodname' WHERE foodID = $foodID");

echo "Data Updated";

?>