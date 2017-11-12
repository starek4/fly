<?php
require_once(dirname(__FILE__) . "/Db.php");

    class DeviceRepository{
        var $db;

        function __construct()
        {
            $this->db = new Db();
        }

        public function AddDevice($login, $device_id, $device_name)
        {
            $query = "INSERT INTO `Devices` (`Id`, `Device_id`, `Name`, `User_login`) VALUES (NULL,?,?,?)";
            $this->db->Query($query, "sss", array($device_id, $device_name, $login));
        }

        public function DelDevice($login, $device_id)
        {
            $query = "DELETE FROM `Devices` WHERE `Devices`.`User_login` = ? AND `Devices`.`Device_id` = ?";
            $this->db->Query($query, "ss", array($login, $device_id));
        }

        public function GetDevicesByLogin($login)
        {
            $query = "SELECT `Device_id`,`Name`,`Is_shutdown_pending` as Status FROM `Devices` WHERE `User_login` = ?";
            return $this->db->Select($query, "s", array($login));
        }

        public function GetShutdownPending($login, $device_id)
        {
            $query = "SELECT `Is_shutdown_pending` FROM `Devices` WHERE `Device_id` = ? AND `User_login` = ?";
            return $this->db->Select($query, "ss", array($device_id,$login));
        }

        public function SetShutdownPending($device_id)
        {
            $query = "UPDATE `Devices` SET `Is_shutdown_pending` = ? WHERE `Device_id` = ?";
            $this->db->Query($query, "is", array(1, $device_id));
        }

        public function ClearShutdownPending($device_id)
        {
            $query = "UPDATE `Devices` SET `Is_shutdown_pending` = ? WHERE `Device_id` = ?";
            $this->db->Query($query, "is", array(0, $device_id));
        }

        public function VerifyDeviceId($device_id)
        {
            $query = "Select `Device_id` FROM `Devices` WHERE `Device_id` = ?";
            return $this->db->Select($query, "s", array($device_id));
        }
    }
?>