/*
Author: Robert Lie (mobilefish.com)
The config.js file contains the MySQL user credentials and The Things Network (TTN) appID and accessKey.
See LoRa/LoRaWAN Tutorial 27
https://www.mobilefish.com/download/lora/lora_part27.pdf

The config.js file is used by:
- drop_db.js
- create_db.js
- create_table.js
- store_records.js
- read_table.js
- retrieve.js
- send.js
*/
const databaseOptions = {
    host: 'localhost',
    user: 'webcontroller',
    password: 'web'
};

const TTNOptions = {
    appID: '20180102',
    accessKey: 'ttn-account-v2.PAUZXYPrQB7VVhB3x_55OsfZrdAQX5S42nxExNSYk_E'
};

module.exports = {databaseOptions: databaseOptions, TTNOptions: TTNOptions};
