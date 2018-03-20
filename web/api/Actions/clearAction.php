<?php
    require_once(dirname(__FILE__) . "/../../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Device_id"]) || !isset($_POST["Action"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $device_id = $_POST["Device_id"];
    $action = $_POST["Action"];

    require_once(__DIR__ . "/../../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $result = $deviceRepo->ClearActionPending($device_id, $action);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    if($result == NULL)
    {
        $response["Success"] = false;
        $response["Error"] = "Cannod find action: " . $action;
        $response["Action"] = false;
        $logger->Info("Cannod find action: " . $action);
    }

    $response["Success"] = true;
    $response["Error"] = "";
    echo json_encode($response, JSON_PRETTY_PRINT);
?>
