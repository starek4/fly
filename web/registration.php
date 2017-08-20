<?php
require_once(__DIR__ . "/db/UserRepository.php");

if (isset($_POST["submit"]))
{
    $userRepository = new UserRepository();

    $variable = $userRepository->CheckIfUserExists($_POST["login"]);;

    if($userRepository->CheckIfUserExists($_POST["login"]) == true){
        echo "<p style=\"color:red;\">The user already exists! </p>";
    }
    else{
        $userRepository->AddUser($_POST["login"], $_POST["password"], $_POST["e-mail"]);
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

<?php
?>