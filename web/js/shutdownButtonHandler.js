function SetShutdownState(name)
{
    $.post("./api/setShutdownPending.php",
    {
        Device_id: name,
    },
    function(data,status){
        // TODO: Change state shutdown pending...
    });
    alert("Device " + name + "has been set to shut down");
}