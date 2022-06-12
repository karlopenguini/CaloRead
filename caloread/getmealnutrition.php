<?php

include_once('connects.php');
$username = $_GET['uname'];
$type = $_GET['type'];
$query = "SELECT DISTINCT mealtbl.servings, foodtbl.calories, foodtbl.protein, foodtbl.carbs, foodtbl.fat FROM usertbl INNER JOIN mealtbl ON '$username' = mealtbl.username INNER JOIN foodtbl ON mealtbl.foodID = foodtbl.foodID WHERE mealtbl.type = '$type'";
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