<?php
    $response = array();

    $login = $_POST["Login"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    $deviceRepo = new DeviceRepository();

    $result = $deviceRepo->GetDevicesByLogin($login);
    $index = 0;
    $devices = array();
    foreach($result as $device)
    {
        $devices[$index]["DeviceId"] = $device["Device_id"];
        $devices[$index]["Name"] = $device["Name"];
        $devices[$index]["Status"] = $device["Status"];
        $index = $index + 1;
    }
    $response["Devices"] = $devices;
    echo json_encode($response, JSON_PRETTY_PRINT);
?>