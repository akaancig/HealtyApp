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
$besin = $_GET["besin"];
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
            $sql = "INSERT INTO beslenmeler (beslenme_adi)
                    VALUES ('{$besin}')";
            if ($conn->query($sql) === TRUE) {
                echo "Yeni kayıt oluşturuldu.";
            } else {
                echo "Hata.";
            }
            die();
        }
    }
}
$conn->close();

echo "Hatalı kullanıcı adı.";
die();
?>