<?php

    $response = array();

    if (!isset($_POST) || !isset($_POST["Device_id"]) || !isset($_POST["Login"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["Shutdown"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    $device_id = $_POST["Device_id"];
    $login = $_POST["Login"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $result = $deviceRepo->GetShutdownPending($login, $device_id);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        $response["Shutdown"] = false;
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    if(empty($result))
    {
        $response["Success"] = false;
        $response["Error"] = "Cannot find device: " . $device_id;
        $response["Shutdown"] = false;
    }
    elseif($result[0]["Is_shutdown_pending"] == "0")
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Shutdown"] = false;
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Shutdown"] = true;
    }
    echo json_encode($response, JSON_PRETTY_PRINT);
?>
