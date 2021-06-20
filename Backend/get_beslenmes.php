<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";

$conn = mysqli_connect($servername, $username, $password, $dbname);
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}
$secretToken = $_GET["secretToken"];
$username = $_GET["username"];
$password = $_GET["password"];
if($secretToken != 'A.t541541'){
    die("Connection failed: secretTokenError ");
}
$sql_sorgu = "SELECT user_username, user_password,user_isTrainer FROM user_login";
$result = mysqli_query($conn, $sql_sorgu);
if (mysqli_num_rows($result) > 0) {
    // output data of each row
    while($row = mysqli_fetch_assoc($result)) {
        if($username == $row["user_username"] && $password == $row["user_password"] && "1" == $row["user_isTrainer"])
        {
            $sql= "SELECT id,beslenme_adi FROM beslenmeler ORDER BY beslenme_adi ASC";
            $result2 = mysqli_query($conn, $sql);
            if (mysqli_num_rows($result2) > 0) {
                while($row2 = mysqli_fetch_assoc($result2)) {
                        echo $row2["beslenme_adi"]."<br>";
                }
                die();
            }
        }
    }
}
$conn->close();

echo "Hatalı kullanıcı adı.";
die();
?>