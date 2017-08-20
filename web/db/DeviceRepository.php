<?php
require_once(__DIR__ . "/Db.php");

    class DeviceRepository{
        var $db;
        
        function __construct(){
            $this->db = new Db();
        }
        public function AddDevice($login, $device_id, $device_name){
            $this->db->Query("INSERT INTO `Devices` (`Id`, `Device_id`, `Name`, `User_login`) VALUES (NULL, '" . $device_id . "', '" . $device_name . "', '" . $login . "');");
        }

        public function DelDevice($login, $device_id){
            $this->db->Query("DELETE FROM `Devices` WHERE `Devices`.`User_login` = '" . $login . "' AND `Devices`.`Device_id` = '" . $device_id . "' ");
        }

        public function GetDevicesByLogin($login){
            return $this->db->Select("SELECT `Device_id`,`Name`,`Is_shutdown_pending` as Status FROM `Devices` WHERE `User_login` = '" . $login . "';");
        }

        public function GetShutdownPending($login, $device_id){
            return $this->db->Select("SELECT `Is_shutdown_pending` FROM `Devices` WHERE `Device_id` = '". $device_id . "' AND `User_login` = '". $login . "';");
        }

        public function SetShutdownPending($device_id)
        {
            return $this->db->Query("UPDATE `Devices` SET `Is_shutdown_pending` = 1 WHERE `Device_id` = '". $device_id . "';");
        }

        public function ClearShutdownPending($device_id)
        {
            return $this->db->Query("UPDATE `Devices` SET `Is_shutdown_pending` = 0 WHERE `Device_id` = '". $device_id . "';");
        }

        public function VerifyDeviceId($device_id)
        {
            return $this->db->Select("Select `Device_id` FROM `Devices` WHERE `Device_id` = '". $device_id . "';");
        }
    }
?>