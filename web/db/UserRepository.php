<?php
require_once(__DIR__ . "/Db.php");

class UserRepository{
    var $db;

    function __construct(){
        $this->db = new Db();
    }

    public function AddUser($login, $passwd, $email){
        $hashPasswd = password_hash($passwd, PASSWORD_DEFAULT);
        $this->db->Query("INSERT INTO `Users` (`Login`, `Pass`, `Email`) VALUES ('" . $login . "', '" . $hashPasswd . "', '" . $email . "');");
    }

    public function DelUser($login)
    {
        $this->db->Query("DELETE FROM `Devices` WHERE `Devices`.`User_login` = '" . $login . "'");  // Firstly delete devices
        $this->db->Query("DELETE FROM `Users` WHERE `Users`.`Login` = '" . $login . "'");           // Then delete user
    }

    public function CheckPassword($login, $password)
    {
        $dbPassword = $this->db->Select("SELECT `Pass` FROM `Users` WHERE `Login` = '" . $login . "';");
        if (password_verify($password, $dbPassword[0]["Pass"]))
            return true;
        return false;
    }

    public function CheckIfUserExists($login)
    {
        $validLogin = $this->db->Select("SELECT `Login` FROM `Users` WHERE `Login` = '" . $login . "';");
        if($validLogin[0]["Login"] == $login) return true;
        return false;
    }
}

?>