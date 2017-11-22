<?php
require_once(dirname(__FILE__) . "/includes/Session.php");

$session = new Session();
if (!$session->CheckIfUserIsLogged())
    header("Location: login.php");
?>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon">
    <title>Fly - manager</title>

    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="css/animate.min.css">
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/table.css">

    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>

<body>
    <div id="topBar">
        <button class="btn btn-default" id="logoutButton" type="button" onclick="location.href='./logout.php';"><i class="fa fa-sign-out"></i> Logout</button>
    </div>
    <?php require_once(dirname(__FILE__) . "/table.php"); ?>
</body>
</html>
