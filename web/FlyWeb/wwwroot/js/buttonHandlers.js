function RenameDevice(name)
{
    var newName = prompt("Enter new device name", "My awesome device");
    if (newName === null || newName === "")
    {
        return;
    }
    $.post("/Admin/Rename",
    {
        DeviceId: name,
        newName: newName
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

function DeleteDevice(name)
{
    if (!confirm("Do you really want to delete this device?"))
    {
        return
    }
    $.post("/Admin/Delete",
        {
            DeviceId: name
        },
        function (data, status) {
            // TODO: Check return status...
        });

    setTimeout(function () {
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
