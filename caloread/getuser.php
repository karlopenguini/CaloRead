<?php 

include_once('connects.php');
$username = $_GET['uname'];

$query = "SELECT weight, height, gender, age, calorie_goal FROM usertbl WHERE username = '{$username}'";
$check=mysqli_query($con,$query);
$row=mysqli_num_rows($check);
$myArray = array();

if($check == FALSE) { 
    echo "NO USER FOUND";
}

while($row=mysqli_fetch_array($check))
{
  	$myArray[] = $row;
}

echo json_encode($myArray);

?>