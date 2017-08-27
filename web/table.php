<?php

    require_once(__DIR__ . "/db/DeviceRepository.php");

    $deviceRep = new DeviceRepository();

    $deviceList = $deviceRep->GetDevicesByLogin($_SESSION["login"]);
    if($deviceList == NULL)
    {
        echo "No devices were found!";
    }
    else
    {
        echo "<table id=\"main_table\">";
            echo "<tr>";
                echo "<th>Device name</th>";
                echo "<th>Action</th>";
            echo "</tr>";
            foreach($deviceList as $device){
                echo "<tr>";
                    echo "<td>".$device["Name"]."</td>";
                    echo "<td><button onclick=\"SetShutdownState(this.name)\" id=\"shutdown\" name=\"" . $device["Device_id"] . "\">Shutdown</button></td>";
                    echo "<td><button onclick=\"DeleteDevice('".$_SESSION["login"]."', this.name);\" id=\"deleteDevice\" name=\"" . $device["Device_id"] . "\">Delete device</button></td>";
                    echo "</tr>";
            }
        echo "</table>";

        // JS sources
        echo '<script type="text/javascript" src="./js/jquery-3.2.1.min.js"></script>';

        // Button handlers
        echo '<script src="./js/shutdownButtonHandler.js"></script>';
        echo '<script src="./js/deleteDeviceButtonHandler.js"></script>';
    }
?>