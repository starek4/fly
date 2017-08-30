<?php

    $response = array();

    if (!isset($_POST) || !isset($_POST["Login"]) || !isset($_POST["Password"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["Valid"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    $login = $_POST["Login"];
    $password = $_POST["Password"];

    require_once(__DIR__ . "/../db/UserRepository.php");

    try
    {
        $userRepo = new UserRepository();
        
            // Verify, if user exists, then check if the password is valid
            $validUser = $userRepo->CheckIfUserExists($login);
            $validCredentials = $userRepo->CheckPassword($login, $password);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        $response["Valid"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }
    
    if($validUser == true)
    {
        if($validCredentials == false)
        {
            $response["Success"] = true;
            $response["Error"] = "";
            $response["Valid"] = false;
        }
        else
        {
            $response["Success"] = true;
            $response["Error"] = "";
            $response["Valid"] = true;
        }
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Valid"] = false;
    }
    echo json_encode($response, JSON_PRETTY_PRINT);

?>