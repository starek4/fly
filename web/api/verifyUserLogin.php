<?php
    require_once(dirname(__FILE__) . "/../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Login"]) || !isset($_POST["Password"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["Valid"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $login = $_POST["Login"];
    $password = $_POST["Password"];

    require_once(__DIR__ . "/../db/UserRepository.php");

    try
    {
        $userRepo = new UserRepository();
        $validUser = $userRepo->CheckIfUserExists($login);
        $validCredentials = $userRepo->CheckPassword($login, $password);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        $response["Valid"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Exception when verifying user " . $login . ": " . $e->getMessage());
        exit;
    }

    if($validUser == true)
    {
        if($validCredentials == false)
        {
            $response["Success"] = true;
            $response["Error"] = "";
            $response["Valid"] = false;
            $logger->Info("Trying to login with invalid credentials for user: " . $login);
        }
        else
        {
            $response["Success"] = true;
            $response["Error"] = "";
            $response["Valid"] = true;
            $logger->Info("Logged with valid credentials for user: " . $login);
        }
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Valid"] = false;
        $logger->Info("Invalid user: " . $login);
    }
    echo json_encode($response, JSON_PRETTY_PRINT);

?>