<?php
    require_once(dirname(__FILE__) . "/../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Device_id"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["Shutdown"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $device_id = $_POST["Device_id"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $deviceRepo->UpdateLastActive($device_id);
        $result = $deviceRepo->GetShutdownPending($device_id);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        $response["Shutdown"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Exception when getting shutdown pending state: " . $e->getMessage());
        exit;
    }

    if(empty($result))
    {
        $response["Success"] = false;
        $response["Error"] = "Cannot find device: " . $device_id;
        $response["Shutdown"] = false;
        $logger->Info("Cannod find device: " . $device_id);
    }
    elseif($result[0]["Is_shutdown_pending"] == "0")
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Shutdown"] = false;
        $logger->Info("Device is not in shutdown state: " . $device_id);
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Shutdown"] = true;
        $logger->Info("Device is in shutdown state: " . $device_id);
    }
    echo json_encode($response, JSON_PRETTY_PRINT);
?>
