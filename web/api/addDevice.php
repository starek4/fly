<?php
    require_once(dirname(__FILE__) . "/../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Login"]) || !isset($_POST["Device_id"]) || !isset($_POST["Name"]) || !isset($_POST["Actionable"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $login = $_POST["Login"];
    $device_id = $_POST["Device_id"];
    $device_name = $_POST["Name"];
    $actionable = $_POST["Actionable"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $actionable = $actionable == "True" ? true : false;
        $result = $deviceRepo->AddDevice($login, $device_id, $device_name, $actionable);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Exception when adding device: " . $e->getMessage());
        exit;
    }

    $response["Success"] = true;
    $response["Error"] = "";
    echo json_encode($response, JSON_PRETTY_PRINT);
    $logger->Info("Added device: " . $device_id);
?>
