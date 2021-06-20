<?php
setlocale(LC_ALL, 'tr_TR');
$servername = "";
$username = "";
$password = "";
$dbname = "";

$conn = mysqli_connect($servername, $username, $password, $dbname);
$conn->set_charset("utf8");
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}
$secretToken = $_GET["secretToken"];
$username = $_GET["username"];
$password = $_GET["password"];
$person = $_GET["person"];
$gun = $_GET["gun"];
$besin = $_GET["besinAdi"];
$miktar = $_GET["miktar"];
$person_id = "";
if($secretToken != 'A.t541541'){
    die("Connection failed: secretTokenError ");
}
$sql_sorgu = "SELECT user_id,user_username, user_password,user_isTrainer FROM user_login";
$result = mysqli_query($conn, $sql_sorgu);
if (mysqli_num_rows($result) > 0) {
    // output data of each row
    while($row = mysqli_fetch_assoc($result)) {
        if($username == $row["user_username"] && $password == $row["user_password"] && "1" == $row["user_isTrainer"])
        {
            $sqll = "SELECT user_id,user_username,user_isTrainer FROM user_login WHERE user_username= '$person'";
            $resultt = mysqli_query($conn, $sqll);
            while($roww = mysqli_fetch_assoc($resultt)) {
                if($person == $roww["user_username"] && "0" == $roww["user_isTrainer"])
                {
                    $person_id = $roww["user_id"];
                    break;
                }
            }
            if($person_id == ""){
                echo "Böyle bir person yok.";
                die();
            }
            $sql = "INSERT INTO ub_".$person_id." (gun, tanim, miktar)
                    VALUES ('{$gun}', '{$besin}', '{$miktar}')";

            if ($conn->query($sql) === TRUE) {
                echo "Kayıt başarılı.";
                die();
            } else {
                echo "Error: " . $sql . "<br>" . $conn->error;
                die();
            }
        }
    }
}

$conn->close();

echo "Hatalı kullanıcı adı.";
die();
?>
