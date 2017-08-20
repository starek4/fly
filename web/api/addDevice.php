<?php
    $response = array();

    $login = $_POST["Login"];
    $device_id = $_POST["Device_id"];
    $device_name = $_POST["Name"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    $deviceRepo = new DeviceRepository();

    $result = $deviceRepo->AddDevice($login, $device_id, $device_name);
    if($result === false)    $response["Result"] = "no";
    else    $response["Result"] = "yes";

    echo json_encode($response, JSON_PRETTY_PRINT);
?>