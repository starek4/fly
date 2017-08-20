<?php

    $response = array();

    if (!isset($_POST) || !isset($_POST["Login"]) || !isset($_POST["Password"]))
    {
        $response["Status"] = "bad_request";
        $response["Valid"] = "none";
    }
    else
    {
        $login = $_POST["Login"];
        $password = $_POST["Password"];
    
        require_once(__DIR__ . "/../db/UserRepository.php");
    
        $userRepo = new UserRepository();
        
        // Verify, if user exists, then check if the password is valid
        $validUser = $userRepo->CheckIfUserExists($login);
        $validCredentials = $userRepo->CheckPassword($login, $password);
        
        if($validUser == true){
            if($validCredentials == false){
                $response["Status"] = "pass";
                $response["Valid"] = "no";
            }
            else{
                $response["Status"] = "pass";
                $response["Valid"] = "yes";
            }
        }
        else{
            $response["Status"] = "pass";
            $response["Valid"] = "no";
        }
    }
    echo json_encode($response, JSON_PRETTY_PRINT);

?>