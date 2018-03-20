function DeleteDevice(login, name)
{
    $.post("./api/deleteDevice.php",
    {
        Login: login,
        Device_id: name
    },
    function(data, status)
    {
        // TODO: Check return status...
    });

    setTimeout(function()
    {
        // Refresh page to reload table
        location.reload(true);
    }, 200);
}


function SetShutdownState(name)
{
    $.post("./api/Actions/setAction.php",
    {
        Device_id: name,
        Action: "Shutdown"
    },
    function(data, status)
    {
        // TODO: Check return status...
    });
    alert("Device " + name + "has been set to shut down");
}


function SetRestartState(name)
{
    $.post("./api/Actions/setAction.php",
    {
        Device_id: name,
        Action: "Restart"
    },
    function(data, status)
    {
        // TODO: Check return status...
    });
    alert("Device " + name + "has been restarted");
}

function SetSleepState(name)
{
    $.post("./api/Actions/setAction.php",
    {
        Device_id: name,
        Action: "Sleep"
    },
    function(data, status)
    {
        // TODO: Check return status...
    });
    alert("Device " + name + "has been set to sleep");
}



function SetMuteState(name)
{
    $.post("./api/Actions/setAction.php",
    {
        Device_id: name,
        Action: "Mute"
    },
    function(data, status)
    {
        // TODO: Check return status...
    });
    alert("Device " + name + "has been muted");
}
