<?php
require_once(__DIR__ . "/Session.php");
require_once(__DIR__ . "/db/UserRepository.php");

if (isset($_POST["submit"]))
{
    $session = new Session();
    try
    {
        $userRepository = new UserRepository();
    }
    catch (Exception $e)
    {
        echo $e->getMessage();
        exit;
    }

    try
    {
        $check = $userRepository->CheckPassword($_POST["login"], $_POST["password"]);
    }
    catch (Exception $e)
    {
        echo "Cannot verify user" . $e->getMessage();
        exit;
    }
    
    if ($check)
    {
        $session->LoginUser($_POST["login"]);
        header("Location: index.php");
    }
    else
        echo "Wrong password!";
}

?>


<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Fly - login</title>
</head>

<body>
    <form method="POST">
        Login:<br>
        <input type="text" name="login" required><br>
        Password:<br>
        <input type="password" name="password" required><br>
        <input name="submit" type="submit" value="Submit">
    </form>
    <a href="./registration.php"> Register here </a>
</body>
</html>