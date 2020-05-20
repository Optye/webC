const ttn = require('ttn');
const mysql = require('mysql');
const moment = require('moment');
const config = require('./config.js');
config.databaseOptions.database = "web_con_db";

const appID = config.TTNOptions.appID;
const accessKey = config.TTNOptions.accessKey;

const con = mysql.createConnection(config.databaseOptions);

con.connect(function(err) {
    if (err) throw err;
    console.log("Connected to database");
});

ttn.data(appID, accessKey)
    .then(function (client) {
        client.on("uplink", async function (devID, payload) {
        console.log("Received uplink from", devID);
        if( payload.counter != undefined) {
            jsonString = stringFromUTF8Array(payload.payload_raw);
            var data = JSON.parse(jsonString);
            console.log(data);
            const query = "INSERT INTO data SET ?";
            const values = {
                A: data.A,
                B: data.B
            };
            con.query(query, data, function (err, results) {
                if(err) throw err;
                    console.log("Record inserted: ",results.insertId);
                });
            }
        })
    })
    .catch(function (error) {
        console.error("Error", error)
        process.exit(1)
    })

function stringFromUTF8Array(data){
    const extraByteMap = [ 1, 1, 1, 1, 2, 2, 3, 0 ];
    var count = data.length;
    var str = "";
    
    for (var index = 0;index < count;){
        var ch = data[index++];
        if (ch & 0x80){
            var extra = extraByteMap[(ch >> 3) & 0x07];
            if (!(ch & 0x40) || !extra || ((index + extra) > count))
                return null;
            
            ch = ch & (0x3F >> extra);
            for (;extra > 0;extra -= 1){
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