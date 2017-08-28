<?php
require_once(__DIR__ . "/Db.php");

class UserRepository{
    var $db;

    function __construct()
    {
        $this->db = new Db();
    }

    public function AddUser($login, $passwd, $email)
    {
        $hashPasswd = password_hash($passwd, PASSWORD_DEFAULT);
        $query = "INSERT INTO `Users` (`Login`, `Pass`, `Email`) VALUES (?,?,?)";
        $variables = array($login, $hashPasswd, $email);
        $this->db->Query($query, "sss", $variables);
    }
    
    public function DelUser($login)
    {
        $deviceQuery = "DELETE FROM `Devices` WHERE `Devices`.`User_login` = ?";
        $userQuery = "DELETE FROM `Users` WHERE `Users`.`Login` = ?";

        // Delete all user devices
        $this->db->Query($deviceQuery, "s", array(login));

        // Delete user
        $this->db->Query($userQuery, "s", array(login));
    }
    
    public function CheckPassword($login, $password)
    {
        $query = "SELECT `Pass` FROM `Users` WHERE `Login` = ?";
        $dbPassword = $this->db->Select($query, "s", array($login));
        if (password_verify($password, $dbPassword[0]["Pass"]))
            return true;
        return false;
    }

    public function CheckIfUserExists($login)
    {
        $query = "SELECT `Login` FROM `Users` WHERE `Login` = ?";

        $validLogin = $this->db->Select($query, "s", array($login));
        if($validLogin[0]["Login"] == $login) return true;
        return false;
    }
}

?>