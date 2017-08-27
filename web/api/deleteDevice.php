<?php
    $response = array();

    $login = $_POST["Login"];
    $device_id = $_POST["Device_id"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    $deviceRepo = new DeviceRepository();

    $result = $deviceRepo->DelDevice($login, $device_id);
    
    if($result === false)    $response["Result"] = "no";
    else    $response["Result"] = "yes";

    echo json_encode($response, JSON_PRETTY_PRINT);
?>