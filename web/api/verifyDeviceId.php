<?php

    $response = array();

    if (!isset($_POST) || !isset($_POST["Device_id"]))
    {
        $response["Status"] = "bad_request";
        $response["IsRegistered"] = "none";
    }
    else
    {
        $device_id = $_POST["Device_id"];
    
        require_once(__DIR__ . "/../db/DeviceRepository.php");
        $deviceRepo = new DeviceRepository();
    
        $result = $deviceRepo->VerifyDeviceId($device_id);
        if(empty($result)){
                $response["Status"] = "pass";
                $response["IsRegistered"] = "no";
        }
        else{
            $response["Status"] = "pass";
            $response["IsRegistered"] = "yes";
        }
    }
    echo json_encode($response, JSON_PRETTY_PRINT);

?>