<?php

include_once('connects.php');
$username = $_GET['uname'];
$type = $_GET['type'];
$date = $_GET['date'];
$query = "SELECT * FROM `mealtbl` WHERE username = '$username' AND type = '$type' AND date = '$date'";
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