<?php
    require_once(dirname(__FILE__) . "/../includes/Logger.php");
    $logger = new Logger();

    $response = array();
    if (!isset($_POST) || !isset($_POST["Device_id"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["Favourite"] = false;
        $response["Login"] = "";
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Bad parameters.", $_POST);
        exit;
    }

    $device_id = $_POST["Device_id"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $result = $deviceRepo->GetFavourite($device_id);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        $response["Favourite"] = false;
        $response["Login"] = "";
        echo json_encode($response, JSON_PRETTY_PRINT);
        $logger->Error("Exception when getting Favourite state: " . $e->getMessage());
        exit;
    }

    if(empty($result))
    {
        $response["Success"] = true;
        $response["Error"] = "Cannot find device: " . $device_id;
        $response["Favourite"] = false;
        $response["Login"] = "";
        $logger->Info("Cannod find device: " . $device_id);
    }
    elseif($result[0]["Is_favourite"] == "0")
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Favourite"] = false;
        $response["Login"] = $result[0]["User_login"];
        $logger->Info("Device is not Favourite: " . $device_id);
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Favourite"] = true;
        $response["Login"] = $result[0]["User_login"];
        $logger->Info("Device is Favourite: " . $device_id);
    }
    echo json_encode($response, JSON_PRETTY_PRINT);
?>
