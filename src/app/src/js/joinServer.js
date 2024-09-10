function joinServer()
{
    let server = document.getElementById("server").value;
    if(server && server.includes("."))
    {
        let parts = server.split(".");

        try{

            if(parts.length === 4) // validates if its a valid ip address with a port
            {
                for(let i = 0; i<parts.length; i++)
                {
                    let num = parseInt(parts[i]);
                    console.log((num < 1 && (i == 0 || i == 3)));
                    if((!isNaN(num)) || (num < 1 && (i == 0 || i == 3)) 
                    || (num < 0 && (i == 1 || i == 2)) || (num > 223 && i == 0)
                    || (num < 255 && (i == 1 || i == 2)) || (num < 254 && i ==3))
                    {
                        console.log("valid ip part");
                    }
                    else
                    {
                        throw "invalid ip address";
                    }
                }
            }
            else
            {

            }
        }
        catch(err)
        {
            console.log(err);
        }
    }
    else
    {
        console.log("error");
    }
}