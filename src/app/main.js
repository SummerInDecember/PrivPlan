const { app, BrowserWindow, ipcMain} = require('electron/main')
const path = require('path')
const fs = require("node:fs");
const os = require("os");
const { dialog } = require('electron');
const crypto = require('crypto');

const platform = process.platform;
const pathToMainConfigLinux = `/home/${os.userInfo().username}/.config/Privplan/config.json`;

/**
 * This function is required for electron to work. 
 *  - the variable "globWin" is used so that other functions can access the "win" 
 */

var globWin;
console.log(os.userInfo().username)
const createWindow = () => {
  const win = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      preload: path.join(__dirname, 'preload.js')
    }
  });

  globWin = win; 

  switch(platform)
  {
    case "win32":
      console.log("TODO: implement windows features");
      break;
    case "darwin":
      console.log("TODO: implement macos features");
      break;
    case "linux":
      loadLinux(win);
      break;
      
    default:
      console.log("operating system not supported");
      throw "operating system not supported";
  }
  
}

/**
 * Function gets the config for linux, the function works in the following way:
 *  - It tries to read the config file, if it fails it loads "firstPage.html"
 *    which lets an user create an account which creates the config file
 * 
 *  - If there is VALID a config file it parses the json and 
 * 
 * TODO: Finish the comment and the function
 */
function loadLinux(win)
{
  try{
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
            }
          }
        });
      }
    })
    }
    catch(err)
    {
      console.log("«««««««««««««««ERROR««««««««««««««««««««««");
      console.log(err);
      console.log("««««««««««««««««««««««««««««««««««««««««««")
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

/**
 * The following code creates a user config file where the password is securely stored
 */
ipcMain.on('createAcc', async (event, data) => {
  
  if(!(await validateUserNameAndPassword(data.username, data.passwd)))
    return;

  data["salt"] = await genSalt();
  data.passwd = crypto.createHash('sha256').update(`${data.passwd}${data.salt}`).digest('hex');

  fs.writeFile(pathToMainConfigLinux, JSON.stringify(data, null, 2), (error) => {
    if(error)
    {
      dialog.showMessageBox(globWin, {
        type: "error",
        buttons: ["OK"],
        Title: "Alert",
        detail: `There was a problem with the json at ${pathToMainConfigLinux}` 
      });

      return;
    }

    dialog.showMessageBox(globWin, {
      type: "info",
      buttons: ["OK"],
      Title: "Alert",
      detail: `Account created successfully` 
    });

    globWin.loadFile('src/html/logIn.html');
  })
  
  // You can process or validate the data here
  // For example, you might send a response back to the renderer process
});


/**
 * Generates a random 64 character salt
 */
async function genSalt()
{
  let buffer = await crypto.randomBytes(64);
  let token = await buffer.toString('hex');
  return token;
}

/**
 * Function with awfully long name, but seems discriptive enough to me :)
 * This is done here instead of the js file linked to the html so that it wont take too long to load
 * (speed it important i think)
 */
async function validateUserNameAndPassword(username, password)
{
  // RegExp checks if password contains 1 capital letter, 1 lower case 
  // letter, 1 digit and one special character (minum)
  const passwdValidate = new RegExp("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\\W).+$");

  if(username.length < 3)
  {
    dialog.showMessageBox(globWin, {
      type: "error",
      buttons: ["OK"],
      title: "Alert",
      detail: "Username must contain at least 3 characters"
    });

    return false;
  }

  if(password.length < 8 || !passwdValidate.test(password))
  {
    dialog.showMessageBox(globWin, {
      type: "error",
      buttons: ["OK"],
      title: "Alert",
      detail: "Password must be larger than 8 characters and it must include: 1 capital letter, 1 lower case\
      letter, 1 digit and one special character (minum)"
    });

    return false;
  }

  return true;

  
}