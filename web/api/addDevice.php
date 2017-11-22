<?php
    $response = array();

    if (!isset($_POST) || !isset($_POST["Login"]) || !isset($_POST["Device_id"]) || !isset($_POST["Name"]) || !isset($_POST["Shutdownable"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    $login = $_POST["Login"];
    $device_id = $_POST["Device_id"];
    $device_name = $_POST["Name"];
    $shutdownable = $_POST["Shutdownable"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $shutdownable = $shutdownable == "True" ? true : false;
        $result = $deviceRepo->AddDevice($login, $device_id, $device_name, $shutdownable);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    $response["Success"] = true;
    $response["Error"] = "";
    echo json_encode($response, JSON_PRETTY_PRINT);
?>
