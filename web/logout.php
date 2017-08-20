<?php
require_once(__DIR__ . "/Session.php");
$session = new Session();
$session->LogoutUser();
header("Location: login.php")
?>