<?php

include_once('connects.php');
$foodID = $_GET['foodID'];

$query = "SELECT * FROM `foodtbl` WHERE foodID = $foodID";
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