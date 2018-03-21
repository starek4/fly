<?php
    require_once(dirname(__FILE__) . "/Db.php");
    require_once(dirname(__FILE__) . "/../api/Actions/ActionsMapper.php");

    class DeviceRepository{
        var $db;
        var $actionsMapper;

        function __construct()
        {
            $this->db = new Db();
            $this->actionsMapper = new ActionsMapper();
        }

        public function AddDevice($login, $device_id, $device_name, $actionable)
        {
            $query = "INSERT INTO `Devices` (`Id`, `Device_id`, `Name`, `User_login`, `Is_actionable`) VALUES (NULL,?,?,?,?)";
            $this->db->Query($query, "sssi", array($device_id, $device_name, $login, $actionable));
        }

        public function DelDevice($login, $device_id)
        {
            $query = "DELETE FROM `Devices` WHERE `Devices`.`User_login` = ? AND `Devices`.`Device_id` = ?";
            $this->db->Query($query, "ss", array($login, $device_id));
        }

        public function GetDevicesByLogin($login)
        {
            $query = "SELECT `Device_id`,`Name`,`Is_shutdown_pending`,`Is_restart_pending`,`Is_sleep_pending`,`Is_mute_pending` as Status,`Last_active` FROM `Devices` WHERE `User_login` = ? AND `Is_actionable` = 1";
            return $this->db->Select($query, "s", array($login));
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

        public function GetFavourite($device_id)
        {
            $query = "SELECT `Is_favourite`,`User_login` FROM `Devices` WHERE `Device_id` = ?";
            return $this->db->Select($query, "s", array($device_id));
        }

        public function SetFavourite($device_id)
        {
            $query = "UPDATE `Devices` SET `Is_favourite` = ? WHERE `Device_id` = ?";
            $this->db->Query($query, "is", array(1, $device_id));
        }

        public function ClearFavourite($device_id)
        {
            $query = "UPDATE `Devices` SET `Is_favourite` = ? WHERE `Device_id` = ?";
            $this->db->Query($query, "is", array(0, $device_id));
        }

        /* Actions handlers */
        public function GetActionPending($device_id, $action)
        {
            $action = $this->actionsMapper->GetDbActionName($action);
            $query = "SELECT `" . $action . "` FROM `Devices` WHERE `Device_id` = ?";
            return $this->db->Select($query, "s", array($device_id));
        }

        public function SetActionPending($device_id, $action)
        {
            $action = $this->actionsMapper->GetDbActionName($action);
            $query = "UPDATE `Devices` SET `" . $action . "` = ? WHERE `Device_id` = ?";
            $this->db->Query($query, "is", array(1, $device_id));
        }

        public function ClearActionPending($device_id, $action)
        {
            $action = $this->actionsMapper->GetDbActionName($action);
            $query = "UPDATE `Devices` SET `" . $action . "` = ? WHERE `Device_id` = ?";
            $this->db->Query($query, "is", array(0, $device_id));
        }
    }
?>
