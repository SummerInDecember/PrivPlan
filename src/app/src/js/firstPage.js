document.getElementById("createAccForm").addEventListener('submit', async (event) => {
    event.preventDefault();
    console.log("Working");

    let data = {
        username: document.getElementById("username").value,
        passwd: document.getElementById("passwd").value,
    }

    let passwdConfirm = document.getElementById("passwdConfirm").value;

    if(data.passwd === passwdConfirm)
        window.versions.send('createAcc', data);
    else
    {
        console.log("You made an oopsy");
         // TODO: Make this send a useful error message and make some stuff red
         /**
          * * I wanted the code above to display a window.alert but that made the input
          * * boxes unusable. Reason: unknown
          */
        
    }
    } );