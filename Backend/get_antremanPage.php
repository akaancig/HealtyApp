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
$person_id = "";
if($secretToken != 'A.t541541'){
    die("Connection failed: secretTokenError ");
}
$sql_sorgu = "SELECT user_id,user_username, user_password,user_isTrainer FROM user_login";
$result = mysqli_query($conn, $sql_sorgu);
if (mysqli_num_rows($result) > 0) {
    while($row = mysqli_fetch_assoc($result)) {
        if($username == $row["user_username"] && $password == $row["user_password"] && "0" == $row["user_isTrainer"])
        {
            $person_id = $row["user_id"];
            if($person_id == ""){
                echo "Böyle bir person yok.";
                die();
            }
            try {
                $sql= "SELECT * FROM ua_".$person_id." ORDER BY gun ASC";
                $result2 = mysqli_query($conn, $sql);
                if(mysqli_num_rows($result2) == 0)
                    die();
                else if (mysqli_num_rows($result2) > 0) {
                    while($row2 = mysqli_fetch_assoc($result2)) {
                        $str=$row2["id"].",".$row2["gun"].",".$row2["tanim"].",".$row2["miktar"]."<br>";
                        echo $str;
                    }
                    die();
                }
            }
            catch (Exception $e){
                die();
            }
        }
    }
}

$conn->close();

echo "Hatalı kullanıcı adı.";
die();
?>