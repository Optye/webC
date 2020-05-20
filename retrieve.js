/*
Author: Robert Lie (mobilefish.com)
The retrieve.js file retrieves sensor data from The Things Network and displays it in the terminal.
See LoRa/LoRaWAN Tutorial 27
https://www.mobilefish.com/download/lora/lora_part27.pdf

Prerequisites:
Install the following applications:
- Node JS version: v10.6.0 (node -v)
- NPM version: 6.5.0 (npm -v)

Usage:
1) Update file config.js
2) Start the app: node retrieve.js

Additional information:
- TTN API, more information:
  https://github.com/TheThingsNetwork/node-app-sdk
- LoRa/LoRaWAN Tutorial 25
  https://youtu.be/lZXiaMFYwfw
  https://www.mobilefish.com/download/lora/lora_part25.pdf
- LoRa/LoRaWAN Tutorial 26
  https://youtu.be/EMoZ9taGZRs
  https://www.mobilefish.com/download/lora/lora_part26.pdf
*/

const ttn = require('ttn');
const moment = require('moment');
const config = require('./config.js');
const hex2ascii = require('hex2ascii');

const appID = config.TTNOptions.appID;
const accessKey = config.TTNOptions.accessKey;

function Decoder(bytes) {
    console.log(bytes);
    jsonString = stringFromUTF8Array(bytes);
    console.log(jsonString);
    var data = JSON.parse(jsonString);
    console.log(data.A);
    console.log(data.B);
    return data;
}

ttn.data(appID, accessKey)
    .then(function (client) {
        client.on("uplink", async function (devID, payload) {
            console.log("Received uplink from ", devID);
            console.log(payload);

            const gateways = payload.metadata.gateways;
            for (i=0; i<gateways.length; i++){
                console.log(gateways[i]);
            }

            console.log("payload_raw decodded = ",Decoder(payload.payload_raw));

        })
    })
    .catch(function (error) {
        console.error("Error", error)
        process.exit(1);
    })

function stringFromUTF8Array(data)
  {
    const extraByteMap = [ 1, 1, 1, 1, 2, 2, 3, 0 ];
    var count = data.length;
    var str = "";
    
    for (var index = 0;index < count;)
    {
      var ch = data[index++];
      if (ch & 0x80)
      {
        var extra = extraByteMap[(ch >> 3) & 0x07];
        if (!(ch & 0x40) || !extra || ((index + extra) > count))
          return null;
        
        ch = ch & (0x3F >> extra);
        for (;extra > 0;extra -= 1)
        {
          var chx = data[index++];
          if ((chx & 0xC0) != 0x80)
            return null;
          
          ch = (ch << 6) | (chx & 0x3F);
        }
      }
      
      str += String.fromCharCode(ch);
    }
    
    return str;
  }