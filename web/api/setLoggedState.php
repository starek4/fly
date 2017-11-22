<?php
    require_once(dirname(__FILE__) . "/../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Device_id"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $device_id = $_POST["Device_id"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $result = $deviceRepo->SetLoggedState($device_id);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Exception when setting logged state: " . $e->getMessage());
    }

    $response["Success"] = true;
    $response["Error"] = "";
    echo json_encode($response, JSON_PRETTY_PRINT);
    $logger->Info("Device set logged state successfully: " . $device_id);
?>