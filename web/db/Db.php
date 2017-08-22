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

    // Simply sends query to database (just to comply OO style)
    private function ExecQuery($query)
    {
        return $this->mysqli->query($query);        
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