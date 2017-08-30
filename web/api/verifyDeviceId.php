<?php

    $response = array();

    if (!isset($_POST) || !isset($_POST["Device_id"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["IsRegistered"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    $device_id = $_POST["Device_id"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $result = $deviceRepo->VerifyDeviceId($device_id);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        $response["IsRegistered"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    if(empty($result))
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["IsRegistered"] = false;
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["IsRegistered"] = true;
    }
    echo json_encode($response, JSON_PRETTY_PRINT);

?>