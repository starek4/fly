function DeleteDevice(login, name)
{
    $.post("./api/deleteDevice.php",
    {
    Login: login,
    Device_id: name
    });
    // Refresh page to reload table
    location.reload(true);
}