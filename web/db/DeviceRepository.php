<?php
require_once(dirname(__FILE__) . "/Db.php");

    class DeviceRepository{
        var $db;

        function __construct()
        {
            $this->db = new Db();
        }

        public function AddDevice($login, $device_id, $device_name, $shutdownable)
        {
            $query = "INSERT INTO `Devices` (`Id`, `Device_id`, `Name`, `User_login`, `Is_shutdownable`) VALUES (NULL,?,?,?,?)";
            $this->db->Query($query, "sssi", array($device_id, $device_name, $login, $shutdownable));
        }

        public function DelDevice($login, $device_id)
        {
            $query = "DELETE FROM `Devices` WHERE `Devices`.`User_login` = ? AND `Devices`.`Device_id` = ?";
            $this->db->Query($query, "ss", array($login, $device_id));
        }

        public function GetDevicesByLogin($login)
        {
            $query = "SELECT `Device_id`,`Name`,`Is_shutdown_pending` as Status,`Last_active` FROM `Devices` WHERE `User_login` = ? AND `Is_shutdownable` = 1";
            return $this->db->Select($query, "s", array($login));
        }

        public function GetShutdownPending($device_id)
        {
            $query = "SELECT `Is_shutdown_pending` FROM `Devices` WHERE `Device_id` = ?";
            return $this->db->Select($query, "s", array($device_id));
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
            $query = "SELECT `Device_id` FROM `Devices` WHERE `Device_id` = ?";
            return $this->db->Select($query, "s", array($device_id));
        }

        public function UpdateLastActive($device_id)
        {
            $query = "UPDATE `Devices` SET `Last_active` = current_timestamp WHERE `Device_id` = ?";
            return $this->db->Query($query, "s", array($device_id));
        }

        public function GetLoggedState($device_id)
        {
            $query = "SELECT `Is_logged`,`User_login` FROM `Devices` WHERE `Device_id` = ?";
            return $this->db->Select($query, "s", array($device_id));
        }

        public function SetLoggedState($device_id)
        {
            $query = "UPDATE `Devices` SET `Is_logged` = ? WHERE `Device_id` = ?";
            $this->db->Query($query, "is", array(1, $device_id));
        }

        public function ClearLoggedState($device_id)
        {
            $query = "UPDATE `Devices` SET `Is_logged` = ? WHERE `Device_id` = ?";
            $this->db->Query($query, "is", array(0, $device_id));
        }
    }
?>
