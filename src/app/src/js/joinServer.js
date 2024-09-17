const tryJoinUri = 'api/AllowedDevices';

const ServerObj = {
    serverIpOrDomain:document.getElementById("server").value,
    password: document.getElementById("servPass").value,
    hasDoubleColon: server.includes(":"),
    hasPeriod: server.includes("."),
    endsWithSlash: server.endsWith("/")
};

function joinServer()
{
    if(ServerObj.serverIpOrDomain && ServerObj.hasDoubleColon && ServerObj.hasPeriod 
        && ServerObj.password)
    {
        var partsOfIp = server.split(".");
        var ipAndPort = server.split(":");

        if(validateIp(partsOfIp, ipAndPort))
        {

        }
        else
        {
            if(!ipAndPort[1])
            {
                if(validateIp(partsOfIp, server.concat("80").split(":")))
                {

                }
            }
            else
            {
                console.log("invalid ip address");
            }
        }
        
    }
    else
    {
        if(!ServerObj.hasDoubleColon && ServerObj.hasPeriod)
        {
            if(validateIp(partsOfIp, server.concat(":80").split(":")))
            {

            }
        }
        else
            console.log("invalid ip address");
    }
}

function validateIp(partsOfIp, ipAndPort)
{
    try{
            
        if(partsOfIp.length === 4 && ipAndPort[1]) // validates if its a valid ip address with a port
        {

            let numPartsOfIp = partsOfIp.map(str => parseInt(str));

            for(num of numPartsOfIp)
            {
                if(isNaN(num))
                    return false;
            }

            if(numPartsOfIp[0] >= 1 && numPartsOfIp[3] >= 1         // Im so sorry for this
            && numPartsOfIp[1] >= 0 && numPartsOfIp[2] >= 0         // but i just cant wrap my head 
            && numPartsOfIp[0] < 256 && numPartsOfIp[3] < 256       // around regex
            && numPartsOfIp[0] < 224 && numPartsOfIp[3] < 255)      // maybe one day i will take the
            {                                                       // time to really learn regex,
                console.log("valid ip part");                       // then I will replace this monster
            }                                                       // for regexp
            else
            {
                console.log("You messed up the ip lol");
                return false
            }
            

            return true;
        }
        else
        {
            return false;
        }
    }
    catch(err)
    {
        console.log(err);   
        return false;    
    }
}

async function sendRequest(server)
{
    if(ServerObj.endsWithSlash)
    {
        server = server.concat(tryJoinUri);
    }
    else
    {
        server = server.concat(`/${tryJoinUri}`);
    }

    const reqBody = {
        password: servPass
    }

    let response = await fetch(server, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-type': 'application/json'
        },
        body: reqBody
    })

    // TODO: Keep working on this
}
