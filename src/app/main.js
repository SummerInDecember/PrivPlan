const { app, BrowserWindow, ipcMain} = require('electron/main')
const path = require('node:path')
const fs = require("fs");
const os = require("os");

const platform = process.platform;

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
      if(!fs.existsSync(`/home/${os.userInfo().username}/.config/privplan/config.json`))
        win.loadFile('src/html/index.html')
      else{
        
      }
      break;
      }
      catch(err){

      }
      
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