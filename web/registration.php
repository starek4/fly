<?php
require_once(dirname(__FILE__) . "/db/UserRepository.php");
$status = "";
if (isset($_POST["submit"]))
{
    try
    {
        $userRepository = new UserRepository();
        $check = $userRepository->CheckIfUserExists($_POST["login"]);
    }
    catch (Exception $e)
    {
        $status = "<font color=\"red\">Cannot verify user</font>";
    }

    if($check)
    {
        $status = "<font color=\"red\">The user already exists!</font>";
    }
    else
    {
        try
        {
            $userRepository->AddUser($_POST["login"], $_POST["password"], $_POST["e-mail"]);
        }
        catch (Exception $e)
        {
            $status = "<font color=\"red\">Cannot add user</font>";
        }
        $status = "<font color=\"green\">Registration was successful!</font>";
    }
}
?>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Fly - registration</title>
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon">

    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="css/animate.min.css">
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/login.css">

    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>

<body>
    <div class="container login-form">
        <h2 class="login-title">- Fly manager registration -</h2>
        <div class="panel panel-default">
            <div class="panel-body">
                <form method="POST">
                    <?php echo $status ?>
                    <div class="input-group login-userinput">
                        <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                        <input id="txtUser" type="text" class="form-control" name="login" placeholder="Username">
                    </div>
                    <div class="input-group login-userinput">
                        <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                        <input  id="txtPassword" type="password" class="form-control" name="password" placeholder="Password">
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon"><span class="glyphicon glyphicon-envelope"></span></span>
                        <input  id="txtEmail" type="email" class="form-control" name="e-mail" placeholder="E-mail">
                    </div>
                    <button name="submit" class="btn btn-primary btn-block login-button" type="submit"><i class="fa fa-user-plus"></i> Register me</button>
                    <div class="checkbox login-options">
                        <a href="./login.php" class="login-forgot">Login here</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
