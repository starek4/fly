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

    // Refresh page to reload table
    location.reload(true);
}
