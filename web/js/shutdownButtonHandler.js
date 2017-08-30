function SetShutdownState(name)
{
    $.post("./api/setShutdownPending.php",
    {
        Device_id: name,
    },
    function(data, status)
    {
        // TODO: Check return status...
    });
    alert("Device " + name + "has been set to shut down");
}
