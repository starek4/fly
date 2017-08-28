<?php

class Db
{
    // Connection to database
    var $mysqli;

    function __construct()
    {
        $str = file_get_contents(__DIR__ . '/config.json');
        $credentials = json_decode($str, true);
        $this->mysqli = new mysqli('127.0.0.1', $credentials["dbUser"], $credentials["dbPass"], $credentials["dbName"]);
        if ($this->mysqli->connect_errno)
        {
            echo "Failed to connect to the database.";
            echo "Errno: " . $this->mysqli->connect_errno . "\n";
            echo "Error: " . $this->mysqli->connect_error . "\n";
            exit;
        }
        // Set UTF-8 encoding
        $this->mysqli->query("SET NAMES 'utf8'");
    }

    public function Query($query, $variable_types, $variables)
    {
        $stmt = $this->mysqli->prepare($query);
        $stmt->bind_param($variable_types, ...$variables);

        $stmt->execute();
        if($stmt === false)
        {
            $stmt->close();
            echo "Error when ExecQuery: " . $query;
            exit;
        }
        $stmt->close();
    }

    public function Select($query, $variable_types, $variables)
    {
        $stmt = $this->mysqli->prepare($query);
        $stmt->bind_param($variable_types, ...$variables);
        $stmt->execute();

        if($stmt === false)
        {
            $stmt->close();
            exit;
        }

        $meta = $stmt->result_metadata(); 
        while ($field = $meta->fetch_field()) 
        { 
            $params[] = &$row[$field->name]; 
        } 
    
        call_user_func_array(array($stmt, 'bind_result'), $params); 
    
        while ($stmt->fetch()) { 
            foreach($row as $key => $val) 
            { 
                $c[$key] = $val; 
            } 
            $result[] = $c; 
        } 

        $stmt->close();
        return $result;
    }
}

?>