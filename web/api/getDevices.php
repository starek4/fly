<?php
    require_once(dirname(__FILE__) . "/../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Login"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["Devices"] = null;
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $login = $_POST["Login"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $result = $deviceRepo->GetDevicesByLogin($login);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Exception when getting device: " . $e->getMessage());
        exit;
    }

    $index = 0;
    $devices = array();
    if ($result != NULL)
    {
        foreach($result as $device)
        {
            $devices[$index]["DeviceId"] = $device["Device_id"];
            $devices[$index]["Name"] = $device["Name"];
            $devices[$index]["Status"] = $device["Status"];
            $devices[$index]["LastActive"] = $device["Last_active"];
            $index = $index + 1;
        }
    }
    $response["Success"] = true;
    $response["Error"] = "";
    $response["Devices"] = $devices;
    echo json_encode($response, JSON_PRETTY_PRINT);
    $logger->Info("Devices successfully getted for user: " . $login, $devices);
?>
