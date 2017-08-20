<?php

    $response = array();

    if (!isset($_POST) || !isset($_POST["Device_id"]) || !isset($_POST["Login"]))
    {
        $response["Status"] = "bad_request";
        $response["Shutdown"] = "none";
    }
    else
    {
        $device_id = $_POST["Device_id"];
        $login = $_POST["Login"];
    
        require_once(__DIR__ . "/../db/DeviceRepository.php");
    
        $deviceRepo = new DeviceRepository();
    
        $result = $deviceRepo->GetShutdownPending($login, $device_id);
        if(empty($result)){
             $response["Status"] = "error";
             $response["Shutdown"] = "none";
        }
        elseif($result[0]["Is_shutdown_pending"] == "0"){
            $response["Status"] = "pass";
            $response["Shutdown"] = "no";
        }
        else{
            $response["Status"] = "pass";
            $response["Shutdown"] = "yes";
        }
    }
    echo json_encode($response, JSON_PRETTY_PRINT);
?>