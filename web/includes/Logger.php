<?php

class Logger
{
    private $file;

    public function __construct()
    {
        $this->file = dirname(__FILE__) . "/../logs/web.log";

        if(!file_exists(dirname($this->file)))
            mkdir(dirname($this->file), 0777, true);
    }

    public function Error($message, $array = "")
    {
        if ($array == "")
            $this->Log("ERROR: " . $message . PHP_EOL . json_encode($array));
        else
            $this->Log("ERROR: " . $message);
    }

    public function Debug($message, $array = "")
    {
        if ($array == "")
        $this->Log("DEBUG: " . $message . PHP_EOL . json_encode($array));
    else
        $this->Log("DEBUG: " . $message);
    }

    public function Info($message, $array = "")
    {
        if ($array == "")
        $this->Log("INFO: " . $message . PHP_EOL . json_encode($array));
    else
        $this->Log("INFO: " . $message);
    }
    public function Log($message)
    {
        $date = new DateTime();
        $date = $date->format("y:m:d h:i:s");
        $message = $date . " " . $message;
        file_put_contents($this->file, $message . PHP_EOL, FILE_APPEND | LOCK_EX);
    }
}

?>