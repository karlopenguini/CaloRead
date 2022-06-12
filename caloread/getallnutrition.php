<?php

include_once('connects.php');
$username = $_GET['uname'];
$date = $_GET['date'];
$query = "SELECT DISTINCT mealtbl.type, mealtbl.servings, foodtbl.calories, foodtbl.protein, foodtbl.carbs, foodtbl.fat FROM usertbl INNER JOIN mealtbl ON '$username' = mealtbl.username INNER JOIN foodtbl ON mealtbl.foodID = foodtbl.foodID WHERE mealtbl.date = '$date'";
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