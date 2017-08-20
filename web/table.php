<?php

    require_once(__DIR__ . "/db/DeviceRepository.php");

    $deviceRep = new DeviceRepository();

    $deviceList = $deviceRep->GetDevicesByLogin($_SESSION["login"]);
    echo "<table>";
        echo "<tr>";
            echo "<th>Device name</th>";
            echo "<th>Action</th>";
        echo "</tr>";
        foreach($deviceList as $device){
            echo "<tr>";
                echo "<td>".$device["Name"]."</td>";
                echo "<td><button onclick=\"SeteShutdownState()\" id=\"shutdown\" name=\"" . $device["Device_id"] . "\">Shutdown</button></td>";
            echo "</tr>";
        }
    echo "</table>";
    echo '<script src="./js/shutdownButoonHandler.js"></script>'
?>