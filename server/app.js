// testing / real query flag
var offline = true;
// flags from command line
if (process.argv[2] == "online") {
    offline = false;
}

var express = require('express');
var app = express();
var fs = require('fs');
var Twitter = require('twitter');
var creds1 = require('./creds/creds1.js');
var creds2 = require('./creds/creds2.js');
var creds3 = require('./creds/creds3.js');
var creds4 = require('./creds/creds4.js');
var creds5 = require('./creds/creds5.js');
var creds6 = require('./creds/creds6.js');
var creds7 = require('./creds/creds7.js');
var creds8 = require('./creds/creds8.js');

var errMsg, sucMsg;
var error = function(err, response, body) {
    console.log('ERROR \n');
    console.log(err);
    errMsg = err;
};
var success = function(data) {
    console.log('Success!\n' + data);
    sucMsg = data;
}

//OAuth data
var clientArray = [
new Twitter({
    consumer_key: creds1.consumer_key,
    consumer_secret: creds1.consumer_secret,
    access_token_key: creds1.access_token_key,
    access_token_secret: creds1.access_token_secret
}),
new Twitter({
    consumer_key: creds2.consumer_key,
    consumer_secret: creds2.consumer_secret,
    access_token_key: creds2.access_token_key,
    access_token_secret: creds2.access_token_secret
}),
new Twitter({
    consumer_key: creds3.consumer_key,
    consumer_secret: creds3.consumer_secret,
    access_token_key: creds3.access_token_key,
    access_token_secret: creds3.access_token_secret
}),
new Twitter({
    consumer_key: creds4.consumer_key,
    consumer_secret: creds4.consumer_secret,
    access_token_key: creds4.access_token_key,
    access_token_secret: creds4.access_token_secret
}),
new Twitter({
    consumer_key: creds5.consumer_key,
    consumer_secret: creds5.consumer_secret,
    access_token_key: creds5.access_token_key,
    access_token_secret: creds5.access_token_secret
}),
new Twitter({
    consumer_key: creds6.consumer_key,
    consumer_secret: creds6.consumer_secret,
    access_token_key: creds6.access_token_key,
    access_token_secret: creds6.access_token_secret
}),
new Twitter({
    consumer_key: creds7.consumer_key,
    consumer_secret: creds7.consumer_secret,
    access_token_key: creds7.access_token_key,
    access_token_secret: creds7.access_token_secret
}),
new Twitter({
    consumer_key: creds8.consumer_key,
    consumer_secret: creds8.consumer_secret,
    access_token_key: creds8.access_token_key,
    access_token_secret: creds8.access_token_secret
}),
];

var clientCounter = 0;
var curClient = clientArray[clientCounter];

app.listen(3000, function() {
    console.log('listening on port 3000!');
    if (offline) {
        console.log('Running in Offline test mode!');
        // getFollowers('michaelhazani');
    } else {
        console.log('WARNING: running LIVE!');
        console.log('Client number: ' + clientCounter);
        // getFollowers('michaelhazani');
    }
});

app.get('/followers/:twitterUser', function(req, res) {
    var response = res;
    // console.log("app.get, response: ");
    // console.log(res);
    getFollowers(req.params.twitterUser, -1, res);
});

//-----------------------------------DATA FETCHING/PARSING HELPERS---------------------------------------------
var totalFollowersJSON = [];

function getFollowers(username, curCursor, res) { //Twitter API: Get Followers
    // console.log("getFollowers res,: ")
    // console.log(res);
    var localJSON;
    var path;
    if (offline) {
        if (username == "1") {
            path = __dirname + "/dummyData/1.JSON";
        } else if (username == "2") {
            path = __dirname + "/dummyData/2.JSON";
        } else if (username == "3") {
            path = __dirname + "/dummyData/3.JSON";
        } else if (username == "4") {
            path = __dirname + "/dummyData/4.JSON";
        } else {
            path = __dirname + "/dummyData/dummy-20.JSON";
        }
        fs.readFile(path, "utf-8", function(err, data) {
            if (err) throw err;
            // console.log(data);
            var parsedData = JSON.parse(data);
            var followers = CreateFollowersObject(parsedData);
            followersString = JSON.stringify(followers);
            console.log("sending followers//local! #" + username);
            res.send(followersString);
        });

    } else { // online realtime fetch:
        curClient.get('followers/list.json', {
            screen_name: username,
            cursor: curCursor,
            count: 200
        }, function(error, tweet, response) {
            if (error) { // prob. api call rate limit
                console.log(error);
                curClient.get('/application/rate_limit_status.json', {
                    resources: 'followers'
                }, function(error, tweet, response) {
                    if (error) {
                        console.log(error);
                        console.log("error!");
                    }
                    var errorMsg = (JSON.parse(response["body"]));
                    var unixTime = errorMsg['resources']['followers']['/followers/list']['reset'];
                    var targetDate = new Date(unixTime);
                    var currentDate = new Date();
                    var diffDate = new Date(targetDate - currentDate);
                    console.log("hours: " + diffDate.getHours() + " minutes: " + diffDate.getMinutes() + " seconds: " + diffDate.getSeconds());
                    console.log("milliseconds: " + diffDate.getMilliseconds());
                    setTimeout(function() {
                        //getFollowers(userName, curCursor);
                    }, 5000);
                });
                //for now, switch creds
                if(clientCounter < clientArray.length-1) {
                  clientCounter++;
                curClient = clientArray[clientCounter];
                console.log("switching client counter to " + clientCounter);
                getFollowers(username, curCursor, res);
              } else {
                console.log("max client reached!");
                return;
              }


            } else { // if no error
                var tempJSON = JSON.parse(response.body);
                var nextCursor = tempJSON.next_cursor
                console.log("sending followers//LIVE FROM API, cursor#: " + nextCursor);
                var followers = CreateFollowersObject(response);
                totalFollowersJSON = totalFollowersJSON.concat(followers);
                if (nextCursor != 0) {
                    getFollowers(username, nextCursor, res);
                } else {
                    var totalFollowersString = JSON.stringify(totalFollowersJSON);
                    res.send(totalFollowersString);

                    // for dummy local JSON file creation - do not touch otherwise
                    // var fullMsgString = JSON.stringify(response);
                    // var path = __dirname + "/dummyData/4.JSON";
                    // fs.writeFile(path, fullMsgString, function(err) {
                    //     if (err) {
                    //         return console.log(err);
                    //     }
                    //     console.log("file saved!");
                    // });


                }
            }
        });
    }
}

function CreateFollowersObject(data) { //from Followers response, create data objects that'll be passed to Unity
    var followers = [];
    var body = JSON.parse(data["body"]);
    var users = body["users"];
    for (obj in users) {
        var tempfollower = {};
        tempfollower["id"] = users[obj]["id"];
        tempfollower["screen_name"] = users[obj]["screen_name"];
        tempfollower["name"] = users[obj]["name"];
        tempfollower["location"] = users[obj]["location"];
        tempfollower["url"] = users[obj]["url"];
        tempfollower["description"] = users[obj]["description"];
        tempfollower["followers_count"] = users[obj]["followers_count"];
        tempfollower["friends_count"] = users[obj]["friends_count"];
        tempfollower["profile_image_url"] = users[obj]["profile_image_url"];
        //object
        followers.push(tempfollower);
    }
    if (users != undefined) {
        console.log("return user length: " + users.length);
        // console.log("first element in this batch:");
        // console.log(users[users.length-1].name);
        return followers;
    }
}



//API: /following/:username retrieves array of username's following
// app.get('/following/:twitterUser', function (req, res){
//   if (offline) {
//     var path =  __dirname + "/dummyData/dummy.JSON";
//     var localJSON;
//     fs.readFile(path, "utf-8", function(err, data) {
//       localJSON = JSON.parse(data);
//       if(err) throw err
//     res.send(JSON.stringify(localJSON));
//     console.log(localJSON["body"])["users"];
//     console.log("sent local!");
//   });
// } else {
//   client.get('friends/list.json', {
//     screen_name: req.params.twitterUser
//   }, function(error, user, response){
//     if (error) console.log(error);
//     //var response_array = JSON.parse(response["body"])["users"];
//   //  response_array = object_array.map(getScreenNames);
//    var response_array = concatScreenNames(JSON.parse(response["body"])["users"]);
//     console.log(typeof response_array); // Raw response object.
//     res.send(response_array);
//   })
//   }
// })
