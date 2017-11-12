<?php
    require_once(dirname(__FILE__) . "/Session.php");
    $session = new Session();
    $session->LogoutUser();
    header("Location: login.php")
?>