var script = document.createElement('script');
script.src = './js/jquery-3.2.1.min.js';
script.type = 'text/javascript';
document.getElementsByTagName('head')[0].appendChild(script);

function SeteShutdownState()
{
    $("button").each(function() {
        $.post("./api/setShutdownPending.php",
        {
          Device_id: this.name,
        },
        function(data,status){
            // TODO: Change state shutdown pending...
        });
        }
      );
}