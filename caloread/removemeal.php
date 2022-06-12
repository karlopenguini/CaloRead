<?php 

include_once('connects.php');
$mealID = $_GET['mealID'];

$result = mysqli_query($con,"DELETE FROM mealtbl WHERE mealID = $mealID");

echo "Data Removed";

?>