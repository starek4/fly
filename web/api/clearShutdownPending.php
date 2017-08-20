<?php

    $response = array();

    if (!isset($_POST) || !isset($_POST["Device_id"]))
    {
        $response["Status"] = "error";
    }
    else
    {
        $device_id = $_POST["Device_id"];
    
        require_once(__DIR__ . "/../db/DeviceRepository.php");
        $deviceRepo = new DeviceRepository();
    
        $result = $deviceRepo->ClearShutdownPending($device_id);
        if ($result === false) $response["Status"] = "no";
        else $response["Status"] = "yes";
    }
    echo json_encode($response, JSON_PRETTY_PRINT);
?>