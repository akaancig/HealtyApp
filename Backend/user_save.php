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
$e_mail = $_GET["e_mail"];

if($secretToken != 'A.t541541'){
    die("Connection failed: secretTokenError ");
}
$sql_sorgu = "SELECT user_username, user_email FROM user_login";
$result = mysqli_query($conn, $sql_sorgu);
if (mysqli_num_rows($result) > 0) {
    // output data of each row
    while($row = mysqli_fetch_assoc($result)) {
        if($username == $row["user_username"])
        {
            echo "Böyle bir kullanıcı zaten mevcut.". $username .  "<br>";
            die();
        }
        else if($e_mail == $row["user_email"])
        {
            echo "Böyle bir e-mail zaten mevcut.". $e_mail .  "<br>";
            die();
        }
    }
} else {

}
$sql = "INSERT INTO user_login (user_username, user_password, user_email)
VALUES ('{$username}', '{$password}', '{$e_mail}')";

if ($conn->query($sql) === TRUE) {
    echo "";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
    die();
}

$sql_sorgu = "SELECT user_id,user_username FROM user_login";
$result = mysqli_query($conn, $sql_sorgu);
$person_id = "";
if (mysqli_num_rows($result) > 0) {
    // output data of each row
    while($row = mysqli_fetch_assoc($result)) {
        if($username == $row["user_username"])
        {
            $person_id = $row["user_id"];
        }
    }
}
else{
    die();
}

$sql = "CREATE TABLE ub_".$person_id." (
id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
gun INT NOT NULL,
tanim VARCHAR(60) NOT NULL,
miktar VARCHAR(50) NOT NULL
) CHARACTER SET utf8 COLLATE utf8_turkish_ci";

if ($conn->query($sql) === TRUE) {
    echo "";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
    die();
}
$sql = "CREATE TABLE ua_".$person_id." (
id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
gun INT NOT NULL,
tanim VARCHAR(60) NOT NULL,
miktar VARCHAR(50) NOT NULL
) CHARACTER SET utf8 COLLATE utf8_turkish_ci";

if ($conn->query($sql) === TRUE) {
    echo "";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
    die();
}

echo "Yeni kayıt oluşturuldu.";
$conn->close();
?>