function DeleteDevice(name)
{
    $.post("/Admin/Delete",
    {
        DeviceId: name
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
    $.post("/Admin/Index",
    {
        DeviceId: name,
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
    $.post("/Admin/Index",
    {
        DeviceId: name,
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
    $.post("/Admin/Index",
    {
        DeviceId: name,
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
    $.post("/Admin/Index",
    {
        DeviceId: name,
        Action: "Mute"
    },
    function(data, status)
    {
        // TODO: Check return status...
    });
    alert("Device " + name + "has been muted");
}

function Logout()
{
    document.location = '/User/Logout';
}
