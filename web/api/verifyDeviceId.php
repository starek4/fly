<?php
    require_once(dirname(__FILE__) . "/../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Device_id"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["IsRegistered"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
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
        $logger->Error("Exception when verifying device id " . $device_id . ": " . $e->getMessage());
        exit;
    }

    if(empty($result))
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["IsRegistered"] = false;
        $logger->Info("Device is not registered: " . $device_id);
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["IsRegistered"] = true;
        $logger->Info("Device is registered: " . $device_id);
    }
    echo json_encode($response, JSON_PRETTY_PRINT);

?>