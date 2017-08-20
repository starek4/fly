<?php

class Db{
    // Connection to database
    var $mysqli;

    function __construct()
    {
        $str = file_get_contents(__DIR__ . '/config.json');
        $credentials = json_decode($str, true);
        $this->mysqli = new mysqli('127.0.0.1', $credentials["dbUser"], $credentials["dbPass"], $credentials["dbName"]);
        if ($this->mysqli->connect_errno) {
            echo "Sorry, this website is experiencing problems.";

            echo "Error: Failed to make a MySQL connection, here is why: \n";
            echo "Errno: " . $this->mysqli->connect_errno . "\n";
            echo "Error: " . $this->mysqli->connect_error . "\n";
            
            // You might want to show them something nice, but we will simply exit
            exit;
        }
    }

    private function ExecQuery($query){
        // Send query to database
        return $this->mysqli->query($query);        
    }

    public function Select($query){
        $select = $this->ExecQuery($query);
        if ($select === false)
        {
            echo "Error when doing Select: " . $query;
            exit;
        }
        for ($res = array(); $tmp = $select->fetch_array();) $res[] = $tmp;
        return $res;
    }

    public function Query($query){
        $update = $this->ExecQuery($query);

        if($update === false)
        {
            echo "Error when ExecQuery: " . $query;
            exit;
        }
    }
}

?>