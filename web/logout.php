<?php
    require_once(dirname(__FILE__) . "/includes/Session.php");
    $session = new Session();
    $session->LogoutUser();
    header("Location: login.php")
?>