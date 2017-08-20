<?php
require_once(__DIR__ . "/Session.php");
require_once(__DIR__ . "/db/UserRepository.php");

if (isset($_POST["submit"]))
{
    $session = new Session();
    $userRepository = new UserRepository();

    if ($userRepository->CheckPassword($_POST["login"], $_POST["password"]))
    {
        $session->LoginUser($_POST["login"]);
        header("Location: index.php");
    }
    else
        echo "Wrong password!";
}

?>
<form method="POST">
    Login:<br>
    <input type="text" name="login" required><br>
    Password:<br>
    <input type="password" name="password" required><br>
    <input name="submit" type="submit" value="Submit">
</form>
<a href="./registration.php"> Register here </a>