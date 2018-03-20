<?php
    class ActionsMapper
    {
        var $actionNamesMapper;

        function __construct()
        {
            $this->actionNamesMapper["Shutdown"] = "Is_shutdown_pending";
            $this->actionNamesMapper["Restart"] = "Is_restart_pending";
            $this->actionNamesMapper["Sleep"] = "Is_sleep_pending";
            $this->actionNamesMapper["Mute"] = "Is_mute_pending";
        }

        public function GetActionName($dbActionName)
        {
            return array_search($dbActionName, $actionNamesMapper);
        }

        public function GetDbActionName($actionName)
        {
            return $this->actionNamesMapper[$actionName];
        }
    }
?>
