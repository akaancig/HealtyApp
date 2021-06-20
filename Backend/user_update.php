<?php
$servername = "";
$username = "";
$password = "";
$dbname = "";

try {
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

    $sql = "SELECT user_username,user_email FROM user_login";
    $result = mysqli_query($conn, $sql);
    if (mysqli_num_rows($result) > 0) {
        // output data of each row
        while($row = mysqli_fetch_assoc($result)) {
            if($username != $row["user_username"]){
                if($e_mail == $row["user_email"])
                {
                    echo "Böyle bir e-mail zaten mevcut.";
                    die();
                }
            }
        }
    }
    $sql_sorgu = "UPDATE user_login SET user_password = '$password', user_email = '$e_mail' WHERE user_username = '$username'";
    $result = mysqli_query($conn, $sql_sorgu);

    if ($conn->query($sql_sorgu) === TRUE) {
        echo "Kayıt güncellendi.";
    } else {
        echo 'Hata oluştu.';
    }

    $conn->close();
} catch (Exception $e) {
    echo 'Hata oluştu.';
}
?>