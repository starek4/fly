<?php

class Session
{
    function __construct()
    {
        session_start();
    }

    public function CheckIfUserIsLogged()
    {
        if (isset($_SESSION["login"]))
        {
            return true;
        }
        return false;
    }

    public function LoginUser($login)
    {
        $_SESSION["login"] = $login;
    }

    public function LogoutUser()
    {
        unset($_SESSION["login"]);
    }
}

?>