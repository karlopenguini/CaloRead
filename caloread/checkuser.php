<?php

include_once('connects.php');
$uname = $_GET['uname'];

$query = "SELECT * FROM `usertbl` WHERE username = '$uname'";
$check=mysqli_query($con,$query);
$row=mysqli_num_rows($check);
$myArray = array();

if($check == FALSE) { 
    echo "No Username Found"
}

  while($row=mysqli_fetch_array($check))
  	{
  	
	 $myArray[] = $row;
	
  	}
  echo json_encode($myArray);

?>