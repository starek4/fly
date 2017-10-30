<?php
require_once(__DIR__ . "/Session.php");

$session = new Session();
if (!$session->CheckIfUserIsLogged())
    header("Location: login.php");

if (isset($_POST["submit"]))
{
    header("Location: logout.php");
}

require_once(__DIR__ . "/table.php");
?>

<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Fly - table</title>
</head>

<body>
    <form method="POST">
    <input name="submit" type="submit" value="Logout">
    </form>
</body>
</html>

