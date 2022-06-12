<?php

include_once('connects.php');
$mealID = $_GET['mealID'];
$query = "SELECT * FROM `mealtbl` WHERE mealID = $mealID";
$check=mysqli_query($con,$query);
$row=mysqli_num_rows($check);
$myArray = array();

if($check == FALSE) { 
    echo ".".$row."."; // TODO: better error handling
}

  while($row=mysqli_fetch_array($check))
  	{
  	
	 $myArray[] = $row;
	
  	}
  echo json_encode($myArray);

?>