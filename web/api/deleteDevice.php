<?php
    require_once(dirname(__FILE__) . "/../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Device_id"]) || !isset($_POST["Login"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $login = $_POST["Login"];
    $device_id = $_POST["Device_id"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $result = $deviceRepo->DelDevice($login, $device_id);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Exception when deleting device: " . $e->getMessage());
        exit;
    }

    $response["Success"] = true;
    $response["Error"] = "";
    echo json_encode($response, JSON_PRETTY_PRINT);
    $logger->Info("Device deleted: " . $device_id);
?>
