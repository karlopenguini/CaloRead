<?php 

include_once('connects.php');
$username = $_GET['uname'];
$calories = $_GET['kcal'];
$grams = $_GET['grams'];
$protein = $_GET['protein'];
$carbs = $_GET['carbs'];
$fat = $_GET['fat'];
$foodname = $_GET['name'];

$result = mysqli_query($con,"INSERT INTO foodtbl (username, calories, grams, protein, carbs, fat, foodname) VALUES ('$username', $calories, $grams, $protein, $carbs, $fat, '$foodname')");

echo "Data Inserted";

?>