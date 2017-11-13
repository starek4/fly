<?php

class Db
{
    // Connection to database
    var $mysqli;

    function __construct()
    {
        $configFileContent = file_get_contents(__DIR__ . '/config.json');
        $credentials = json_decode($configFileContent, true);
        $this->mysqli = new mysqli('127.0.0.1', $credentials["dbUser"], $credentials["dbPass"], $credentials["dbName"]);
        if ($this->mysqli->connect_errno)
        {
            throw new Exception("Cannot connect to DB.");
        }

        // Set UTF-8 encoding
        $this->mysqli->query("SET NAMES 'utf8'");
    }

    public function Query($query, $variable_types, $variables)
    {
        $statement = $this->mysqli->prepare($query);
        $statement->bind_param($variable_types, ...$variables);

        $statement->execute();
        if($statement === false)
        {
            $statement->close();
            throw new Exception("Cannot exec query: " . $query);
        }
        $statement->close();
    }

    public function Select($query, $variable_types, $variables)
    {
        $statement = $this->mysqli->prepare($query);
        $statement->bind_param($variable_types, ...$variables);
        $statement->execute();

        if($statement === false)
        {
            $statement->close();
            throw new Exception("Cannot exec query: " . $query);
        }

        $meta = $statement->result_metadata();
        while ($field = $meta->fetch_field())
        {
            $params[] = &$row[$field->name];
        }

        call_user_func_array(array($statement, 'bind_result'), $params);

        while ($statement->fetch())
        {
            foreach($row as $key => $val)
            {
                $c[$key] = $val;
            }
            $result[] = $c;
        }

        $statement->close();
        return $result;
    }
}

?>