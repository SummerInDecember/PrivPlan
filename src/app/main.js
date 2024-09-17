const { app, BrowserWindow, ipcMain} = require('electron/main')
const path = require('path')
const fs = require("node:fs");
const os = require("os");
const { dialog } = require('electron');

const platform = process.platform;
const pathToMainConfigLinux = `/home/${os.userInfo().username}/.config/Privplan/config.json`;

console.log(os.userInfo().username)
const createWindow = () => {
  const win = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      preload: path.join(__dirname, 'preload.js')
    }
  });

  try
  {
  switch(platform)
  {
    case "win32":
      console.log("TODO: implement windows features");
      break;
    case "darwin":
      console.log("TODO: implement macos features");
      break;
    case "linux":
      

      try{ // check if config exists and if it has any error
        fs.access(pathToMainConfigLinux, fs.constants.F_OK, (failedToRead) =>{
          if(failedToRead)
            win.loadFile('src/html/firstPage.html');
          else
          {    
            fs.readFile(pathToMainConfigLinux, "utf8", (err, data) => {
                if(err){
                  
                }
                else{
                  try{
                    let mainCfgJson = JSON.parse(data);
                  }
                  catch(error)
                  {
                    console.log("There was a problem with the json");
                    win.loadFile('src/html/firstPage.html');

                    dialog.showMessageBox(win, {
                      type: "error",
                      buttons: ["OK"],
                      Title: "Alert",
                      detail: `There was a problem with the json at ${pathToMainConfigLinux}` 
                    });
                  }
                }
            
                
              });
          }
        })

      }
      catch(err){

      }
      break;
      
    default:
      console.log("operating system not supported");
      throw "operating system not supported";
  }
  }
  catch(err)
  {
    if(err === "operating system not supported")
      process.abort();
  }
}

app.whenReady().then(() => {
  createWindow()

  app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
      createWindow()
    }
  })
})

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit()
  }
})

ipcMain.on('createAcc', (event, data) => {
  console.log("STUFF");
  console.log('Received login data:', data);
  // You can process or validate the data here
  // For example, you might send a response back to the renderer process
});