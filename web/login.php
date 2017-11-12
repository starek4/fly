<?php
require_once(dirname(__FILE__) . "/Session.php");
require_once(dirname(__FILE__) . "/db/UserRepository.php");

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
        $status = "<font color=\"red\">Cannot verify user!</font>";
    }

    if ($check)
    {
        $session->LoginUser($_POST["login"]);
        header("Location: index.php");
    }
    else
        $status = "<font color=\"red\">Wrong password!</font>";
}

?>


<!DOCTYPE html>
<html>
<head>
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon">

    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="css/animate.min.css">
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/login.css">

    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <meta charset="UTF-8">
    <title>Fly - login</title>
</head>

<body>
    <div class="container login-form">
        <h2 class="login-title">- Fly manager login -</h2>
        <div class="panel panel-default">
            <div class="panel-body">
                <form method="POST">
                    <?php echo $status ?>
                    <div class="input-group login-userinput">
                        <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                        <input id="txtUser" type="text" class="form-control" name="login" placeholder="Username">
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                        <input  id="txtPassword" type="password" class="form-control" name="password" placeholder="Password">
                        <span id="showPassword" class="input-group-btn">
                            <button class="btn btn-default reveal" type="button"><i class="glyphicon glyphicon-eye-open"></i></button>
                        </span>
                    </div>
                    <button name="submit" class="btn btn-primary btn-block login-button" type="submit"><i class="fa fa-sign-in"></i> Login</button>
                    <div class="checkbox login-options">
                        <a href="./registration.php" class="login-forgot">Register here</a>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <script>
        window.onload = function()
        {
            $("#showPassword").hide();
        }

        $("#txtPassword").on('change',function()
        {
            if($("#txtPassword").val())
            {
                $("#showPassword").show();
            }
            else
            {
                $("#showPassword").hide();
            }
        });

        $(".reveal").on('click',function()
        {
            var $pwd = $("#txtPassword");
            if ($pwd.attr('type') === 'password')
            {
                $pwd.attr('type', 'text');
            }
            else
            {
                $pwd.attr('type', 'password');
            }
        });
    </script>


</body>
</html>