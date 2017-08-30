<?php
require_once(__DIR__ . "/db/UserRepository.php");

if (isset($_POST["submit"]))
{
    try
    {
        $userRepository = new UserRepository();
        $check = $userRepository->CheckIfUserExists($_POST["login"]);
    }
    catch (Exception $e)
    {
        echo "Cannot verify user" . $e->getMessage();
        exit;
    }

    if($check)
    {
        echo "<p style=\"color:red;\">The user already exists! </p>";
    }
    else
    {
        try
        {
            $userRepository->AddUser($_POST["login"], $_POST["password"], $_POST["e-mail"]);
        }
        catch (Exception $e)
        {
            echo "Cannot add user: " . $e->getMessage();
            exit;
        }
        echo "<p style=\"color:green;\">Registration was successful!</p>";
    }
}

?>

<form method="POST">
    Login:<br>
    <input type="text" name="login" required><br>
    Password:<br>
    <input type="password" name="password" required><br>
    E-mail:<br>
    <input type="e-mail" name="e-mail">
    <input name="submit" type="submit" value="Submit">
</form>
<a href="./login.php"> Log in </a>