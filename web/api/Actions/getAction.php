<?php
    require_once(dirname(__FILE__) . "/../../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Device_id"]) || !isset($_POST["Action"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["Action"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $device_id = $_POST["Device_id"];
    $action = $_POST["Action"];

    require_once(__DIR__ . "/../../db/DeviceRepository.php");
    require_once(__DIR__ . "/ActionsMapper.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $actionsMapper = new ActionsMapper();

        $deviceRepo->UpdateLastActive($device_id);
        $result = $deviceRepo->GetActionPending($device_id, $action);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        $response["Action"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Exception when getting action pending state: " . $e->getMessage());
        exit;
    }

    if(empty($result))
    {
        $response["Success"] = false;
        $response["Error"] = "Cannot find device: " . $device_id;
        $response["Action"] = false;
        $logger->Info("Cannod find device: " . $device_id);
    }
    elseif($result == NULL)
    {
        $response["Success"] = false;
        $response["Error"] = "Cannod find action: " . $action;
        $response["Action"] = false;
        $logger->Info("Cannod find action: " . $action);
    }
    elseif($result[0][$actionsMapper->GetDbActionName($action)] == "0")
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Action"] = false;
        $logger->Info("Device is not in action state: " . $device_id);
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Action"] = true;
        $logger->Info("Device is in action state: " . $device_id);
    }
    echo json_encode($response, JSON_PRETTY_PRINT);
?>
