<?php
    $response = array();

    if (!isset($_POST) || !isset($_POST["Device_id"]))
    {
        $response["Success"] = false;
        $response["Error"] = "Bad parameters.";
        $response["Logged"] = false;
        $response["Login"] = "";
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    $device_id = $_POST["Device_id"];

    require_once(__DIR__ . "/../db/DeviceRepository.php");

    try
    {
        $deviceRepo = new DeviceRepository();
        $result = $deviceRepo->GetLoggedState($device_id);
    }
    catch (Exception $e)
    {
        $response["Success"] = false;
        $response["Error"] = $e->getMessage();
        $response["Logged"] = false;
        $response["Login"] = "";
        echo json_encode($response, JSON_PRETTY_PRINT);
        exit;
    }

    if(empty($result))
    {
        $response["Success"] = true;
        $response["Error"] = "Cannot find device: " . $device_id;
        $response["Logged"] = false;
        $response["Login"] = "";
    }
    elseif($result[0]["Is_logged"] == "0")
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Logged"] = false;
        $response["Login"] = $result[0]["User_login"];
    }
    else
    {
        $response["Success"] = true;
        $response["Error"] = "";
        $response["Logged"] = true;
        $response["Login"] = $result[0]["User_login"];
    }
    echo json_encode($response, JSON_PRETTY_PRINT);
?>
