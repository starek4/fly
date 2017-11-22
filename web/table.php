<?php

    require_once(dirname(__FILE__) . "/db/DeviceRepository.php");

    try
    {
        $deviceRep = new DeviceRepository();
    }
    catch (Exception $e)
    {
        echo $e->getMessage();
        $logger->Error("Cannot create device repository on table page.");
        exit;
    }

    try
    {
        $deviceList = $deviceRep->GetDevicesByLogin($_SESSION["login"]);
    }
    catch (Exception $e)
    {
        echo "Cannot get user's devices:" . $e->getMessage();
        $logger->Error("Cannot get devices on table page.");
        exit;
    }

    if($deviceList == NULL)
    {
?>
        <div class="wrapper">
            <h1>No devices were found!</h1>
        </div>
<?php
    }
    else
    {
?>
        <div class="wrapper">
        <div class="table">
            <div class="row header green">
                <div class="cell">Device name</div>
                <div class="cell">State</div>
                <div class="cell">Actions</div>
            </div>

<?php
            foreach($deviceList as $device)
            {
?>
                <div class="row">
                    <div class="cell"><?php echo $device["Name"] ?></div>
                    <div class="cell">
                        <?php
                            $time = strtotime($device['Last_active']);
                            $curtime = time();
                            if (($curtime - $time) < 60)      // 1 minutes
                                echo '<img width="20" src="images/green.png"></img>';
                            else
                                echo '<img width="20" src="images/red.png"></img>';
                         ?>
                    </div>
                    <div class="cell">
                        <button onclick="SetShutdownState(this.name)" id="shutdown" name="<?php echo $device["Device_id"] ?>">Shutdown</button>
                        <button onclick="DeleteDevice('<?php echo$_SESSION["login"] ?>', this.name);" id="deleteDevice" name="<?php echo $device["Device_id"] ?>">Delete device</button>
                    </div>
                </div>
<?php
            }
        }
?>
        </div>
        </div>

        <!-- Button handlers -->
        <script src="./js/shutdownButtonHandler.js"></script>
        <script src="./js/deleteDeviceButtonHandler.js"></script>