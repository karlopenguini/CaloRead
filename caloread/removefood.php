<?php 

include_once('connects.php');
$foodID = $_GET['foodID'];

$result = mysqli_query($con,"DELETE FROM foodtbl WHERE foodID = $foodID");

echo "Data Removed";

?>